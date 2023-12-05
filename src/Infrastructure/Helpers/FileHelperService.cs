using Application.Interfaces.Helpers;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Helpers
{
    public class FileHelperService : IFileHelperService
    {
        public async Task<string[]> UploadFile(IFormFile file)
        {
            string fileName = $"{Guid.NewGuid().ToString()}-{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "../../uploads");
            string imagePath = Path.Combine(filePath, fileName);

            using var stream = File.Create(imagePath);
            await file.CopyToAsync(stream);

            return [fileName, filePath];
        }

        public void DeleteFile(string imagePath)
        {
            File.Delete(imagePath);
        }
    }
}
