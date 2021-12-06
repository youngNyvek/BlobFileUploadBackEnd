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
            var containerName = configuration.GetSection("Storage:ContainerName").Value;

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            for(var file = 0; file < formFile.Length; file++)
            {
                var blobClient = containerClient.GetBlobClient(formFile[file].FileName);

                var stream = formFile[file].OpenReadStream();
                blobClient.Upload(stream, true);
            }
        }

        public DownloadBlobDto Download(string filename)
        {
            BlobClient blobClient;
            BlobDownloadResult blobContent;

            var containerName = configuration.GetSection("Storage:ContainerName").Value;
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            blobClient = containerClient.GetBlobClient(filename);
            blobContent = blobClient.DownloadContent();
           
            Stream blobStream = blobClient.OpenRead();

            return new DownloadBlobDto()
            {
                BlobContent = blobContent,
                BlobStream = blobStream
            };
        }
    }
}
