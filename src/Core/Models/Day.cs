namespace Core.Models
{
    public class Day
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
