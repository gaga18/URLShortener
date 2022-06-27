using Project.Core.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using URLShortener.Core.Domain.Entities;

namespace URLShortener.Core.Application.Interfaces.Repositories
{
    public interface ILinkTrackingRepository : IRepository<int, LinkTracking>
    {
        Task<IEnumerable<LinkTracking>> GetAllWithLinks(); 
    }
}
