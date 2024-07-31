using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SimpleDeveloperCore.Models
{
    public class ErrorLogger
    {
        private readonly string _loggingDirectory;

        // Constructor to set the logging directory
        public ErrorLogger()
        {
            // Get the current working directory of the application
            var currentDirectory = Directory.GetCurrentDirectory();

            // Define the path where the error log will be stored
            _loggingDirectory = Path.Combine(currentDirectory, "wwwroot", "CustomLogging");

            // Ensure the directory exists
            Directory.CreateDirectory(_loggingDirectory);
        }

        // Instance method to log errors
        public void LogError(Exception ex, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string methodName = "")
        {
            // Create an object to hold error details
            var errorDetails = new
            {
                Timestamp = DateTime.Now,
                MethodName = methodName,
                LineNumber = lineNumber,
                ExceptionMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace
            };

            // Serialize the object to JSON
            string jsonError = JsonSerializer.Serialize(errorDetails, new JsonSerializerOptions { WriteIndented = true });

            // Define the file path
            string filePath = Path.Combine(_loggingDirectory, "ErrorLog.txt");

            // Write the JSON to the file
            File.AppendAllText(filePath, jsonError + Environment.NewLine);
        }
    }
}
