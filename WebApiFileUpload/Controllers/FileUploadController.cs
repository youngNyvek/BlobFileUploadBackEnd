using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    }
}
