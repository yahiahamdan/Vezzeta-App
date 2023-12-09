using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface IDiscountService
    {
        public string CreateNewDiscount(DiscountDto discountDto);
    }
}
