using Application.Dtos;
using Core.Models;

namespace Application.Interfaces.Repositories
{
    public interface IDiscountRepository
    {
        public string CreateNewDiscount(DiscountDto discountDto);
        public string UpdateDiscountCode(DiscountDto discountDto, int discountId);
        public string DeleteDiscountCode(int discountId);
        public string DeActivateDiscountCode(int discountId);
        public Booking GetBookingByDiscountId(int discountId);
    }
}
