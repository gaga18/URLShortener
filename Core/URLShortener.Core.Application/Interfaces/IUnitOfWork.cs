using Project.Core.Application.Interfaces.Repositories;
using URLShortener.Core.Application.Interfaces.Repositories;

namespace Project.Core.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public ILinkRepository LinkRepository { get; }
        public ILinkTrackingRepository LinkTrackingRepository { get; }
        int Complete();
    }
}
