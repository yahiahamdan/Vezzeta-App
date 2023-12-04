using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Database.Data
{
    internal static class AspNetRolesSeeding
    {
        public static List<IdentityRole> SeedAspNetRolesEntity()
        {
            List<IdentityRole> roles = new List<IdentityRole>();

            foreach (RolesEnum role in Enum.GetValues(typeof(RolesEnum)))
                roles.Add(new IdentityRole(role.ToString()));

            return roles;
        }
    }
}
