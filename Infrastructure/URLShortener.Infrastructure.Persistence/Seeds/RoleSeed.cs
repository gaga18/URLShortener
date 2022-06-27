using URLShortener.Core.Domain.Entities;

namespace URLShortener.Infrastructure.Persistence.Seeds
{
    internal static class RoleSeed
    {
        internal static readonly Role[] Roles = new Role[]
        {
                new Role { Id = 1, Name = "Administrator" },
                new Role { Id = 2, Name = "User" }
        };
    }
}
