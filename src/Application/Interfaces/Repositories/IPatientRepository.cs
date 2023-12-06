using Application.Dtos;

namespace Application.Interfaces.Repositories
{
    public interface IPatientRepository
    {
        public Task<int> GetCountOfPatients(string roleId);
        public Task<object> GetPatientById(string patientId);
        public Task<List<PatientDto>> GetAllPatient(
            int page,
            int size,
            string searchQuery,
            string roleName
        );
    }
}
