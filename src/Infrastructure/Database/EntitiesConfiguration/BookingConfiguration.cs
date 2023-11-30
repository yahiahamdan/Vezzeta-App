using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntitiesConfiguration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder
                .HasOne(booking => booking.BookingStatus)
                .WithMany(bookingStatus => bookingStatus.Bookings)
                .HasForeignKey(booking => booking.StatusId);

            builder
                .HasOne(booking => booking.Day)
                .WithMany(day => day.Bookings)
                .HasForeignKey(booking => booking.DayId);
        }
    }
}
