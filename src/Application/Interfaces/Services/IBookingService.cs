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
        public (object, int) GetAllBookingsForPatient(string patientId, int page, int limit);
        public (object, int) GetAllBookingsForDoctor(string patientId, int page, int limit);
    }
}
