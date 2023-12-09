using Core.Enums;
using Core.Models;
using Infrastructure.Database.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database.Data
{
    public static class DiscountTypesSeeding
    {
        public static async Task SeedDiscountTypesEntity(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            if (!context.DiscountTypes.Any())
            {
                foreach (
                    DiscountTypesEnum discountType in Enum.GetValues(typeof(DiscountTypesEnum))
                )
                    await context.DiscountTypes.AddAsync(
                        new DiscountType { Name = discountType.ToString() }
                    );

                await context.SaveChangesAsync();
            }
        }
    }
}
