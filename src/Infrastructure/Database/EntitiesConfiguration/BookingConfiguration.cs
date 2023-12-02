using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntitiesConfiguration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(property => property.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder
                .HasOne(booking => booking.BookingStatus)
                .WithMany(bookingStatus => bookingStatus.Bookings)
                .HasForeignKey(booking => booking.StatusId);

            builder
                .HasOne(booking => booking.Discount)
                .WithOne(discount => discount.Booking)
                .HasForeignKey<Booking>(booking => booking.DiscountId);

            builder
                .HasOne(booking => booking.AppointmentTime)
                .WithOne(appointmentTime => appointmentTime.Booking)
                .HasForeignKey<Booking>(booking => booking.AppointmentTimeId);

            builder
                .HasOne(booking => booking.Patient)
                .WithMany(patient => patient.Bookings)
                .HasForeignKey(booking => booking.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
