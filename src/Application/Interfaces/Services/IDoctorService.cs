using Application.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Services
{
    public interface IDoctorService
    {
        public Task<int> GetCountOfDoctors(string roleName);
        public Task<object> GetDoctorById(string doctorId);
        public Task<IdentityResult> CreateNewDoctor(
            DoctorDto doctor,
            string password,
            string specialization
        );
        public Task<IdentityResult> UpdateDoctorById(
            UpdateDoctorDto doctorDto,
            string doctorId,
            string specialization
        );
        public Task<List<ReturnDoctorDto>> GetAllDoctors(int page, int limit, string searchQuery);
    }
}
