
using System;
using System.IO;
using System.Threading.Tasks;

namespace BookArchive
{
    public interface IFileStorageService
    {
        Uri AccessURI { get; }

        Task<Stream> Load(string name);
        Task<string> Save(Stream content, string name);
        Task<bool> Delete(string name) => Task.FromResult(true);
        string GetServiceName();
    }
}