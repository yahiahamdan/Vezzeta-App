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

        public string ConfirmBooking(string doctorId, int bookingId)
        {
            try
            {
                var booking = this.bookingRepository.GetBookingById(bookingId);

                if (booking == null)
                    return "Booking not found with the given Id";

                var appointment = this.bookingRepository.GetAppointmentById(
                    booking.AppointmentTimeId
                );

                if (appointment == null || appointment.DoctorId != doctorId)
                    return "Forbidden. You can't access this content";

                var bookingStatus = this.bookingRepository.GetBookingStatusById(booking.StatusId);

                if (
                    bookingStatus.Name.ToString() == "Completed"
                    || bookingStatus.Name.ToString() == "Cancelled"
                )
                    return "Forbidden. The booking status has been officially confirmed or cancelled";

                var userBookingTracking = this.bookingRepository.GetUserBookingTracking(
                    booking.PatientId
                );

                if (userBookingTracking == null)
                    this.bookingRepository.AddNewBookingTracking(booking.PatientId);
                else
                    this.bookingRepository.UpdateUserBookingTracking(userBookingTracking);

                var appointmentTime = this.bookingRepository.GetAppointmentTimeById(
                    booking.AppointmentTimeId
                );

                this.bookingRepository.UpdateAppointmentTime(appointmentTime);

                var completedStatusId = this.bookingRepository.GetBookingStatusId("Completed");

                this.bookingRepository.ConfirmBooking(booking, completedStatusId);

                return "Succeeded";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
