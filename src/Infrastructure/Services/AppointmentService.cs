using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Infrastructure.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }

        public string CreateNewAppointment(AppointmentDto appointmentDto, string doctorId)
        {
            try
            {
                return this.appointmentRepository.AddNewAppointmentTime(appointmentDto, doctorId);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateAppointmentTime(
            UpdateAppointmentTimeDto updateAppointmentTimeDto,
            int appointmentTimeId,
            string doctorId
        )
        {
            try
            {
                var result = this.appointmentRepository.UpdateAppointmentTimeById(
                    updateAppointmentTimeDto,
                    appointmentTimeId,
                    doctorId
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
