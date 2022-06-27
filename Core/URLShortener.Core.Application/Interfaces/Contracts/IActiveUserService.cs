using System;

namespace Project.Core.Application.Interfaces.Contracts
{
    public interface IActiveUserService
    {
        int UserId { get; }
        string IpAddress { get; }
        int Port { get; }
        string RequestUrl { get; }
        string RequestMethod { get; }
    }
}
