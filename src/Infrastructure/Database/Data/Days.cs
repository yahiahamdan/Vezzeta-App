using Core.Enums;
using Core.Models;
using Infrastructure.Database.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database.Data
{
    public static class DaysSeeding
    {
        public static async Task SeedDaysEntity(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            if (!context.Days.Any())
            {
                foreach (WeekDaysEnum day in Enum.GetValues(typeof(WeekDaysEnum)))
                    await context.Days.AddAsync(new Day { Name = day });

                await context.SaveChangesAsync();
            }
        }
    }
}
