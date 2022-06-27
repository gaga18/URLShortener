using Project.Core.Application.Interfaces;
using Project.Core.Application.Interfaces.Repositories;
using Project.Infrastructure.Persistence.Implementations.Repositories;
using System;
using URLShortener.Core.Application.Interfaces.Repositories;
using URLShortener.Infrastructure.Persistence.Implementations.Repositories;

namespace Project.Infrastructure.Persistence.Implementations
{
    internal class UnitOfWork : IUnitOfWork
    {
        private IUserRepository userRepository;
        private IUserRoleRepository userRoleRepository;
        private IRoleRepository roleRepository;
        private ILinkRepository linkRepository;
        private ILinkTrackingRepository linkTrackingRepository;

        private readonly DataContext context;
        public UnitOfWork(DataContext context) => this.context = context;


        public IUserRepository UserRepository => userRepository ??= new UserRepository(context);
        public IUserRoleRepository UserRoleRepository => userRoleRepository ??= new UserRoleRepository(context);
        public IRoleRepository RoleRepository => roleRepository ??= new RoleRepository(context);
        public ILinkRepository LinkRepository => linkRepository ??= new LinkRepository(context);
        public ILinkTrackingRepository LinkTrackingRepository => linkTrackingRepository ??= new LinkTrackingRepository(context);

        public int Complete()
        {
            return context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
    }
}
