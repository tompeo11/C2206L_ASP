using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TEST.Models;

namespace TEST.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Vaccine> Vaccines { get; set;}
        public DbSet<VaccineType> VaccinesTypes { get; set;}
        public DbSet<VaccineSchedule> VaccineSchedules { get;set; }
    }
}
