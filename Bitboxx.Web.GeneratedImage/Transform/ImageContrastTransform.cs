﻿/* 
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
	public class ImageContrastTransform : ImageTransform
	{
		/// <summary>
		/// Sets the counter value. Defaultvalue is 0
		/// </summary>
		[DefaultValue(0)]
		[Category("Behavior")]
		public double Contrast { get; set; }


		public override string UniqueString
		{
			get { return base.UniqueString + "-" + this.Contrast.ToString(); }
		}

		public ImageContrastTransform()
		{
			InterpolationMode = InterpolationMode.HighQualityBicubic;
			SmoothingMode = SmoothingMode.Default;
			PixelOffsetMode = PixelOffsetMode.Default;
			CompositingQuality = CompositingQuality.HighSpeed;
			this.Contrast = 0;
		}

		public override Image ProcessImage(Image image)
		{
			Bitmap temp = (Bitmap)image;
			Bitmap bmap = (Bitmap)temp.Clone();
			if (Contrast < -100) Contrast = -100;
			if (Contrast > 100) Contrast = 100;
			Contrast = (100.0 + Contrast) / 100.0;
			Contrast *= Contrast;
			Color c;
			for (int i = 0; i < bmap.Width; i++)
			{
				for (int j = 0; j < bmap.Height; j++)
				{
					c = bmap.GetPixel(i, j);
					double pR = c.R / 255.0;
					pR -= 0.5;
					pR *= Contrast;
					pR += 0.5;
					pR *= 255;
					if (pR < 0) pR = 0;
					if (pR > 255) pR = 255;

					double pG = c.G / 255.0;
					pG -= 0.5;
					pG *= Contrast;
					pG += 0.5;
					pG *= 255;
					if (pG < 0) pG = 0;
					if (pG > 255) pG = 255;

					double pB = c.B / 255.0;
					pB -= 0.5;
					pB *= Contrast;
					pB += 0.5;
					pB *= 255;
					if (pB < 0) pB = 0;
					if (pB > 255) pB = 255;

					bmap.SetPixel(i, j, Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
				}
			}
			return (Bitmap)bmap.Clone();
		}
	}
}