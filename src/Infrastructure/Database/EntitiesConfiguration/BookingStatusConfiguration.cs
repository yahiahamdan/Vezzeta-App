using Core.Enums;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntitiesConfiguration
{
    public class BookingStatusConfiguration : IEntityTypeConfiguration<BookingStatus>
    {
        public void Configure(EntityTypeBuilder<BookingStatus> builder)
        {
            builder
                .Property(property => property.Name)
                .HasMaxLength(9)
                .HasConversion<string>()
                .IsRequired();

            var bookStatus = Enum.GetNames(typeof(BookingStatusEnum))
                .Select(status => $"'{status}'");
            var inClause = string.Join(", ", bookStatus);
            var checkConstraint = $"Name IN ({inClause})";

            builder.ToTable(
                prop => prop.HasCheckConstraint("CK_BookingStatus_Name", checkConstraint)
            );
        }
    }
}
