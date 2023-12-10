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
        public string ConfirmBooking(string doctorId, int bookingId);
        public string CancelBooking(string doctorId, int bookingId);
        public object GetCountOfBookings();
    }
}
