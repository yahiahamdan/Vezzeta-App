using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntitiesConfiguration
{
    public class ExaminationPriceConfiguration : IEntityTypeConfiguration<ExaminationPrice>
    {
        public void Configure(EntityTypeBuilder<ExaminationPrice> builder)
        {
            builder.HasKey(e => e.DoctorId);

            builder
                .HasOne(e => e.Doctor)
                .WithOne(d => d.ExaminationPrice)
                .HasForeignKey<ExaminationPrice>(ex => ex.DoctorId);
        }
    }
}
