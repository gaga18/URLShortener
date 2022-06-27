using Project.Core.Domain.Entities;
using Project.Core.Domain.Enums;
using System;

namespace Project.Infrastructure.Persistence.Seeds
{
    internal static class UserSeed
    {
        internal static readonly User[] Users = new User[]
        {
                new User { Id = 1, FirstName = "Gaga", LastName = "Goginashvili", UserName = "gaga18", BirthdayDate = new DateTime(1992,1,18), Gender = Gender.Male, Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413"},
                new User { Id = 2, FirstName = "John", LastName = "Smith", UserName = "john14", BirthdayDate = new DateTime(1990,4,26), Gender = Gender.Male, Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413"},
        };
    }
}
