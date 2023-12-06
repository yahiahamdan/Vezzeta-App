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
    }
}
