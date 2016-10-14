/* 
  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF 
  ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
  THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A 
  PARTICULAR PURPOSE. 
  
    This is sample code and is freely distributable. 
*/
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Bitboxx.Web.GeneratedImage.Transform
{
	public class ImageResizeTransform : ImageTransform
	{
	    private int _width = 0, _height = 0, _border = 0, _maxWidth = 0, _maxHeight = 0;
		private Color _BackColor = Color.White;

		/// <summary>
		/// Sets the resize mode. The default value is Fit.
		/// </summary>
		[DefaultValue(ImageResizeMode.Fit)]
		[Category("Behavior")]
		public ImageResizeMode Mode { get; set; }
        
		/// <summary>
		/// Sets the width of the resulting image
		/// </summary>
		[DefaultValue(0)]
		[Category("Behavior")]
		public int Width {
			get {
				return _width;
			}
			set {
				CheckValue(value);
				_width = value;
			}
		}

        /// <summary>
        /// Sets the Max width of the resulting image
        /// </summary>
        [DefaultValue(0)]
        [Category("Behavior")]
        public int MaxWidth
        {
            get
            {
                return _maxWidth;
            }
            set
            {
                CheckValue(value);
                _maxWidth = value;
            }
        }

		/// <summary>
		/// Sets the height of the resulting image
		/// </summary>
		[DefaultValue(0)]
		[Category("Behavior")]
		public int Height {
			get {
				return _height;
			}
			set {
				CheckValue(value);
				_height = value;
			}
		}

        /// <summary>
        /// Sets the height of the resulting image
        /// </summary>
        [DefaultValue(0)]
        [Category("Behavior")]
        public int MaxHeight
        {
            get
            {
                return _maxHeight;
            }
            set
            {
                CheckValue(value);
                _maxHeight = value;
            }
        }

		/// <summary>
		/// Sets the border width of the resulting image
		/// </summary>
		[DefaultValue(0)]
		[Category("Behavior")]
		public int Border
		{
			get
			{
				return _border;
			}
			set
			{
				CheckValue(value);
				_border = value;
			}
		}

		/// <summary>
		/// Sets the Backcolor 
		/// </summary>
		[Category("Behavior")]
		public Color BackColor 
		{
			get	{ return _BackColor; }
			set { _BackColor = value; }
		}

		public ImageResizeTransform() {
			InterpolationMode = InterpolationMode.HighQualityBicubic;
			SmoothingMode = SmoothingMode.Default;
			PixelOffsetMode = PixelOffsetMode.Default;
			CompositingQuality = CompositingQuality.HighSpeed;
			Mode = ImageResizeMode.Fit;
		}

		private static void CheckValue(int value) {
			if (value < 0) {
				throw new ArgumentOutOfRangeException("value");
			}
		}

		public override Image ProcessImage(Image img)
		{
            if (this.MaxWidth > 0)
            {
                if (img.Width > this.MaxWidth)
                    this.Width = this.MaxWidth;
                else
                    this.Width = img.Width;
            }

            if (this.MaxHeight > 0)
            {
                if (img.Height > this.MaxHeight)
                    this.Height = this.MaxHeight;
                else
                    this.Height = img.Height;
            }

            int scaledHeight = (int)(img.Height * ((float)this.Width / (float)img.Width));
			int scaledWidth = (int)(img.Width * ((float)this.Height / (float)img.Height));

			Image procImage;
			switch (Mode) {
				case ImageResizeMode.Fit:
					procImage =  FitImage(img, scaledHeight, scaledWidth);
					break;
				case ImageResizeMode.Crop:
					procImage = CropImage(img, scaledHeight, scaledWidth);
					break;
				case ImageResizeMode.FitSquare:
					procImage = FitSquareImage(img, scaledHeight, scaledWidth);
					break;
				default:
					Debug.Fail("Should not reach this");
					return null;
			}
			return procImage;
		}

		private Image FitImage(Image img, int scaledHeight, int scaledWidth) {
			int resizeWidth = 0;
			int resizeHeight = 0;
			if (this.Height == 0) {
				resizeWidth = this.Width;
				resizeHeight = scaledHeight;
			}
			else if (this.Width == 0) {
				resizeWidth = scaledWidth;
				resizeHeight = this.Height;
			}
			else {
				if (((float)this.Width / (float)img.Width < this.Height / (float)img.Height)) {
					resizeWidth = this.Width;
					resizeHeight = scaledHeight;
				}
				else {
					resizeWidth = scaledWidth;
					resizeHeight = this.Height;
				}
			}

			Bitmap newimage = new Bitmap(resizeWidth + 2 * _border, resizeHeight + 2 * _border);
			Graphics graphics = Graphics.FromImage(newimage);

			graphics.CompositingMode = CompositingMode.SourceCopy;
			graphics.CompositingQuality = CompositingQuality;
			graphics.InterpolationMode = InterpolationMode;
			graphics.SmoothingMode = SmoothingMode;

			graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, resizeWidth + 2 * _border, resizeHeight + 2 * _border));
			graphics.DrawImage(img, _border, _border, resizeWidth, resizeHeight);

	
			return newimage;
		}

		private Image FitSquareImage(Image img, int scaledHeight, int scaledWidth)
		{
			int resizeWidth = 0;
			int resizeHeight = 0;

			if (img.Height > img.Width)
			{
				resizeWidth = Convert.ToInt32((float)img.Width / (float)img.Height * this.Width);
				resizeHeight = this.Width;
			}
			else
			{
				resizeWidth = this.Width;
				resizeHeight = Convert.ToInt32((float)img.Height / (float)img.Width * this.Width);
			}

			Bitmap newimage = new Bitmap(this.Width + 2 * _border, this.Width + 2 * _border);
			
			Graphics graphics = Graphics.FromImage(newimage);
			graphics.CompositingMode = CompositingMode.SourceCopy;
			graphics.CompositingQuality = CompositingQuality;
			graphics.InterpolationMode = InterpolationMode;
			graphics.SmoothingMode = SmoothingMode;

			graphics.FillRectangle(new SolidBrush(BackColor),new Rectangle(0,0,this.Width + 2*_border ,this.Width + 2*_border));
			graphics.DrawImage(img, (this.Width - resizeWidth) / 2 + _border, (this.Width - resizeHeight) / 2 + _border, resizeWidth, resizeHeight);
			return newimage;
		}

		private Image CropImage(Image img, int scaledHeight, int scaledWidth) {
			int resizeWidth = 0;
			int resizeHeight = 0;
			if (((float)this.Width / (float)img.Width > this.Height / (float)img.Height)) {
				resizeWidth = this.Width;
				resizeHeight = scaledHeight;
			}
			else 
			{
				resizeWidth = scaledWidth;
				resizeHeight = this.Height;
			}

			Bitmap newImage = new Bitmap(this.Width, this.Height);
			
			Graphics graphics = Graphics.FromImage(newImage);
			graphics.CompositingMode = CompositingMode.SourceCopy;
			graphics.CompositingQuality = CompositingQuality;
			graphics.InterpolationMode = InterpolationMode;
			graphics.SmoothingMode = SmoothingMode;
			graphics.PixelOffsetMode = PixelOffsetMode;

			graphics.DrawImage(img, (this.Width - resizeWidth) / 2, (this.Height - resizeHeight) / 2, resizeWidth, resizeHeight);
			return newImage;
		}


		[Browsable(false)]
		public override string UniqueString {
			get {
				return base.UniqueString + Width + InterpolationMode.ToString() + Height + Mode.ToString();
			}
		}

		public override string ToString() {
			return "ImageResizeTransform";
		}
	}
}