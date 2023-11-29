using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<BookingStatus> BookingStatus { get; set; }
        public DbSet<AppointmentDays> AppointmentDays { get; set; }
        public DbSet<AppointmentTimes> AppointmentTimes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
