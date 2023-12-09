using Application.Validations;
using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class DaysAndTimesDto
    {
        [Required, EnumValidationAttribute(typeof(WeekDaysEnum))]
        public string Day { get; set; }

        [Required]
        public List<string> Times { get; set; }
    }
}
