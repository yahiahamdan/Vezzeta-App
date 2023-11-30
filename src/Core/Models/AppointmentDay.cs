namespace Core.Models
{
    public class AppointmentDay
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
