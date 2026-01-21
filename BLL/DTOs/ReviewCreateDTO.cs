using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class ReviewCreateDTO
    {
        [Range(1, 5)]
        public int Rating { get; set; }
        [StringLength(1000)]
        public string Comment { get; set; }
    }
}
