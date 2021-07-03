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

namespace ResizeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResizeController : ControllerBase
    {

        public IActionResult Get()
        {
            return Ok("API is running...");
        }

        [HttpPost]
        public IActionResult Upload([FromForm]ImageToResize ogImg)
        {
            if (ogImg.File.ContentType != "image/png") return Problem("Problemo");

            ResizedImage image = new ResizedImage(ogImg.FileName, ogImg.File, ogImg.Size);
            image.Resize();

            
            //for testing purposes
            //var username = Environment.GetEnvironmentVariable("userprofile");
            //System.IO.File.WriteAllBytes($@"{username}\desktop\test.png", image.fileBlob);



            //basic log
            float size = (float)Math.Round(ogImg.File.Length / 1048576f, 2);
            Console.WriteLine(HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString() + " requested a resize. File size: " + size + "mb");
            
            return Ok(image);
            
        }
        private static async Task<string> ImageToString(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            while (reader.Peek() >= 0 )
                {
                    result.AppendLine(await reader.ReadLineAsync());
                }
            return result.ToString();
        }
    }
}
