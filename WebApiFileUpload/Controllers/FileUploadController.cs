using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            DownloadBlobDto blob = storageService.Download(fileName);
            return File(blob.stream, blob.contentType, fileName);
        }
    }
}
