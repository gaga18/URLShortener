using Microsoft.AspNetCore.Http;

namespace Project.Core.Application.Interfaces.Contracts
{
    public interface IFileManager
    {
        string SaveImage(string base64String);
    }
}
