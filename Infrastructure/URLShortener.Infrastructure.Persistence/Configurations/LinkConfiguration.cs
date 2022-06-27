using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLShortener.Core.Domain.Entities;

namespace URLShortener.Infrastructure.Persistence.Configurations
{
    internal class LinkConfiguration : IEntityTypeConfiguration<LinkEntity>
    {
        public void Configure(EntityTypeBuilder<LinkEntity> builder)
        {
            builder.ToTable("Links");
            builder.HasQueryFilter(x => !x.DateDeleted.HasValue);
        }
    }
}
