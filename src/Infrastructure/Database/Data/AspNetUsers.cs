using Core.Enums;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Database.Data
{
    public static class AspNetUsersSeeding
    {
        public static async Task SeedAspNetUsersEntity(UserManager<ApplicationUser> userManager)
        {
            string userPassword = "NewAdminPassword@123";
            ApplicationUser applicationUser = new ApplicationUser
            {
                FirstName = "Mahmoud",
                LastName = "Serag",
                Gender = "Male",
                UserName = "Admin123@admin.com",
                DateOfBirth = "18/03/1999",
                Email = "Admin123@admin.com",
                EmailConfirmed = true,
                PhoneNumber = "01064560413",
            };

            IdentityResult user = await userManager.CreateAsync(applicationUser, userPassword);

            if (user.Succeeded)
                await userManager.AddToRoleAsync(applicationUser, RolesEnum.Admin.ToString());
        }
    }
}
