using Application.Dtos;

namespace Application.Interfaces.Repositories
{
    public interface IAppointmentRepository
    {
        public string AddTimes(AppointmentDto appointmentDto);
        public List<List<int>> GetTimesIds(AppointmentDto appointmentDto);
        public List<int> GetDaysIds(AppointmentDto appointmentDto);
        public List<int> AddNewAppointment(AppointmentDto appointmentDto, string doctorId);
        public string AddNewAppointmentTime(AppointmentDto appointmentDto, string doctorId);
        public string UpdateAppointmentTimeById(
            UpdateAppointmentTimeDto updateAppointmentTimeDto,
            int appointmentTimeId,
            string doctorId
        );
    }
}
