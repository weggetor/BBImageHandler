/* 
  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF 
  ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
  THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A 
  PARTICULAR PURPOSE. 
  
    This is sample code and is freely distributable. 
*/
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;

namespace Bitboxx.Web.GeneratedImage.Transform
{
	public class ImageUrlImageTransform : ImageTransform
	{
		/// <summary>
		/// Sets the Url. Defaultvalue is empty
		/// </summary>
		[DefaultValue("")]
		[Category("Behavior")]
		public string ImageUrl { get; set; }

        /// <summary>
        /// Sets the empty Image
        /// </summary>
        [DefaultValue("")]
        [Category("Behavior")]
        public Image EmptyImage { get; set; }

		public override string UniqueString
		{
			get
			{
				return base.UniqueString + "-" +  this.ImageUrl;
			}
		}

        public ImageUrlImageTransform()
		{
			InterpolationMode = InterpolationMode.HighQualityBicubic;
			SmoothingMode = SmoothingMode.Default;
			PixelOffsetMode = PixelOffsetMode.Default;
			CompositingQuality = CompositingQuality.HighSpeed;
		}

		public override Image ProcessImage(Image image)
		{
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(ImageUrl);

		    try
		    {
                using (HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream stream = httpWebReponse.GetResponseStream())
                    {
                        return Image.FromStream(stream);
                    }
                }
		    }
		    catch (Exception)
		    {
                return EmptyImage;
		    }
		
		}
	}
}