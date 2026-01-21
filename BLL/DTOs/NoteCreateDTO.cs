using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class NoteCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string NoteName { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string ShortDescription { get; set; }
    }
}
