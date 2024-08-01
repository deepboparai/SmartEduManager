using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperServices.Services
{
    public class FileService
    {
        public static async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }

            // Get the current directory where the application is running
            var currentDirectory = Directory.GetCurrentDirectory();

            // Define the path where the file will be stored
            var uploadsFolder = Path.Combine(currentDirectory, "wwwroot", folderName);

            // Ensure the folder exists
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Create a unique file name by combining a GUID with the original file name
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Save the file to the specified path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return the file path relative to the web root
            return Path.Combine(folderName, uniqueFileName).Replace("\\", "/");
        }
    }
}
