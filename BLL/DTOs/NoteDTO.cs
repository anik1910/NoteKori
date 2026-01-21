using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class NoteDTO
    {
        public int NoteId { get; set; }

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

        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        public int UserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
