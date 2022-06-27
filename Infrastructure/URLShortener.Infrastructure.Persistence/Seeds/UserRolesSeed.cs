using Project.Core.Domain.Entities;

namespace Project.Infrastructure.Persistence.Seeds
{
    internal static class UserRolesSeed
    {
        internal static readonly UserRole[] UserRoles = new UserRole[]
        {
                new UserRole { Id = 1, UserId = 1, RoleId = 1},
                new UserRole { Id = 2, UserId = 2, RoleId = 2},
        };
    }
}
