using Core.Models;
using Infrastructure.Database.Context;
using Infrastructure.Database.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database.Seeding
{
    public static class DatabaseSeeding
    {
        public async static Task SeedData(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            var roleManager = serviceScope.ServiceProvider.GetRequiredService<
                RoleManager<IdentityRole>
            >();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<
                UserManager<ApplicationUser>
            >();

            // Seed data
            await SeedAspNetRoles(roleManager);
            await SeedAspNetUsers(userManager);
            await SeedDaysEntity(applicationBuilder);
            await SeedBookingStatusEntity(applicationBuilder);
            await SeedSpecializationsEntity(applicationBuilder);
            await SeedDiscountTypesEntity(applicationBuilder);
        }

        public async static Task SeedAspNetRoles(RoleManager<IdentityRole> roleManager)
        {
            await AspNetRolesSeeding.SeedAspNetRolesEntity(roleManager);
        }

        public async static Task SeedAspNetUsers(UserManager<ApplicationUser> userManager)
        {
            await AspNetUsersSeeding.SeedAspNetUsersEntity(userManager);
        }

        public async static Task SeedDaysEntity(IApplicationBuilder applicationBuilder)
        {
            await DaysSeeding.SeedDaysEntity(applicationBuilder);
        }

        public async static Task SeedBookingStatusEntity(IApplicationBuilder applicationBuilder)
        {
            await BookingStatusSeeding.SeedBookingStatusEntity(applicationBuilder);
        }

        public async static Task SeedSpecializationsEntity(IApplicationBuilder applicationBuilder)
        {
            await SpecializationsSeeding.SeedSpecializationsEntity(applicationBuilder);
        }

        public async static Task SeedDiscountTypesEntity(IApplicationBuilder applicationBuilder)
        {
            await DiscountTypesSeeding.SeedDiscountTypesEntity(applicationBuilder);
        }
    }
}
