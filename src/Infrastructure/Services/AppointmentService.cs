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
    }
}
