using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MinLength(3)]
        public string FirstName { get; set; }

        [MinLength(3)]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string? Image { get; set; }
        public ExaminationPrice ExaminationPrice { get; set; }
        public UserBookingTracking UserBookingTracking { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public Specialization Specialization { get; set; }
        public int? SpecializationId { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
