using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ResizeAPI.Models
{
    /// <summary>
    /// This class contains the final thumbnail object to be send
    /// as well as the Resize function to generate said thumbnail.
    /// </summary>
    public class ResizedImage
    {
        //JSON Object will contain these
        public string fileName { get; set; }
        public byte[] fileBlob { get; set; }
        //end of JSON

        private int size;
        private IFormFile file;

        /// <summary>
        /// The constructor for this class.
        /// </summary>
        /// <param name="filename"> Required, the keyword 'thumb' will be prefixed.</param>
        /// <param name="formfile"> Required, the file that was sent through the HttpRequest </param>
        /// <param name="Size"> Optional, the size of the thumbnail to generate, default is 250 x 250px </param>
        public ResizedImage(string filename, IFormFile formfile, int Size = 250)
        {
            fileName = "thumb" + filename;
            file = formfile;
            size = Size;
        }

        /// <summary>
        ///This function uses the initial file passed in the constructor
        ///and creates a thumbnail image given a specified size.
        /// <para>Thumbnails will always be squared.</para>
        /// </summary>
        public void Resize()
        {

            //we get the image from the og file stream
            Image bm = Image.FromStream(file.OpenReadStream());

            //we make a thumbnail of it
            Image thumbnail = bm.GetThumbnailImage(size, size, new Image.GetThumbnailImageAbort(ThumbnailCallback), new IntPtr());

            //we pass it to a memory stream in raw format (binary array) thus creating a blob
            //then assign the new blob to the public property
            using (var ms = new MemoryStream())
            {
                thumbnail.Save(ms, ImageFormat.Png);
                byte[] newBlob = ms.ToArray();
                fileBlob = newBlob;
            }


        }

        //no effing clue
        private bool ThumbnailCallback()
        {
            return true;
        }

    }
}
