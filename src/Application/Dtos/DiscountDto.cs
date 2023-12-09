using Application.Validations;
using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class DiscountDto
    {
        [Required, MinLength(8), MaxLength(8)]
        public string DiscountCode { get; set; }

        [Required, EnumValidationAttribute(typeof(DiscountTypesEnum))]
        public string DiscountType { get; set; }

        [Required]
        public int DiscountValue { get; set; }
    }
}
