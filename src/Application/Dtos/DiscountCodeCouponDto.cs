using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class DiscountCodeCouponDto
    {
        [MaxLength(8), MinLength(8)]
        public string? DiscountCodeCoupon { get; set; }
    }
}
