namespace Core.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public AppointmentDay AppointmentDay { get; set; }
    }
}
