using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntitiesConfiguration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder
                .HasOne(appointment => appointment.AppointmentDay)
                .WithMany(appointmentDay => appointmentDay.Appointments)
                .HasForeignKey(appointment => appointment.DayId);
        }
    }
}
