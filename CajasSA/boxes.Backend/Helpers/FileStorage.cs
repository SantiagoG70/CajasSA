using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace boxes.Backend.Helpers
{
    public class FileStorage : IFileStorage
    {
        private readonly string _basePath;

        public FileStorage(IWebHostEnvironment env)
        {
            if (env is null) throw new ArgumentNullException(nameof(env));

            // Prefer the web root if set; otherwise fall back to content root; then to current directory.
            var root = !string.IsNullOrWhiteSpace(env.WebRootPath)
                ? env.WebRootPath
                : !string.IsNullOrWhiteSpace(env.ContentRootPath)
                    ? env.ContentRootPath
                    : Directory.GetCurrentDirectory();

            _basePath = Path.Combine(root, "Images");

            // Ensure the base Images folder exists so later writes won't fail.
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }
        }

        public async Task<string> SaveFileAsync(byte[] content, string extension, string containerName)
        {
            if (content is null) throw new ArgumentNullException(nameof(content));
            if (string.IsNullOrWhiteSpace(containerName)) throw new ArgumentException("Container name is required.", nameof(containerName));

            // Normalize extension
            if (!string.IsNullOrEmpty(extension) && !extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            // Create container folder if it doesn't exist
            string folder = Path.Combine(_basePath, containerName);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(folder, fileName);

            await File.WriteAllBytesAsync(filePath, content);

            // Return URL path that matches how static files are exposed in Program.cs (RequestPath = "/Images")
            return $"/Images/{containerName}/{fileName}";
        }

        public Task RemoveFileAsync(string path, string containerName)
        {
            if (string.IsNullOrWhiteSpace(path)) return Task.CompletedTask;
            if (string.IsNullOrWhiteSpace(containerName)) return Task.CompletedTask;

            // Use the file name from the incoming path to avoid path traversal
            var fileName = Path.GetFileName(path);
            if (string.IsNullOrEmpty(fileName)) return Task.CompletedTask;

            string file = Path.Combine(_basePath, containerName, fileName);
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            return Task.CompletedTask;
        }
    }
}
