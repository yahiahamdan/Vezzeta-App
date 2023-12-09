using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class AppointmentDto
    {
        [Required]
        public int Price { get; set; }

        [Required]
        public List<DaysAndTimesDto> Appointments { get; set; }
    }
}
