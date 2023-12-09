using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Core.Enums;

namespace Infrastructure.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository discountRepository;

        public DiscountService(IDiscountRepository discountRepository)
        {
            this.discountRepository = discountRepository;
        }

        public string CreateNewDiscount(DiscountDto discountDto)
        {
            try
            {
                if (
                    discountDto.DiscountType == DiscountTypesEnum.Percentage.ToString()
                    && discountDto.DiscountValue == 100
                )
                    return "Discount value can't be 100%";

                var result = this.discountRepository.CreateNewDiscount(discountDto);

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateDiscountCode(DiscountDto discountDto, int discountId)
        {
            try
            {
                if (
                    discountDto.DiscountType == DiscountTypesEnum.Percentage.ToString()
                    && discountDto.DiscountValue == 100
                )
                    return "Discount value can't be 100%";

                var result = this.discountRepository.UpdateDiscountCode(discountDto, discountId);

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteDiscount(int discountId)
        {
            try
            {
                var result = this.discountRepository.DeleteDiscountCode(discountId);

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
