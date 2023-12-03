using Core.Enums;
using Core.Models;

namespace Infrastructure.Database.Data
{
    internal static class AspNetUsersSeeding
    {
        public static ApplicationUser SeedUsers()
        {
            return new ApplicationUser()
            {
                Id = "0d893228-14b9-446d-b727-442a0d353458",
                FirstName = "Mahmoud",
                LastName = "Serag",
                Gender = GendersEnum.Male,
                DateOfBirth = "18/03/1999",
                Email = "sragmahmoud4@gmail.com",
                PasswordHash = "Admin123",
                PhoneNumber = "+201064560413",
            };
        }
    }
}
