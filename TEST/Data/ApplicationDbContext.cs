using System;
using Microsoft.EntityFrameworkCore;
using TEST.Models;

namespace TEST.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 

        }

        public DbSet<Category> Categories { get; set; }
    }
}
