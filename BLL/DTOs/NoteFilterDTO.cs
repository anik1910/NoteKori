using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class NoteFilterDTO
    {
        public string? NoteName { get; set; }
        public string? CourseName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinReviews { get; set; }
        public string SortBy { get; set; }
        public bool Asc { get; set; } = false;
    }
}
