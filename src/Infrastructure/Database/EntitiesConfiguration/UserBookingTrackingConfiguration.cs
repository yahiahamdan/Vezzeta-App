using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntitiesConfiguration
{
    public class UserBookingTrackingConfiguration : IEntityTypeConfiguration<UserBookingTracking>
    {
        public void Configure(EntityTypeBuilder<UserBookingTracking> builder)
        {
            builder.HasKey(userBookingTracking => userBookingTracking.PatientId);

            builder
                .HasOne(userBookingTracking => userBookingTracking.Patient)
                .WithOne(patient => patient.UserBookingTracking)
                .HasForeignKey<UserBookingTracking>(
                    userBookingTracking => userBookingTracking.PatientId
                );
        }
    }
}
