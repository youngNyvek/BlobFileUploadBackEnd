using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace WebApiFileUpload.Services
{
    public interface IStorageService
    {
        void Upload(IFormFile[] formFile);
        DownloadBlobDto Download(string filename);
    }
}
