using Microsoft.EntityFrameworkCore;
using Project.Core.Application.Commons;
using Project.Infrastructure.Persistence;
using Project.Infrastructure.Persistence.Implementations;
using System.Linq;
using System.Threading.Tasks;
using URLShortener.Core.Application.Interfaces.Repositories;
using URLShortener.Core.Domain.Entities;

namespace URLShortener.Infrastructure.Persistence.Implementations.Repositories
{
    internal class LinkRepository : Repository<LinkEntity>, ILinkRepository
    {
        public LinkRepository(DataContext context) : base(context) { }

        public async Task<Pagination<LinkEntity>> FilterAsync(int pageIndex, int pageSize, string originalUrl = null)
        {
            var links = context.Links.Where(x => (originalUrl == null || x.OriginUrl == originalUrl));

            return await Pagination<LinkEntity>.CreateAsync(links, pageIndex, pageSize);
        }

        public async Task<LinkEntity> getByOriginUrl(string originalUrl)
        {
            return await this.context.Links.FirstOrDefaultAsync(x => x.OriginUrl == originalUrl);
        }
        public async Task<LinkEntity> getByToken(string token)
        {
            return await this.context.Links.FirstOrDefaultAsync(x => x.FwToken == token);
        }
    }
}
