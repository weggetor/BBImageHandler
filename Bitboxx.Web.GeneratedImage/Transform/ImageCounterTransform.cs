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
	public class ImageCounterTransform : ImageTransform
	{
		/// <summary>
		/// Sets the counter value. Defaultvalue is 0
		/// </summary>
		[DefaultValue(0)]
		[Category("Behavior")]
		public int Counter { get; set; }

		/// <summary>
		/// Sets the number of digits. Defaultvalue is 5
		/// </summary>
		[DefaultValue(5)]
		[Category("Behavior")]
		public int Digits { get; set; }

		public override string UniqueString
		{
			get
			{
				return base.UniqueString + "-" +
				       this.Counter.ToString() + "-" +
				       this.Digits.ToString() + "-";
			}
		}

		public ImageCounterTransform()
		{
			InterpolationMode = InterpolationMode.HighQualityBicubic;
			SmoothingMode = SmoothingMode.Default;
			PixelOffsetMode = PixelOffsetMode.Default;
			CompositingQuality = CompositingQuality.HighSpeed;

			this.Counter = 0;
			this.Digits = 5;
		}

		public override Image ProcessImage(Image image)
		{
			//Get measurements of a digit 
			int digitWidth = image.Width / 10;
			int digitHeight = image.Height;

			// Create output grahics
			Bitmap imgOutput = new Bitmap(digitWidth * this.Digits, digitHeight, PixelFormat.Format24bppRgb);
			Graphics graphics = Graphics.FromImage(imgOutput);

			graphics.CompositingMode = CompositingMode.SourceCopy;
			graphics.CompositingQuality = CompositingQuality;
			graphics.InterpolationMode = InterpolationMode;
			graphics.SmoothingMode = SmoothingMode;
			graphics.PixelOffsetMode = PixelOffsetMode;


			// Sampling the output together
			string strCountVal = this.Counter.ToString().PadLeft(this.Digits, '0');
			for (int i = 0; i < this.Digits; i++)
			{
				// Extract digit from countVal
				int digit = Convert.ToInt32(strCountVal.Substring(i, 1));

				// Add digit to output graphics
				Rectangle targetRect = new Rectangle(i * digitWidth, 0, digitWidth, digitHeight);
				Rectangle sourceRect = new Rectangle(digit * digitWidth, 0, digitWidth, digitHeight);
				graphics.DrawImage(image, targetRect, sourceRect, GraphicsUnit.Pixel);
			}
			return imgOutput;
		}
	}
}