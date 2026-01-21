using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    public class NoteRepo : INoteRepository
    {
        NMSContext db;
        public NoteRepo(NMSContext db)
        {
            this.db = db;
        }
        public bool Create(Note entity)
        {
            db.Notes.Add(entity);
            return db.SaveChanges() > 0;
        }

        public List<Note> Get()
        {
            return db.Notes.ToList();
        }

        public Note Get(int id)
        {
            return db.Notes.Find(id);
        }

        public List<Note> GetByUserId(int userId)
        {
            return db.Notes.Where(n => n.UserId == userId).OrderByDescending(n => n.CreatedDateTime).ToList();
        }

        public bool Update(Note entity)
        {
            db.Notes.Update(entity);
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            var note = db.Notes.Find(id);
            if (note == null)
            {
                return false;
            }
            db.Notes.Remove(note);
            return db.SaveChanges() > 0;
        }

        public List<Note> GetFiltered(string noteName, string courseName, decimal? minPrice, decimal? maxPrice, int? minReviews, string sortBy, bool asc)
        {
            IQueryable<Note> q = db.Notes;

            if (!string.IsNullOrWhiteSpace(noteName))
            {
                var nname = noteName.Trim();
                q = q.Where(n => n.NoteName != null && n.NoteName.Contains(nname));
            }

            if (!string.IsNullOrWhiteSpace(courseName))
            {
                var cname = courseName.Trim();
                q = q.Where(n => n.CourseName != null && n.CourseName.Contains(cname));
            }

            if (minPrice.HasValue)
            {
                q = q.Where(n => n.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                q = q.Where(n => n.Price <= maxPrice.Value);
            }

            if (minReviews.HasValue)
            {
                q = q.Where(n => n.TotalReviews >= minReviews.Value);
            }

            var key = (sortBy ?? "created").Trim().ToLowerInvariant();

            switch (key)
            {
                case "notename":
                case "name":
                    q = asc ? q.OrderBy(n => n.NoteName) : q.OrderByDescending(n => n.NoteName);
                    break;
                case "coursename":
                case "course":
                    q = asc ? q.OrderBy(n => n.CourseName) : q.OrderByDescending(n => n.CourseName);
                    break;
                case "price":
                    q = asc ? q.OrderBy(n => n.Price) : q.OrderByDescending(n => n.Price);
                    break;
                case "reviews":
                case "totalreviews":
                    q = asc ? q.OrderBy(n => n.TotalReviews) : q.OrderByDescending(n => n.TotalReviews);
                    break;
                case "downloads":
                case "totaldownloads":
                    q = asc ? q.OrderBy(n => n.TotalDownloads) : q.OrderByDescending(n => n.TotalDownloads);
                    break;
                case "created":
                case "date":
                default:
                    q = asc ? q.OrderBy(n => n.CreatedDateTime) : q.OrderByDescending(n => n.CreatedDateTime);
                    break;
            }

            return q.ToList();
        }
    }

}