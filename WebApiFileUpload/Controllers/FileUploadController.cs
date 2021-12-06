using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiFileUpload.Services;

namespace WebApiFileUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IStorageService storageService;
        public FileUploadController(IStorageService storageService)
        {
            this.storageService = storageService;
        }
        public IActionResult Get()
        {
            return Ok("File Upload API running...");
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult Upload(IFormFile[] file)
        {
           storageService.Upload(file);
            return Ok("");
        }

        [HttpGet]
        [Route("download")]
        public IActionResult Download(string fileName)
        {
            var blob = storageService.Download(fileName);
            return File(blob.BlobStream, blob.BlobContent.Details.ContentType);
        }
    }
}
