using Microsoft.AspNetCore.Http;

namespace DAL.Helpers
{
    public static class FileHelper
    {
        private static readonly string UploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        static FileHelper()
        {
            // Ensure uploads directory exists
            if (!Directory.Exists(UploadsDirectory))
            {
                Directory.CreateDirectory(UploadsDirectory);
            }
        }

        public static async Task<string?> SaveFileAsync(IFormFile? file, string subfolder)
        {
            if (file == null || file.Length == 0)
                return null;

            try
            {
                var folderPath = Path.Combine(UploadsDirectory, subfolder);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(folderPath, fileName);
                var relativePath = Path.Combine("uploads", subfolder, fileName).Replace("\\", "/");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return relativePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
                return null;
            }
        }

        public static void DeleteFile(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
            }
        }
    }
}

