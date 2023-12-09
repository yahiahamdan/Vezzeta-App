using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface IBookingService
    {
        public string CreateNewBooking(
            string patientId,
            int appointmentTimeId,
            DiscountCodeCouponDto discountCodeCouponDto
        );
    }
}
