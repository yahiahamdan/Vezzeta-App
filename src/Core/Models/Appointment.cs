namespace Core.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public Day Day { get; set; }
        public int DayId { get; set; }
        public ApplicationUser Doctor { get; set; }
        public string DoctorId { get; set; }
    }
}
