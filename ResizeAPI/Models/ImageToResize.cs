using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResizeAPI.Models
{
    public class ImageToResize
    {
        public string FileName { get; set; }
        public IFormFile File { get; set; }
        public int Size { get; set; }
    }
}
