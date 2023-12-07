using Application.Dtos;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Repositories
{
    public interface IDoctorRepository
    {
        public Task<int> GetCountOfDoctors(string roleName);
        public Task<object> GetDoctorById(string doctorId);
        public Task<IdentityResult> CreateNewDoctor(
            ApplicationUser doctor,
            string password,
            string specialization
        );
        public Task<IdentityResult> DeleteSingleDoctor(ApplicationUser user);
        public Task<(IdentityResult, string)> UpdateDoctorById(
            ApplicationUser doctor,
            string specialization,
            string doctorId,
            string imageName
        );
        public Task<List<ReturnDoctorDto>> GetAllDoctors(int page, int limit, string searchQuery);
    }
}
