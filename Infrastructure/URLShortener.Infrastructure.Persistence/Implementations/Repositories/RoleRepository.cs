using Project.Infrastructure.Persistence;
using Project.Infrastructure.Persistence.Implementations;
using URLShortener.Core.Application.Interfaces.Repositories;
using URLShortener.Core.Domain.Entities;

namespace URLShortener.Infrastructure.Persistence.Implementations.Repositories
{
    internal class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DataContext context) : base(context) { }
    }
}
