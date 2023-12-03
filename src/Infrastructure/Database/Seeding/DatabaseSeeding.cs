using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Seeding
{
    public class DatabaseSeeding<T> : IEntityTypeConfiguration<T>
        where T : class
    {
        private readonly List<T> entities;
        private readonly ApplicationUser user;

        public DatabaseSeeding(ApplicationUser user)
        {
            this.user = user;
        }

        public DatabaseSeeding(List<T> entities) => this.entities = entities;

        public void Configure(EntityTypeBuilder<T> builder)
        {
            if (typeof(T) != typeof(ApplicationUser))
            {
                for (int i = 0; i < entities.Count; i++)
                    builder.HasData(entities[i]);

                return;
            }

            builder.HasData(user);
        }
    }
}
