using Application.Dtos;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Repositories
{
    public interface IDoctorRepository
    {
        public Task<int> GetCountOfDoctors(string roleName);
        public Task<object> GetDoctorById(string doctorId);
        public List<int> GetAppointmentsByDoctorId(string doctorId);
        public List<int> GetAppointmentTimeByAppointmentId(List<int> appointmentIds);
        public bool CheckUserDeletionEligibility(List<int> appointmentTimeIds);
        public Task DeleteSingleDoctor(ApplicationUser user);
        public Task<ApplicationUser> FindDoctorById(string doctorId);
        public Task<IdentityResult> CreateNewDoctor(
            ApplicationUser doctor,
            string password,
            string specialization
        );
        public Task<(IdentityResult, string)> UpdateDoctorById(
            ApplicationUser doctor,
            string specialization,
            string doctorId,
            string imageName
        );
        public Task<List<ReturnDoctorDto>> GetAllDoctors(int page, int limit, string searchQuery);
    }
}
