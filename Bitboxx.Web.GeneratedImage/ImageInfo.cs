using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web;


namespace Bitboxx.Web.GeneratedImage
{
    public class ImageInfo {
        public Image Image { get; private set; }
        public byte[] ImageByteBuffer { get; private set; }
        public HttpStatusCode? HttpStatusCode { get; private set; }

        public ImageInfo(HttpStatusCode statusCode) {
            HttpStatusCode = statusCode;
        }

        public ImageInfo(Image image) {
            if (image == null) {
                throw new ArgumentNullException("image");
            }

            Image = image;
        }

        public ImageInfo(byte[] imageBuffer) {
            if (imageBuffer == null) {
                throw new ArgumentNullException("imageBuffer");
            }

            ImageByteBuffer = imageBuffer;
        }
    }
}
