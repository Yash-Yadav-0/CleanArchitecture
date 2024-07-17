using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Interfaces.Storage
{
    public interface ILocalStorage
    {
        Task<(string FileName, string Path)> UploadAsync(int id, string folderName, IFormFile file);
        Task<IList<(string FilesName, string Path)>> UploadManyAsync(int id, string folderName, IFormFileCollection files);
        Task DeleteAsync(string path, string fileName);
        IList<string> GetFile(string path);
    }
}
