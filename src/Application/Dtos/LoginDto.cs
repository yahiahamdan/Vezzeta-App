using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class LoginDto
    {
        [EmailAddress, Required, MinLength(3), MaxLength(64)]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(32), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
