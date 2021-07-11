using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResizeAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace ResizeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResizeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly Secret admin = new Secret();

        //constructor
        public ResizeController(ILogger<ResizeController> logger)
        {
            _logger = logger;
        }

        //get request
        [HttpGet]
        public IActionResult Get()
        {
            string key = this.HttpContext.Request.Headers["x-auth"];
            
            if (key == admin.id) return Ok("API is running...");
            return Problem("Not Authorized to use the API");
        }

        //resize request as POST
        [HttpPost]
        public IActionResult Upload([FromForm]ImageToResize ogImg)
        {
            string key = this.HttpContext.Request.Headers["x-auth"];
            if (key != admin.id) return Problem("You're not Authorized to use the API");
            if (ogImg.File == null) return Problem("You sent nothing...");
            if (ogImg.File.ContentType != "image/png") return Problem("Image is not in a valid format.");

            ResizedImage image = new ResizedImage(ogImg.FileName, ogImg.File, ogImg.Size);
            image.Resize();



            //basic log
            float size = (float)Math.Round(ogImg.File.Length / 1048576f, 2);
            _logger.LogInformation(HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString() + " requested a resize. File size: " + size + "mb");
            
            return Ok(image);
            
        }

    }
}
