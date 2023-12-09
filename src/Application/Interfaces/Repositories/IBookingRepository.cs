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
        public Appointment GetAppointmentById(int appointmentTimeId);
        public int GetDoctorExaminationPrice(string doctorId);
        public string AddNewBooking(
            string patientId,
            int appointmentTimeId,
            DiscountCodeCouponDto discountCodeCouponDto
        );
    }
}
