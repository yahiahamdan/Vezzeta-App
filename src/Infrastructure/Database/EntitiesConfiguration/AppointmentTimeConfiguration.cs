using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntitiesConfiguration
{
    public class AppointmentTimeConfiguration : IEntityTypeConfiguration<AppointmentTime>
    {
        public void Configure(EntityTypeBuilder<AppointmentTime> builder)
        {
            builder
                .HasOne(appointmentTime => appointmentTime.Appointment)
                .WithMany(appointment => appointment.AppointmentTimes)
                .HasForeignKey(appointmentTime => appointmentTime.AppointmentId);

            builder
                .HasOne(appointmentTime => appointmentTime.Time)
                .WithMany(time => time.AppointmentTimes)
                .HasForeignKey(appointmentTime => appointmentTime.TimeId);
        }
    }
}
