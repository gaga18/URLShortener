using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Persistence;
using Project.Infrastructure.Persistence.Implementations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortener.Core.Application.Interfaces.Repositories;
using URLShortener.Core.Domain.Entities;

namespace URLShortener.Infrastructure.Persistence.Implementations.Repositories
{
    internal class LinkTrackingRepository : Repository<LinkTracking>, ILinkTrackingRepository
    {
        public LinkTrackingRepository(DataContext context) : base(context) { }
        private IQueryable<LinkTracking> Including =>
            this.context.LinkTracking.Include(x => x.Link);

        public async Task<IEnumerable<LinkTracking>> GetAllWithLinks() => await Including.ToListAsync();
    }
}
