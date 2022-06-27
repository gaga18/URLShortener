using Project.Core.Domain.Basics;
using Project.Core.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Project.Core.Domain.Entities
{
    public class User : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime BirthdayDate { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
