using Core.Enums;

namespace Core.Models
{
    public class BookingStatus
    {
        public int Id { get; set; }
        public BookingStatusEnum Name { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
