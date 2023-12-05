using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Helpers
{
    public interface IFileHelperService
    {
        public Task<string[]> UploadFile(IFormFile file);
        public void DeleteFile(string imagePath);
    }
}
