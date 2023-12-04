// Ignore Spelling: Trackings

using Core.Models;
using Infrastructure.Database.Data;
using Infrastructure.Database.EntitiesConfiguration;
using Infrastructure.Database.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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

        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IApplicationBuilder applicationBuilder;
            modelBuilder.ApplyConfiguration<ExaminationPrice>(new ExaminationPriceConfiguration());
            modelBuilder.ApplyConfiguration<Booking>(new BookingConfiguration());
            modelBuilder.ApplyConfiguration<Appointment>(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration<AppointmentTime>(new AppointmentTimeConfiguration());
            modelBuilder.ApplyConfiguration<Discount>(new DiscountConfiguration());
            modelBuilder.ApplyConfiguration<DiscountType>(new DiscountTypeConfiguration());
            modelBuilder.ApplyConfiguration<ApplicationUser>(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration<BookingStatus>(new BookingStatusConfiguration());
            modelBuilder.ApplyConfiguration<Specialization>(new SpecializationConfiguration());
            modelBuilder.ApplyConfiguration<Time>(new TimeConfiguration());
            modelBuilder.ApplyConfiguration<Day>(new DayConfiguration());
            modelBuilder.ApplyConfiguration<UserBookingTracking>(
                new UserBookingTrackingConfiguration()
            );

            // Database Seeding
            modelBuilder.ApplyConfiguration<BookingStatus>(
                new DatabaseSeeding<BookingStatus>(BookingStatusSeeding.SeedBookingStatusEntity())
            );
            modelBuilder.ApplyConfiguration<Day>(
                new DatabaseSeeding<Day>(DaysSeeding.SeedDaysEntity())
            );
            modelBuilder.ApplyConfiguration<ApplicationUser>(
                new DatabaseSeeding<ApplicationUser>(AspNetUsersSeeding.SeedAspNetUsersEntity())
            );
            modelBuilder.ApplyConfiguration<IdentityRole>(
                new DatabaseSeeding<IdentityRole>(AspNetRolesSeeding.SeedAspNetRolesEntity())
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
