using Core.Enums;
using Core.Models;
using Infrastructure.Database.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database.Data
{
    public class SpecializationsSeeding
    {
        public static async Task SeedSpecializationsEntity(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            if (!context.Specializations.Any())
            {
                foreach (
                    SpecializationsEnum specialization in Enum.GetValues(
                        typeof(SpecializationsEnum)
                    )
                )
                    await context.Specializations.AddAsync(
                        new Specialization { Title = specialization.ToString() }
                    );

                await context.SaveChangesAsync();
            }
        }
    }
}
