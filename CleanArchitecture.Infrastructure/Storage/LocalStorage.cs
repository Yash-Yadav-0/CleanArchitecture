using CleanArchitecture.Application.Interfaces.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Storage
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly string[] _allowedFileExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".svg", ".jfif" };
        private const long _allowedFileSize = 1 * 1024 * 1024; // 1 MB

        public LocalStorage(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public async Task<string> UploadFileAsync(string genre, IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file), "File is null.");
            }

            if (file.Length > _allowedFileSize)
            {
                throw new ArgumentException("File size exceeds 1 MB", nameof(file));
            }
            var contentPath = _webHost.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads", genre);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var extension = Path.GetExtension(file.FileName);
            if (!_allowedFileExtensions.Contains(extension))
            {
                throw new ArgumentException($"Not an allowed file format, must be in format {string.Join(',', _allowedFileExtensions)}", nameof(file));
            }
            var fileName = $"{Guid.NewGuid()}{extension}";
            var fileNameWithPath = Path.Combine(path, fileName);
            using var stream = new FileStream(fileNameWithPath, FileMode.CreateNew);
            await file.CopyToAsync(stream, cancellationToken);

            return fileName;
        }

        public Task DeleteAsync(string genre, string fileNameWithExtension , CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                throw new ArgumentException("Path must be provided.", nameof(fileNameWithExtension));
            }

            var contentPath = _webHost.ContentRootPath;
            var fileNameWithPath = Path.Combine(contentPath, "Uploads", genre, fileNameWithExtension);

            if (!File.Exists(fileNameWithPath))
            {
                throw new FileNotFoundException($"Unable to find File{fileNameWithExtension}");
            }
            File.Delete(fileNameWithPath);
            return Task.CompletedTask;
        }
        public string GetFileAsync(string genre, string fileNameWithExtension)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                throw new ArgumentException("File name must be provided.", nameof(fileNameWithExtension));
            }

            var contentPath = _webHost.ContentRootPath;
            var fileNameWithPath = Path.Combine(contentPath, "Uploads", genre, fileNameWithExtension);

            if (!File.Exists(fileNameWithPath))
            {
                throw new FileNotFoundException($"Unable to find file {fileNameWithExtension}");
            }

            return fileNameWithPath;
        }
        public async Task<IList<(string FileName, string Path)>> UploadManyFilesAsync(string genre, IFormFileCollection files, CancellationToken cancellationToken)
        {
            if (files == null || files.Count == 0)
            {
                throw new ArgumentNullException(nameof(files), "Files collection is null or empty.");
            }

            var uploadedFiles = new List<(string FileName, string Path)>();

            var contentPath = _webHost.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads", genre);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var file in files)
            {
                if (file.Length > _allowedFileSize)
                {
                    throw new ArgumentException($"File {file.FileName} size exceeds 1 MB", nameof(file));
                }

                var extension = Path.GetExtension(file.FileName);
                if (!_allowedFileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    throw new ArgumentException($"File {file.FileName} format is not allowed. Must be in format {string.Join(',', _allowedFileExtensions)}", nameof(file));
                }

                var fileName = $"{Guid.NewGuid()}{extension}";
                var fileNameWithPath = Path.Combine(path, fileName);
                using var stream = new FileStream(fileNameWithPath, FileMode.CreateNew);
                await file.CopyToAsync(stream, cancellationToken);

                uploadedFiles.Add((fileName, fileNameWithPath));
            }

            return uploadedFiles;
        }
    }
}
