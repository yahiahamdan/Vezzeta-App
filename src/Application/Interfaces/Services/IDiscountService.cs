using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface IDiscountService
    {
        public string CreateNewDiscount(DiscountDto discountDto);
        public string UpdateDiscountCode(DiscountDto discountDto, int discountId);
        public string DeleteDiscount(int discountId);
        public string DeActivateDiscount(int discountId);
    }
}
