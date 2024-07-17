using CleanArchitecture.Application.Interfaces.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Storage
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment webHost;
        private readonly string _wwwroot;
        private readonly string _folderPath = "images";
        private readonly string[] _allowedFileExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".svg", ".jfif" };
        private const long _allowedFileSize = 1 * 1024 * 1024; // 1 Mb


        public LocalStorage(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            _wwwroot = webHost.WebRootPath;
        }
        public async Task<(string FileName, string Path)> UploadAsync(int id, string folderName, IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file), "File is null.");
            }

            if (file.Length > _allowedFileSize)
            {
                throw new ArgumentException("File size exceeds 1 Mb", nameof(file));
            }

            string extension = Path.GetExtension(file.FileName);
            if (!_allowedFileExtensions.Contains(extension))
            {
                throw new ArgumentException($"Not an allowed file format. Must be in format {string.Join(", ", _allowedFileExtensions)}", nameof(file));
            }

            string folderPath = Path.Combine(_wwwroot, _folderPath, folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string newFileName = $"{file.FileName.Replace(extension, $"_{id}").ToLower()}_{DateTime.UtcNow:dd_MM_yyyy}{extension}";
            string path = Path.Combine(folderPath, newFileName);

            using var stream = new FileStream(path, FileMode.CreateNew);
            await file.CopyToAsync(stream);

            return (newFileName, path);
        }

        public async Task<IList<(string FilesName, string Path)>> UploadManyAsync(int id, string folderName, IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
            {
                throw new ArgumentException("No files to upload.", nameof(files));
            }

            string folderPath = Path.Combine(_wwwroot, _folderPath, folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            List<(string FilesName, string Path)> uploadedFiles = new List<(string FilesName, string Path)>();

            foreach (IFormFile file in files)
            {
                if (file.Length > _allowedFileSize)
                {
                    throw new ArgumentException("File size exceeds 1 Mb", nameof(file));
                }

                string extension = Path.GetExtension(file.FileName);
                if (!_allowedFileExtensions.Contains(extension))
                {
                    throw new ArgumentException($"Not an allowed file format. Must be in format {string.Join(", ", _allowedFileExtensions)}", nameof(file));
                }

                string newFileName = $"{file.FileName.Replace(extension, $"_{id}").ToLower()}_{DateTime.UtcNow:dd_MM_yyyy}{extension}";
                string path = Path.Combine(folderPath, newFileName);

                using var stream = new FileStream(path, FileMode.CreateNew);
                await file.CopyToAsync(stream);

                uploadedFiles.Add((newFileName, path));
            }

            return uploadedFiles;
        }

        public Task DeleteAsync(string path, string fileName)
        {
            string filePath = Path.Combine(_wwwroot, _folderPath, path, fileName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File '{fileName}' not found.", fileName);
            }

            File.Delete(filePath);
            return Task.CompletedTask;
        }

        public IList<string> GetFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}
