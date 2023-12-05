using Core.Enums;
using Core.Models;
using Infrastructure.Database.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database.Data
{
    internal static class BookingStatusSeeding
    {
        public async static Task SeedBookingStatusEntity(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            foreach (BookingStatusEnum status in Enum.GetValues(typeof(BookingStatusEnum)))
                await context.BookingStatus.AddAsync(new BookingStatus() { Name = status });

            await context.SaveChangesAsync();
        }
    }
}
