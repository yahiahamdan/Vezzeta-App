using Core.Enums;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntitiesConfiguration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(property => property.FirstName).HasMaxLength(64);
            builder.Property(property => property.LastName).HasMaxLength(64);
            builder.Property(property => property.Email).HasMaxLength(64).IsRequired();
            builder.Property(property => property.PhoneNumber).HasMaxLength(13).IsRequired();
            builder.Property(property => property.PasswordHash).HasMaxLength(32).IsRequired();
            builder.Property(property => property.DateOfBirth).HasMaxLength(15);
            builder.Property(property => property.Gender).HasConversion<string>();

            builder
                .HasOne(doctor => doctor.Specialization)
                .WithMany(spec => spec.Doctors)
                .HasForeignKey(doctor => doctor.SpecializationId);

            var genders = Enum.GetNames(typeof(GendersEnum)).Select(status => $"'{status}'");
            var inClause = string.Join(", ", genders);
            var checkConstraint = $"Gender IN ({inClause})";

            builder.ToTable(
                prop => prop.HasCheckConstraint("CK_AspNetUser_Gender", checkConstraint)
            );

            builder.ToTable(
                prop =>
                    prop.HasCheckConstraint(
                        "CK_AspNetUser_Email",
                        "Email LIKE '%[a-zA-Z0-9.!#$%&''*+/=?^_`{|}~-]%@[a-zA-Z0-9-]%'"
                    )
            );
        }
    }
}
