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
	public class ImageRotateFlipTransform : ImageTransform
	{
		/// <summary>
		/// Sets the counter value. Defaultvalue is 0
		/// </summary>
		[DefaultValue(0)]
		[Category("Behavior")]
		public RotateFlipType RotateFlip { get; set; }

		public override string UniqueString
		{
			get { return base.UniqueString + "-" + this.RotateFlip; }
		}

		public ImageRotateFlipTransform()
		{
			InterpolationMode = InterpolationMode.HighQualityBicubic;
			SmoothingMode = SmoothingMode.Default;
			PixelOffsetMode = PixelOffsetMode.Default;
			CompositingQuality = CompositingQuality.HighSpeed;
			this.RotateFlip = RotateFlipType.RotateNoneFlipNone;
		}

		public override Image ProcessImage(Image image)
		{
			Bitmap temp = (Bitmap)image;
			Bitmap bmap = (Bitmap)temp.Clone();
			bmap.RotateFlip(RotateFlip);
			return (Bitmap)bmap.Clone();
		}
	}
}