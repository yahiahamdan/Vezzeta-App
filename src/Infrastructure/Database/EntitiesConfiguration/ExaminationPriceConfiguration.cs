using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntitiesConfiguration
{
    public class ExaminationPriceConfiguration : IEntityTypeConfiguration<ExaminationPrice>
    {
        public void Configure(EntityTypeBuilder<ExaminationPrice> builder)
        {
            builder.HasKey(examinationPrice => examinationPrice.DoctorId);

            builder
                .HasOne(examinationPrice => examinationPrice.Doctor)
                .WithOne(doctor => doctor.ExaminationPrice)
                .HasForeignKey<ExaminationPrice>(examinationPrice => examinationPrice.DoctorId);
        }
    }
}
