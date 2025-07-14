using Microsoft.AspNetCore.Http;

namespace Project.Application.Contracts.Infrastructure
{
    public interface IFileStorageService
    {
        Task DeleteFile(string containerName, string fileRoute);
        Task<string> SaveFile(string containerName, IFormFile file, bool wattermark = false);
        Task<string> EditFile(string containerName, IFormFile file, string fileRoute);
    }
}
