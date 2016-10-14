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

namespace Bitboxx.Web.GeneratedImage.Transform
{
	public class ImageGreyScaleTransform : ImageTransform
	{
		public ImageGreyScaleTransform()
		{
			InterpolationMode = InterpolationMode.HighQualityBicubic;
			SmoothingMode = SmoothingMode.Default;
			PixelOffsetMode = PixelOffsetMode.Default;
			CompositingQuality = CompositingQuality.HighSpeed;
		}

		public override Image ProcessImage(Image image)
		{
			Bitmap temp = (Bitmap)image;
			Bitmap bmap = (Bitmap)temp.Clone();
			Color c;
			for (int i = 0; i < bmap.Width; i++)
			{
				for (int j = 0; j < bmap.Height; j++)
				{
					c = bmap.GetPixel(i, j);
					byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);

					bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
				}
			}
			return (Bitmap)bmap.Clone();
		}
	}
}