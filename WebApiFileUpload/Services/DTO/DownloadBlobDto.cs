using Azure.Storage.Blobs.Models;
using System.IO;

namespace WebApiFileUpload.Services
{
    public class DownloadBlobDto
    {
       public string contentType { get; set; }
       public Stream stream { get; set; }
    }
}
