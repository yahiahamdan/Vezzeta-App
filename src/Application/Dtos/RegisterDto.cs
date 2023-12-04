using Application.Validations;
using Core.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class RegisterDto : LoginDto
    {
        [Required, MinLength(3), MaxLength(64)]
        public string FirstName { get; set; }

        [Required, MinLength(3), MaxLength(64)]
        public string LastName { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required, EnumValidationAttribute(typeof(GendersEnum))]
        public GendersEnum Genders { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public IFormFile? Image { get; set; }
    }
}
