using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebApiFileUpload.Services
{
    public class StorageService : IStorageService
    {
        private readonly BlobServiceClient blobServiceClient;
        private readonly IConfiguration configuration;
        

        public StorageService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            this.blobServiceClient = blobServiceClient;
            this.configuration = configuration;
        }


        public void Upload(IFormFile[] formFile)
        {
            string containerName = configuration.GetSection("Storage:ContainerName").Value;
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            for(var file = 0; file < formFile.Length; file++)
            {
                BlobClient blobClient = containerClient.GetBlobClient(formFile[file].FileName);

                Stream stream = formFile[file].OpenReadStream();
                blobClient.Upload(stream, true);
            }
        }

        public DownloadBlobDto Download(string filename)
        {
            string containerName = configuration.GetSection("Storage:ContainerName").Value;
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(filename);
           
            Stream blobStream = blobClient.OpenRead();
            string contetType = Path.GetExtension(blobClient.Name);

            return new DownloadBlobDto()
            {
                contentType = $"image/{contetType.TrimStart('.')}",
                stream = blobStream
            };
        }
    }
}
