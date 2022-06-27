using Microsoft.EntityFrameworkCore;
using Project.Core.Application.Commons;
using Project.Core.Application.Interfaces.Repositories;
using Project.Core.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Infrastructure.Persistence.Implementations.Repositories
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }

        private IQueryable<User> Including =>
            this.context.Users.Include(x => x.UserRoles);

        public async Task<User> GetUserByUserName(string username)
        {
            return await this.Including.FirstOrDefaultAsync(x => x.UserName == username.Trim().ToLower());
        }

        public override async Task<User> Get(int id)
        {
            return await this.Including.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Pagination<User>> FilterAsync(int pageIndex, int pageSize, string firstName = null, string lastName = null, string username = null)
        {
            var users = this.Including.Where(x =>
                (firstName == null || x.FirstName == firstName) &&
                (lastName == null || x.LastName == lastName) &&
                (username == null || x.UserName == username)
            );

            return await Pagination<User>.CreateAsync(users, pageIndex, pageSize);
        }
    }
}
