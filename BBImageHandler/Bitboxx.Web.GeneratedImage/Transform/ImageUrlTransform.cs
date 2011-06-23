/* 
  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF 
  ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
  THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A 
  PARTICULAR PURPOSE. 
  
    This is sample code and is freely distributable. 
*/
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace Bitboxx.Web.GeneratedImage.Transform
{
	public class ImageUrlTransform : ImageTransform
	{
		/// <summary>
		/// Sets the Url. Defaultvalue is empty
		/// </summary>
		[DefaultValue("")]
		[Category("Behavior")]
		public string Url { get; set; }

		/// <summary>
		/// Sets the Url. Defaultvalue is empty
		/// </summary>
		[DefaultValue(UrlRatioMode.Full)]
		[Category("Behavior")]
		public UrlRatioMode Ratio { get; set; }

		public override string UniqueString
		{
			get
			{
				return base.UniqueString + "-" + this.Url + "-" + this.Ratio.ToString();
			}
		}

		public ImageUrlTransform()
		{
			InterpolationMode = InterpolationMode.HighQualityBicubic;
			SmoothingMode = SmoothingMode.Default;
			PixelOffsetMode = PixelOffsetMode.Default;
			CompositingQuality = CompositingQuality.HighSpeed;

		}

		public override Image ProcessImage(Image image)
		{
			AutoResetEvent resultEvent = new AutoResetEvent(false);
			IEBrowser browser = new IEBrowser(Url, Ratio, resultEvent);
			WaitHandle.WaitAll(new[] { resultEvent });
			return browser.Thumb;
		}
	}
}
