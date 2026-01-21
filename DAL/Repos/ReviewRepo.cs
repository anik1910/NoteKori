using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class ReviewRepo : IReviewRepository
    {
        NMSContext db;
        public ReviewRepo(NMSContext db)
        {
            this.db = db;
        }

        public bool Create(Review entity)
        {
            db.Reviews.Add(entity);
            return db.SaveChanges() > 0;
        }

        public List<Review> Get()
        {
            return db.Reviews.ToList();
        }

        public Review Get(int id)
        {
            return db.Reviews.Find(id);
        }

        public List<Review> GetByNoteId(int noteId)
        {
            return db.Reviews.Where(r => r.NoteId == noteId).OrderByDescending(r => r.CreatedAt).ToList();
        }
        public bool Update(Review entity)
        {
            db.Reviews.Update(entity);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var review = db.Reviews.Find(id);
            if (review == null)
            {
                return false;
            }
            db.Reviews.Remove(review);
            return db.SaveChanges() > 0;
        }
    }
}
