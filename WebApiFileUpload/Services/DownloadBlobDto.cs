using Azure.Storage.Blobs.Models;
using System.IO;

namespace WebApiFileUpload.Services
{
    public class DownloadBlobDto
    {
       public BlobDownloadResult BlobContent { get; set; }
       public Stream BlobStream { get; set; }
    }
}
