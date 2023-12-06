using Application.Interfaces.Repositories;
using Core.Enums;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AuthRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> Register(ApplicationUser user, string password)
        {
            user.UserName = user.Email;
            var result = await this.userManager.CreateAsync(user, password);

            try
            {
                await this.userManager.AddToRoleAsync(user, RolesEnum.Patient.ToString());

                return result;
            }
            catch (Exception ex)
            {
                if (result.Succeeded)
                    await this.userManager.DeleteAsync(user);

                return result;
            }
        }

        public async Task<(ApplicationUser user, IList<string> roles)> Login(
            string email,
            string password
        )
        {
            var user = await this.userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var isCorrectPassword = await this.userManager.CheckPasswordAsync(user, password);
                if (isCorrectPassword)
                {
                    var userRole = await this.userManager.GetRolesAsync(user);

                    return (user, userRole);
                }
            }

            return (null, null);
        }
    }
}
