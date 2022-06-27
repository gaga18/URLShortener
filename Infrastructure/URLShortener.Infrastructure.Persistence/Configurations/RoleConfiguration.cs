using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLShortener.Core.Domain.Entities;
using URLShortener.Infrastructure.Persistence.Seeds;

namespace URLShortener.Infrastructure.Persistence.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            // Seed data
            builder.HasData(RoleSeed.Roles);
        }
    }
}
