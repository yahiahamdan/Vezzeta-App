using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        public string CreateNewBooking(
            string patientId,
            int appointmentTimeId,
            DiscountCodeCouponDto discountCodeCouponDto
        )
        {
            try
            {
                var result = bookingRepository.AddNewBooking(
                    patientId,
                    appointmentTimeId,
                    discountCodeCouponDto
                );
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
