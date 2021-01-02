using System;
using System.IO;
using System.Threading.Tasks;

namespace BookArchive.Application
{
    public class LocalWebFileStorageService : IFileStorageService
    {
        public string BasePath { get; }
        public string StoragePath { get; }
        public Uri AccessURI { get; set; }

        public LocalWebFileStorageService(string basePath, string storagePath, Uri accessURI)
        {
            BasePath = basePath;
            StoragePath = storagePath;
            AccessURI = accessURI;
            Directory.CreateDirectory(Path.Combine(basePath, StoragePath));
        }

        public async Task<Stream> Load(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Save(Stream content, string name)
        {
            var storePath = Path.Combine(BasePath, StoragePath, name);
            FileStream fileStream = default;
            try
            {
                fileStream = new FileStream(storePath, FileMode.Create);
                await content.CopyToAsync(fileStream);
                fileStream.Close();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                fileStream?.Close();
            }
            return new Uri(Path.Combine(AccessURI.AbsoluteUri, StoragePath, name)).AbsoluteUri;
        }

        public string GetServiceName()
        {
            return GetType().Name;
        }

    }
}
