using BLL.DTOs;
using DAL;
using DAL.EF.Models;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class NoteService
    {
        DataAccessFactory factory;
        EmailService emailserv;

        public NoteService(DataAccessFactory factory, EmailService emailserv)
        {
            this.factory = factory;
            this.emailserv = emailserv;
        }

        public (bool Success, string Error) Create(int userId, NoteCreateDTO dto)
        {
            var user = factory.UserData().Get(userId);
            if (user == null)
            {
                return (false, "User not found");
            } 

            var mapper = MapperConfig.GetMapper();
            var note = mapper.Map<Note>(dto);

            note.UserId = userId;
            note.UserName = user.Name;
            note.CreatedDateTime = DateTime.UtcNow;

            var created = factory.NoteData().Create(note);
            if (created)
            {
                return (true, null);
            }
            else
            {
                return (false, "Failed to create note");
            }
        }

        public List<NoteDTO> GetByUser(int userId)
        {
            var notes = factory.NoteData().GetByUserId(userId);
            return MapperConfig.GetMapper().Map<List<NoteDTO>>(notes);
        }

        public NoteDTO Get(int id)
        {
            var note = factory.NoteData().Get(id);
            if (note == null)
            {
                return null;
            }
            return MapperConfig.GetMapper().Map<NoteDTO>(note);
        }

        public (bool Success, string Error) Update(int id, int userId, NoteCreateDTO dto)
        {
            var note = factory.NoteData().Get(id);
            if (note == null)
            {
                return (false, "Note not found");
            }
            if (note.UserId != userId)
            {
                return (false, "Not authorized");
            }

            var user = factory.UserData().Get(userId);
            if (user == null)
            {
                return (false, "User not found");
            }

            var mapper = MapperConfig.GetMapper();
            mapper.Map(dto, note);

            note.UserName = user.Name;

            var ok = factory.NoteData().Update(note);
            if (ok)
            {
                return (true, null);
            }
            else
            {
                return (false, "Failed to update note");
            }
        }

        public bool Delete(int id, int userId)
        {
            var note = factory.NoteData().Get(id);
            if (note == null)
            {
                return false;
            }
            if (note.UserId != userId)
            {
                return false;
            }
            return factory.NoteData().Delete(id);
        }
        public List<NoteSummaryDTO> GetAllSummaries()
        {
            var notes = factory.NoteData().Get(); 
            var list = new List<NoteSummaryDTO>();
            foreach (var n in notes)
            {
                list.Add(new NoteSummaryDTO
                {
                    NoteName = n.NoteName,
                    CourseName = n.CourseName,
                    CreatedBy = n.UserName,
                    TotalReviews = n.TotalReviews,      
                    TotalDownloads = n.TotalDownloads,
                    UploadedDate = n.CreatedDateTime.Date.ToString("yyyy-MM-dd"),
                    Price = n.Price
                });
            }
            return list;
        }
        public (bool Success, string Error) AddReview(int userId, int noteId, ReviewCreateDTO dto)
        {
            var user = factory.UserData().Get(userId);
            if (user == null)
            {
                return (false, "User not found");
            }

            var note = factory.NoteData().Get(noteId);
            if (note == null)
            {
                return (false, "Note not found");
            }

            var review = new Review
            {
                NoteId = noteId,
                UserId = userId,
                UserName = user.Name,
                Rating = dto.Rating,
                CreatedAt = DateTime.UtcNow,
                Comment = dto.Comment
            };

            var created = factory.ReviewData().Create(review);
            if (!created)
            {
                return (false, "Failed to save review");
            }

            note.TotalReviews = note.TotalReviews + 1;
            var ok = factory.NoteData().Update(note);
            if (!ok)
            {
                return (false, "Failed to update note review count");
            }

            return (true, null);
        }

        public (bool Success, string Error) BuyNote(int userId, int noteId)
        {
            var user = factory.UserData().Get(userId);
            if (user == null)
            {
                return (false, "User not found");
            }

            var note = factory.NoteData().Get(noteId);
            if (note == null)
            {
                return (false, "Note not found");
            }

            if (user.Balance < note.Price)
            {
                return (false, "Insufficient balance");
            }

            user.Balance = user.Balance - note.Price;
            var userUpdated = factory.UserData().Update(user);
            if (!userUpdated)
            {
                return (false, "Failed to deduct balance");
            }

            note.TotalDownloads = note.TotalDownloads + 1;
            var ok = factory.NoteData().Update(note);
            if (!ok)
            {
                return (false, "Failed to increment downloads");
            }
            try
            {
                emailserv.SendPurchaseEmail(user.Email, user.Name, note.NoteName, note.Price, user.Balance);
            }
            catch
            {
          
            }
            return (true, null);
        }
        public List<NoteSummaryDTO> GetFilteredNotes(NoteFilterDTO filter)
        {
            var notes = factory.NoteData().GetFiltered(filter.NoteName, filter.CourseName, filter.MinPrice, filter.MaxPrice, filter.MinReviews, filter.SortBy, filter.Asc);

            var list = new List<NoteSummaryDTO>();
            foreach (var n in notes)
            {
                list.Add(new NoteSummaryDTO
                {
                    NoteName = n.NoteName,
                    CourseName = n.CourseName,
                    CreatedBy = n.UserName,
                    UploadedDate = n.CreatedDateTime.Date.ToString("yyyy-MM-dd"),
                    Price = n.Price,
                    TotalReviews = n.TotalReviews,
                    TotalDownloads = n.TotalDownloads
                });
            }
            return list;
        }
    }
}
