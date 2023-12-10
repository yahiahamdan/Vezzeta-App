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
        public bool IsAppointmentTimeBooked(int appointmentTimeId);
        public void UpdateUserBookingTracking(UserBookingTracking userBookingTracking);
        public object GetAllBookingsForDoctor(string doctorId, int page, int limit);
        public void UpdateAppointmentTime(AppointmentTime appointmentTime);
        public Appointment GetAppointmentById(int appointmentTimeId);
        public int GetDoctorExaminationPrice(string doctorId);
        public List<int> GetCountOfBookings();
        public void UpdateBookingStatus(Booking booking, int statusId);
        public int GetTotalBookingsCount();
        public object GetAllBookingsForPatient(string patientId, int page, int limit);
        public BookingStatus GetBookingStatusById(int bookingStatusId);
        public Booking GetBookingById(int bookingId);
        public string AddNewBooking(
            string patientId,
            int appointmentTimeId,
            DiscountCodeCouponDto discountCodeCouponDto
        );
    }
}
