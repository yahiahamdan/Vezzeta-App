using Application.Dtos;
using Application.Interfaces.Helpers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository patientRepository;
        private readonly IJwtHelpService jwtHelpService;

        public PatientService(IPatientRepository patientRepository, IJwtHelpService jwtHelpService)
        {
            this.jwtHelpService = jwtHelpService;
            this.patientRepository = patientRepository;
        }

        public async Task<int> GetCountOfPatients(string roleName)
        {
            return await this.patientRepository.GetCountOfPatients(roleName);
        }

        public async Task<object> GetPatientById(string patientId)
        {
            var patient = await this.patientRepository.GetPatientById(patientId);

            if (patient == null)
                return null;

            return patient;
        }

        public async Task<List<PatientDto>> GetAllPatients(
            int page,
            int size,
            string searchQuery,
            string roleName
        )
        {
            List<PatientDto> patients = await this.patientRepository.GetAllPatient(
                page,
                size,
                searchQuery,
                roleName
            );

            return patients;
        }
    }
}
