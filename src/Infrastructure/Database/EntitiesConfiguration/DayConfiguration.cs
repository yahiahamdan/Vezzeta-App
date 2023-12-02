using Core.Enums;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntitiesConfiguration
{
    public class DayConfiguration : IEntityTypeConfiguration<Day>
    {
        public void Configure(EntityTypeBuilder<Day> builder)
        {
            builder
                .Property(property => property.Name)
                .HasMaxLength(9)
                .HasConversion<string>()
                .IsRequired();

            var weekDays = Enum.GetNames(typeof(WeekDaysEnum)).Select(day => $"'{day}'");
            var inClause = string.Join(", ", weekDays);
            var checkConstraint = $"Name IN ({inClause})";

            builder.ToTable(prop => prop.HasCheckConstraint("CK_Day_Name", checkConstraint));
        }
    }
}
