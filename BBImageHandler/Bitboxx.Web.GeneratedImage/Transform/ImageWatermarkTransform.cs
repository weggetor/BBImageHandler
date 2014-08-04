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
using System.Drawing.Drawing2D;

namespace Bitboxx.Web.GeneratedImage.Transform
{
	public class ImageWatermarkTransform : ImageTransform
	{
		/// <summary>
		/// Sets the watermark text. Defaultvalue is empty
		/// </summary>
		[DefaultValue("")]
		[Category("Behavior")]
		public string WatermarkText { get; set; }

		/// <summary>
		/// Sets the watermark position Defaultvalue is center
		/// </summary>
		[DefaultValue(WatermarkPositionMode.Center)]
		[Category("Behavior")]
		public WatermarkPositionMode WatermarkPosition { get; set; }

		/// <summary>
		/// Sets the watermark opacity. Defaultvalue is 127 (0..255).
		/// </summary>
		[DefaultValue(127)]
		[Category("Behavior")]
		public int WatermarkOpacity { get; set; }

		/// <summary>
		/// Sets the watermark fontcolor. Default is black
		/// </summary>
		[DefaultValue(typeof(Color),"Black")]
		[Category("Behavior")]
		public Color FontColor  { get; set; }

		/// <summary>
		/// Sets the watermark font family. Default is Verdana
		/// </summary>
		[DefaultValue("Verdana")]
		[Category("Behavior")]
		public string FontFamily { get; set; }

		/// <summary>
		/// Sets the watermark font size. Default is 14
		/// </summary>
		[DefaultValue(14.0)]
		[Category("Behavior")]
		public Single FontSize { get; set; }

		public override string UniqueString
		{
			get
			{
				// MyBase.UniqueString, "-", Me.WatermarkText, "-", Me.FontFamily, "-", Me.FontSize, "-", Me.FontColor.ToString()
				return base.UniqueString + "-" + 
				       this.WatermarkText + "-" + 
				       this.WatermarkPosition.ToString()+"-"+
				       this.WatermarkOpacity.ToString()+"-"+
				       this.FontColor.ToString()+"-"+
				       this.FontFamily + "-"+
				       this.FontSize.ToString();

			}
		}

		public ImageWatermarkTransform() {
			InterpolationMode = InterpolationMode.HighQualityBicubic;
			SmoothingMode = SmoothingMode.Default;
			PixelOffsetMode = PixelOffsetMode.Default;
			CompositingQuality = CompositingQuality.HighSpeed;

			WatermarkText = string.Empty;
			WatermarkPosition = WatermarkPositionMode.Center;
			WatermarkOpacity = 127;
			FontColor = Color.Black;
			FontFamily = "Verdana";
			FontSize = 14;
		}

		public override Image ProcessImage(Image image)
		{
			Font watermarkFont = new Font(this.FontFamily, this.FontSize);

            Bitmap newBitmap = new Bitmap(image.Width, image.Height);
            Graphics graphics = Graphics.FromImage(newBitmap);

            graphics.CompositingMode = CompositingMode.SourceOver;
			graphics.CompositingQuality = CompositingQuality;
			graphics.InterpolationMode = InterpolationMode;
			graphics.SmoothingMode = SmoothingMode;

            graphics.DrawImage(image, 0, 0);


			SizeF sz = graphics.MeasureString(this.WatermarkText, watermarkFont);
			Single x = 0;
			Single y = 0;
			
			switch (this.WatermarkPosition)
			{
				case WatermarkPositionMode.TopLeft:
					x = 0;
					y = 0;
					break;
				case WatermarkPositionMode.TopCenter:
					x = image.Width / 2 - sz.Width / 2;
					y = 0;
					break;
				case WatermarkPositionMode.TopRight:
					x = image.Width - sz.Width;
					y = 0;
					break;
				case WatermarkPositionMode.CenterLeft:
					x = 0;
					y = image.Height / 2 - sz.Height / 2;
					break;
				case WatermarkPositionMode.Center:
					x = image.Width / 2 - sz.Width / 2;
					y = image.Height / 2 - sz.Height / 2;
					break;
				case WatermarkPositionMode.CenterRight:
					x = image.Width - sz.Width;
					y = image.Height / 2 - sz.Height / 2;
					break;
				case WatermarkPositionMode.BottomLeft:
					x = 0;
					y = image.Height - sz.Height;
					break;
				case WatermarkPositionMode.BottomCenter:
					x = image.Width / 2 - sz.Width / 2;
					y = image.Height - sz.Height;
					break;
				case WatermarkPositionMode.BottomRight:
					x = image.Width - sz.Width;
					y = image.Height - sz.Height;
					break;
				default:
					break;
			}
			Brush watermarkBrush  = new SolidBrush(Color.FromArgb(WatermarkOpacity, FontColor));
			graphics.DrawString(this.WatermarkText, watermarkFont, watermarkBrush, x, y);
			return image;
	
		}

	}
}