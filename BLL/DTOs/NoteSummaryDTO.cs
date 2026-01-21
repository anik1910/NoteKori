using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class NoteSummaryDTO
    {
        public string NoteName { get; set; }
        public string CourseName { get; set; }
        public string CreatedBy { get; set; }      
        public string UploadedDate { get; set; }   
        public decimal Price { get; set; }
        public int TotalReviews { get; set; }
        public int TotalDownloads { get; set; }
    }
}
