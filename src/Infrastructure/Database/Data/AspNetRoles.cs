using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Database.Data
{
    public static class AspNetRolesSeeding
    {
        public static async Task SeedAspNetRolesEntity(RoleManager<IdentityRole> roleManager)
        {
            foreach (RolesEnum role in Enum.GetValues(typeof(RolesEnum)))
            {
                if (await roleManager.FindByNameAsync(role.ToString()) == null)
                    await roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
            }
        }
    }
}
