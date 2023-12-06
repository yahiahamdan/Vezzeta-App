using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface IPatientService
    {
        public Task<int> GetCountOfPatients(string token);
        public Task<object> GetPatientById(string patientId);
        public Task<List<PatientDto>> GetAllPatients(
            int page,
            int size,
            string searchQuery,
            string roleName
        );
    }
}
