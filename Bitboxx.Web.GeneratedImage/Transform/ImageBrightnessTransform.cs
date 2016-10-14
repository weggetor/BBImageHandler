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
	public class ImageBrightnessTransform : ImageTransform
	{
		/// <summary>
		/// Sets the counter value. Defaultvalue is 0
		/// </summary>
		[DefaultValue(0)]
		[Category("Behavior")]
		public int Brightness { get; set; }


		public override string UniqueString
		{
			get { return base.UniqueString + "-" + this.Brightness.ToString(); }
		}

		public ImageBrightnessTransform()
		{
			InterpolationMode = InterpolationMode.HighQualityBicubic;
			SmoothingMode = SmoothingMode.Default;
			PixelOffsetMode = PixelOffsetMode.Default;
			CompositingQuality = CompositingQuality.HighSpeed;
			this.Brightness = 0;
		}

		public override Image ProcessImage(Image image)
		{
			Bitmap temp = (Bitmap)image;
			Bitmap bmap = (Bitmap)temp.Clone();
			if (Brightness < -255) Brightness = -255;
			if (Brightness > 255) Brightness = 255;
			Color c;
			for (int i = 0; i < bmap.Width; i++)
			{
				for (int j = 0; j < bmap.Height; j++)
				{
					c = bmap.GetPixel(i, j);
					int cR = c.R + Brightness;
					int cG = c.G + Brightness;
					int cB = c.B + Brightness;

					if (cR < 0) cR = 1;
					if (cR > 255) cR = 255;

					if (cG < 0) cG = 1;
					if (cG > 255) cG = 255;

					if (cB < 0) cB = 1;
					if (cB > 255) cB = 255;

					bmap.SetPixel(i, j, Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
				}
			}
			return (Bitmap)bmap.Clone();
		}
	}
}