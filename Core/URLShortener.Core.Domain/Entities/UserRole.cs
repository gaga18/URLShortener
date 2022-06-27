using Project.Core.Domain.Basics;
using URLShortener.Core.Domain.Entities;

namespace Project.Core.Domain.Entities
{
    public class UserRole : AuditableEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
