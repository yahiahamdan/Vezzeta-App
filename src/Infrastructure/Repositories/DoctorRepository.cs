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
    }
}
