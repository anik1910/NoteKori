using DAL.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF
{
    public class NMSContext : DbContext
    {
        public NMSContext(DbContextOptions<NMSContext> opt) : base(opt) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
