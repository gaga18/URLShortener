using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Core.Domain.Entities;
using Project.Infrastructure.Persistence.Seeds;

namespace Project.Infrastructure.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();

            // IgnoreQueryFilters
            builder.HasQueryFilter(x => !x.DateDeleted.HasValue);

            // Seed data
            builder.HasData(UserSeed.Users);
        }
    }
}
