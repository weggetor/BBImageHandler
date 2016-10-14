/* 
  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF 
  ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
  THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A 
  PARTICULAR PURPOSE. 
  
    This is sample code and is freely distributable. 
*/
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;

namespace Bitboxx.Web.GeneratedImage.Transform
{
	public class ImagePercentageTransform : ImageTransform
	{
		/// <summary>
		/// Sets the percentage value for the radial indicator
		/// </summary>
		[DefaultValue("")]
		[Category("Behavior")]
		public int Percentage { get; set; }

		/// <summary>
		/// Sets the Color of the indicator element
		/// </summary>
		[DefaultValue("")]
		[Category("Behavior")]
		public Color Color { get; set; }

		public override string UniqueString
		{
			get { return base.UniqueString + this.Percentage.ToString() + "-" + this.Color.ToString(); }
		}

		public ImagePercentageTransform()
		{
			InterpolationMode = InterpolationMode.HighQualityBicubic;
			SmoothingMode = SmoothingMode.Default;
			PixelOffsetMode = PixelOffsetMode.Default;
			CompositingQuality = CompositingQuality.HighSpeed;
		}

		public override Image ProcessImage(Image image)
		{
			Bitmap bitmap = new Bitmap(100, 100);
			using (Graphics objGraphics = Graphics.FromImage(bitmap))
			{
				// Initialize graphics
				objGraphics.Clear(Color.White);
				objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
				objGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

				// Fill pie
				// Degrees are taken clockwise, 0 is parallel with x
				// For sweep angle we must convert percent to degrees (90/25 = 18/5)
				float startAngle = -90.0F;
				float sweepAngle = (18.0F/5)*Percentage;

				Rectangle rectangle = new Rectangle(5, 5, 90, 90);
				Brush colorBrush = new SolidBrush(Color);
				objGraphics.FillPie(colorBrush, rectangle, startAngle, sweepAngle);

				// Fill inner circle with white
				rectangle = new Rectangle(20, 20, 60, 60);
				objGraphics.FillEllipse(Brushes.White, rectangle);

				// Draw circles
				rectangle = new Rectangle(5, 5, 90, 90);
				objGraphics.DrawEllipse(Pens.LightGray, rectangle);
				rectangle = new Rectangle(20, 20, 60, 60);
				objGraphics.DrawEllipse(Pens.LightGray, rectangle);

				// Draw text on image
				// Use rectangle for text and align text to center of rectangle
				var font = new Font("Arial", 13, FontStyle.Bold);
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.LineAlignment = StringAlignment.Center;

				rectangle = new Rectangle(20, 40, 62, 20);
				objGraphics.DrawString(Percentage + "%", font, Brushes.DarkGray, rectangle, stringFormat);

				// Save indicator to file
				objGraphics.Flush();
			}
			return (Image) bitmap;
		}
	}
}
