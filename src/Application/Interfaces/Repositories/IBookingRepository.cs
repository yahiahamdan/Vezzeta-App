using Application.Dtos;
using Core.Models;

namespace Application.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        public bool CheckDiscountEligibility(string patientId);
        public int GetBookingStatusId(string bookingStatusName);
        public Discount GetDiscountByCodeName(DiscountCodeCouponDto discountCodeCouponDto);
        public AppointmentTime GetAppointmentTimeById(int appointmentTimeId);
        public UserBookingTracking GetUserBookingTracking(string patientId);
        public void AddNewBookingTracking(string patientId);
        public void UpdateUserBookingTracking(UserBookingTracking userBookingTracking);
        public void UpdateAppointmentTime(AppointmentTime appointmentTime);
        public Appointment GetAppointmentById(int appointmentTimeId);
        public int GetDoctorExaminationPrice(string doctorId);
        public void UpdateBookingStatus(Booking booking, int statusId);
        public BookingStatus GetBookingStatusById(int bookingStatusId);
        public Booking GetBookingById(int bookingId);
        public string AddNewBooking(
            string patientId,
            int appointmentTimeId,
            DiscountCodeCouponDto discountCodeCouponDto
        );
    }
}
