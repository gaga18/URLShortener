using Project.Core.Application.Commons;
using Project.Core.Application.Interfaces;
using System.Threading.Tasks;
using URLShortener.Core.Domain.Entities;

namespace URLShortener.Core.Application.Interfaces.Repositories
{
    public interface ILinkRepository : IRepository<int, LinkEntity>
    {
        Task<LinkEntity> getByOriginUrl(string originalUrl);
        Task<LinkEntity> getByToken(string token);
        Task<Pagination<LinkEntity>> FilterAsync(int pageIndex, int pageSize, string originalUrl = null);
    }
}
