using Application.Interfaces.Repositories;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public DoctorRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<int> GetCountOfDoctors(string roleName)
        {
            IList<ApplicationUser> doctors = await this.userManager.GetUsersInRoleAsync(roleName);

            return doctors.Count;
        }

        public async Task<object> GetDoctorById(string doctorId)
        {
            ApplicationUser doctor = await this.userManager.FindByIdAsync(doctorId);

            return new
            {
                email = doctor.Email,
                firstName = doctor.FirstName,
                lastName = doctor.LastName,
                image = doctor.Image,
                phoneNumber = doctor.PhoneNumber,
                gender = doctor.Gender,
                dateOfBirth = doctor.DateOfBirth,
                Specialization = doctor.Specialization,
            };
        }
    }
}
