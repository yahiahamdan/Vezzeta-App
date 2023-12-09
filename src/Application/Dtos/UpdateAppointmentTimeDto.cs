using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class UpdateAppointmentTimeDto
    {
        [Required]
        public string Time { get; set; }
    }
}
