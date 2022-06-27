using Project.Core.Application.Interfaces.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace Project.Infrastructure.Files
{
    public class FileManager : IFileManager
    {
        private readonly string directory;
        public FileManager(IConfiguration configuration)
        {
            this.directory = configuration["Document:Address"];
        }

        public string SaveImage(string base64String)
        {
            // დირექტორიის შექმნა
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            var imageName = Guid.NewGuid() + ".jpg";

            string imgPath = Path.Combine(directory, imageName);

            byte[] imageBytes = Convert.FromBase64String(base64String);

            File.WriteAllBytes(imgPath, imageBytes);

            return imageName;
        }
    }
}
