using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Database.Data
{
    public static class AspNetUsersSeeding
    {
        public static async Task SeedAspNetUsersEntity(UserManager<ApplicationUser> userManager)
        {
            {
                await userManager.CreateAsync(
                    new ApplicationUser
                    {
                        FirstName = "Mahmoud",
                        LastName = "Serag",
                        Gender = "Male",
                        UserName = "Admin123@admin.com",
                        DateOfBirth = "18/03/1999",
                        Email = "Admin123@admin.com",
                        PasswordHash = "Admin123@",
                        EmailConfirmed = true,
                        PhoneNumber = "01064560413",
                    }
                );
            }
        }
    }
}
