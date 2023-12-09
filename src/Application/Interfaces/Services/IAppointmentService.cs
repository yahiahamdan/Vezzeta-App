using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface IAppointmentService
    {
        public string CreateNewAppointment(AppointmentDto appointmentDto, string doctorId);
        public string UpdateAppointmentTimeById(
            UpdateAppointmentTimeDto updateAppointmentTimeDto,
            int appointmentTimeId,
            string doctorId
        );
        public string DeleteAppointmentTimeById(int appointmentTimeId, string doctorId);
    }
}
