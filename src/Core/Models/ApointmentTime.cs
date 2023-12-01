namespace Core.Models
{
    public class AppointmentTime
    {
        public int Id { get; set; }
        public bool IsBooked { get; set; }
        public Appointment Appointment { get; set; }
        public int AppointmentId { get; set; }
        public Time Time { get; set; }
        public int TimeId { get; set; }
        public Booking Booking { get; set; }
    }
}
