using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface IAppointmentService
    {
        public string CreateNewAppointment(AppointmentDto appointmentDto, string doctorId);
        public string UpdateAppointmentTime(
            UpdateAppointmentTimeDto updateAppointmentTimeDto,
            int appointmentTimeId,
            string doctorId
        );
    }
}
