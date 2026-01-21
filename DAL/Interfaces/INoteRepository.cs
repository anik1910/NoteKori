using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface INoteRepository : IRepository<Note>
    {
        List<Note> GetByUserId(int userId);
        List<Note> GetFiltered(string noteName, string courseName, decimal? minPrice, decimal? maxPrice, int? minReviews, string sortBy, bool asc);
    }
}
