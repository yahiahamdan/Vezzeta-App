using Application.Interfaces.Repositories;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public PatientRepository(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<int> GetCountOfPatients(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (role != null)
            {
                var patientsTotalCount = await userManager.GetUsersInRoleAsync(roleName);
                Console.WriteLine(patientsTotalCount);

                if (patientsTotalCount != null)
                    return patientsTotalCount.Count;
                else
                    return 0;
            }

            return 0;
        }
    }
}
