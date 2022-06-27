using System;
using System.Collections.Generic;

namespace Project.Core.Application.DTOs
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public DateTime BirthdayDate { get; set; }

        public virtual ICollection<GetUserRoleDto> UserRoles { get; set; }
    }
}
