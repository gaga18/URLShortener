using Project.Core.Application.Commons;
using Project.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Project.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<int, User>
    {
        Task<User> GetUserByUserName(string username);
        Task<Pagination<User>> FilterAsync(int pageIndex, int pageSize, string FirstName = null, string LastName = null, string Username = null);
    }
}
