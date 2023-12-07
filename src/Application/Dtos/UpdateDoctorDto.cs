using Application.Validations;
using Core.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class UpdateDoctorDto
    {
        [EmailAddress, Required, MinLength(3), MaxLength(64)]
        public string Email { get; set; }

        [Required, MinLength(3), MaxLength(64)]
        public string FirstName { get; set; }

        [Required, MinLength(3), MaxLength(64)]
        public string LastName { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string DateOfBirth { get; set; }

        [Required, RegularExpression(@"^01[0125]\d{8}$")]
        public string PhoneNumber { get; set; }

        [Required, EnumValidationAttribute(typeof(GendersEnum))]
        public string Gender { get; set; }

        [Required, EnumValidationAttribute(typeof(SpecializationsEnum))]
        public string Specialization { get; set; }

        public IFormFile Image { get; set; }
    }
}
