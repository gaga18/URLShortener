using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Core.Domain.Entities;
using Project.Infrastructure.Persistence.Seeds;

namespace Project.Infrastructure.Persistence.Configurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");

            builder.HasQueryFilter(x => !x.DateDeleted.HasValue);

            // Seed data
            builder.HasData(UserRolesSeed.UserRoles);
        }
    }
}
