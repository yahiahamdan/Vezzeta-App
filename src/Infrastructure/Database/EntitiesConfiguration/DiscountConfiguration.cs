using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntitiesConfiguration
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasIndex(property => property.DiscountCode).IsUnique();

            builder
                .HasOne(discount => discount.DiscountType)
                .WithMany(discountType => discountType.Discounts)
                .HasForeignKey(discount => discount.DiscountTypeId);
        }
    }
}
