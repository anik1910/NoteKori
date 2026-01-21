using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.EF.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        [StringLength(100)]
        [Column(TypeName = "VARCHAR(100)")]
        public string NoteName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "VARCHAR(100)")]
        public string CourseName { get; set; }

        public decimal Price { get; set; }

        [StringLength(500)]
        [Column(TypeName = "VARCHAR(500)")]
        public string ShortDescription { get; set; }

        [StringLength(50)]
        [Column(TypeName = "VARCHAR(50)")]
        public string UserName { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public int TotalReviews { get; set; }

        public int TotalDownloads { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
