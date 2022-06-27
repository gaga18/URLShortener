using Project.Core.Application.Commons;
using Project.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Core.Application.Interfaces.Repositories
{
    public interface IUserRoleRepository : IRepository<int, UserRole>
    {
        Task<List<UserRole>> GetUserRolesAsync(int userId);

    }
}
