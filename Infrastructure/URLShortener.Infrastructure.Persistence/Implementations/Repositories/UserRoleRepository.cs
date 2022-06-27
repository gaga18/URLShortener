using Microsoft.EntityFrameworkCore;
using Project.Core.Application.Interfaces.Repositories;
using Project.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Infrastructure.Persistence.Implementations.Repositories
{
    internal class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(DataContext context) : base(context) { }
        private IQueryable<UserRole> Including =>
            this.context.UserRoles.Include(x => x.Role);

        public async Task<List<UserRole>> GetUserRolesAsync(int userId)
        {
            return await Including.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
