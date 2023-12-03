using Core.Enums;
using Core.Models;

namespace Infrastructure.Database.Data
{
    internal static class BookingStatusSeeding
    {
        public static List<BookingStatus> SeedBookingStatusEntity()
        {
            List<BookingStatus> bookingStatuses = [];

            int idCounter = 0;

            foreach (BookingStatusEnum status in Enum.GetValues(typeof(BookingStatusEnum)))
                bookingStatuses.Add(new BookingStatus() { Id = ++idCounter, Name = status });

            return bookingStatuses;
        }
    }
}
