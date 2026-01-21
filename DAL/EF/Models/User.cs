using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.EF.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string Name { get; set; }
        public string Email { get; set; }
        [StringLength(12)]
        [Column(TypeName = "VARCHAR")]
        public string PhoneNumber { get; set; }
        [StringLength(200)]
        [Column(TypeName = "VARCHAR")]
        public string PasswordHash { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; } = 0m;
        public List<Note> Notes { get; set; }
    }
}
