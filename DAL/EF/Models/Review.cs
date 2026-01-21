using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.EF.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        public int NoteId { get; set; }

        public int UserId { get; set; }

        [StringLength(50)]
        [Column(TypeName = "VARCHAR(50)")]
        public string UserName { get; set; }
        public int? Rating { get; set; }

        [StringLength(1000)]
        [Column(TypeName = "VARCHAR(1000)")]
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
