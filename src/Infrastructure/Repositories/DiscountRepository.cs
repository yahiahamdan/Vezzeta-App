using Application.Dtos;
using Application.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Database.Context;

namespace Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDbContext context;

        public DiscountRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public string CreateNewDiscount(DiscountDto discountDto)
        {
            try
            {
                int discountTypeId = context.DiscountTypes
                    .Where(dis => dis.Name == discountDto.DiscountType)
                    .Select(dis => dis.Id)
                    .FirstOrDefault();

                if (discountTypeId == 0)
                    return "No discount type (Percentage / Value) found to create new discount";

                var discount = this.context.Discounts.Add(
                    new Discount
                    {
                        DiscountCode = discountDto.DiscountCode,
                        IsActivated = true,
                        DiscountValue = discountDto.DiscountValue,
                        DiscountTypeId = discountTypeId
                    }
                );

                this.context.SaveChanges();

                return "Succeeded";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Booking GetBookingByDiscountId(int discountId)
        {
            var booking = this.context.Bookings
                .Where(booking => booking.DiscountId == discountId)
                .FirstOrDefault();

            return booking;
        }

        public string UpdateDiscountCode(DiscountDto discountDto, int discountId)
        {
            try
            {
                var discount = context.Discounts
                    .Where(dis => dis.Id == discountId)
                    .FirstOrDefault();

                if (discount == null)
                    return "No discount found with the given Id";

                int discountTypeId = context.DiscountTypes
                    .Where(dis => dis.Name == discountDto.DiscountType)
                    .Select(dis => dis.Id)
                    .FirstOrDefault();

                discount.DiscountValue = discountDto.DiscountValue;
                discount.DiscountCode = discountDto.DiscountCode;
                discount.DiscountTypeId = discountTypeId;

                this.context.SaveChanges();

                return "Succeeded";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteDiscountCode(int discountId)
        {
            try
            {
                var discount = context.Discounts
                    .Where(dis => dis.Id == discountId)
                    .FirstOrDefault();

                if (discount == null)
                    return "No discount found with the given Id";

                this.context.Discounts.Remove(discount);
                this.context.SaveChanges();

                return "Succeeded";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeActivateDiscountCode(int discountId)
        {
            try
            {
                var discount = context.Discounts
                    .Where(dis => dis.Id == discountId)
                    .FirstOrDefault();

                if (discount == null)
                    return "No discount found with the given Id";

                discount.IsActivated = false;
                this.context.SaveChanges();

                return "Succeeded";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
