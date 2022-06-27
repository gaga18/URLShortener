using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLShortener.Core.Domain.Entities;

namespace URLShortener.Infrastructure.Persistence.Configurations
{
    internal class LinkTrackingConfiguration : IEntityTypeConfiguration<LinkTracking>
    {
        public void Configure(EntityTypeBuilder<LinkTracking> builder)
        {
            builder.ToTable("LinkTrackings");
        }
    }
}
