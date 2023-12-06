using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Infrastructure.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        public async Task<int> GetCountOfDoctors(string roleName)
        {
            int totalDoctorsCount = await this.doctorRepository.GetCountOfDoctors(roleName);

            if (totalDoctorsCount == 0)
                return 0;

            return totalDoctorsCount;
        }

        public async Task<object> GetDoctorById(string doctorId)
        {
            var totalDoctorsCount = await this.doctorRepository.GetDoctorById(doctorId);

            if (totalDoctorsCount == null)
                return null;

            return totalDoctorsCount;
        }
    }
}
