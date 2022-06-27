using Project.Core.Domain.Basics;
using Project.Core.Domain.Entities;
using System.Collections.Generic;

namespace URLShortener.Core.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
