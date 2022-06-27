using Project.Core.Domain.Enums;
using System;

namespace Project.Core.Application.DTOs
{
    public class SetUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
