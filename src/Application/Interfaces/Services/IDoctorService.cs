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
    }
}
