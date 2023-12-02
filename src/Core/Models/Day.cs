using Core.Enums;

namespace Core.Models
{
    public class Day
    {
        public int Id { get; set; }
        public WeekDaysEnum Name { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
