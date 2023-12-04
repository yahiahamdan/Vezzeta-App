using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Seeding
{
    public class DatabaseSeeding<T> : IEntityTypeConfiguration<T>
        where T : class
    {
        private readonly List<T> entities;

        public DatabaseSeeding(List<T> entities) => this.entities = entities;

        public void Configure(EntityTypeBuilder<T> builder)
        {
            for (int i = 0; i < entities.Count; i++)
                builder.HasData(entities[i]);

            return;
        }
    }
}
