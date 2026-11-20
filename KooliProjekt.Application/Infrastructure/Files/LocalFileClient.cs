using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace KooliProjekt.Application.Infrastructure.Files
{
    public class LocalFileClient : IFileClient
    {
        private readonly string _filesRoot;

        public LocalFileClient(string filesRoot)
        {
            _filesRoot = filesRoot;
        }

        public async Task Save(string fileName, string fileStore, Stream inputStream)
        {
            var folderPath = Path.Combine(_filesRoot, fileStore);
            var filePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await inputStream.CopyToAsync(fileStream);
        }

        public Task Delete(string fileName, string fileStore)
        {
            var filePath = Path.Combine(_filesRoot, fileStore, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }

        public async Task<byte[]> Get(string fileName, string fileStore)
        {
            var filePath = Path.Combine(_filesRoot, fileStore, fileName);
            return await File.ReadAllBytesAsync(filePath);
        }

        public Task<IList<string>> List(string fileStore)
        {
            var folderPath = Path.Combine(_filesRoot, fileStore);
        
            IList<string> files = Directory.Exists(folderPath)
                ? Directory.GetFiles(folderPath)
                .Select(Path.GetFileName)
                .Where(fileName => fileName != null)
                .ToList()! : new List<string>();

            return Task.FromResult(files);
        }
    }
}
