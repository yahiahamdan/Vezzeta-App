using Core.Enums;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntitiesConfiguration
{
    public class DiscountTypeConfiguration : IEntityTypeConfiguration<DiscountType>
    {
        public void Configure(EntityTypeBuilder<DiscountType> builder)
        {
            builder.Property(property => property.Name).HasMaxLength(9).HasConversion<string>();

            var discountTypes = Enum.GetNames(typeof(DiscountTypesEnum))
                .Select(discountType => $"'{discountType}'");
            var inClause = string.Join(", ", discountTypes);
            var checkConstraint = $"Name IN ({inClause})";

            builder.ToTable(
                prop => prop.HasCheckConstraint("CK_DiscountType_Name", checkConstraint)
            );
        }
    }
}
