using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        List<Review> GetByNoteId(int noteId);
    }
}
