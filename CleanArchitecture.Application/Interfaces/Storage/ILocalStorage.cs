using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Interfaces.Storage
{
    public interface ILocalStorage
    {
        Task<string> UploadFileAsync(string genre, IFormFile file, CancellationToken cancellationToken);
        Task DeleteAsync(string genre, string fileNameWithExtension,CancellationToken cancellationToken);
        string GetFileAsync(string genre, string fileNameWithExtension);
        Task<IList<(string FileName, string Path)>> UploadManyFilesAsync(string genre, IFormFileCollection files, CancellationToken cancellationToken);
    }
}
