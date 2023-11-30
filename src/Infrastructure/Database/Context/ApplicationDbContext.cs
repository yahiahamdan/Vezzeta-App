using Core.Models;
using Infrastructure.Database.EntitiesConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<BookingStatus> BookingStatus { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<AppointmentTime> AppointmentTimes { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountType> DiscountTypes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ExaminationPrice> ExaminationPrices { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<UserBookingTracking> UserBookingTrackings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<ExaminationPrice>(new ExaminationPriceConfiguration());
            modelBuilder.ApplyConfiguration<Booking>(new BookingConfiguration());
            modelBuilder.ApplyConfiguration<Appointment>(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration<AppointmentTime>(new AppointmentTimeConfiguration());
            modelBuilder.ApplyConfiguration<UserBookingTracking>(
                new UserBookingTrackingConfiguration()
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
