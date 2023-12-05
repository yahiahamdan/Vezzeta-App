using Application.Interfaces.Helpers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using System.Security.Claims;

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

        public async Task<int> GetCountOfPatients(string token)
        {
            var decodedToken = this.jwtHelpService.DecodeToken(token);

            string roleName = decodedToken.Claims
                .First(claim => claim.Type == ClaimTypes.Role)
                .Value;

            return await this.patientRepository.GetCountOfPatients(roleName);
        }
    }
}
