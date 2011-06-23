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

namespace Bitboxx.Web.GeneratedImage.Transform
{
    /// <summary>
    /// An abstract ImageTransform class
    /// </summary>
    public abstract class ImageTransform {

		/// <summary>
		/// Sets the interpolation mode used for resizing images. The default is HighQualityBicubic.
		/// </summary>
		[Category("Behavior")]
		public InterpolationMode InterpolationMode { get; set; }

		/// <summary>
		/// Sets the smoothing mode used for resizing images. The default is Default.
		/// </summary>
		[Category("Behavior")]
		public SmoothingMode SmoothingMode { get; set; }

		/// <summary>
		/// Sets the pixel offset mode used for resizing images. The default is Default.
		/// </summary>
		[Category("Behavior")]
		public PixelOffsetMode PixelOffsetMode { get; set; }

		/// <summary>
		/// Sets the compositing quality used for resizing images. The default is HighSpeed.
		/// </summary>
		[Category("Behavior")]
		public CompositingQuality CompositingQuality { get; set; }

		public abstract Image ProcessImage(Image image);
        
        // REVIEW: should this property be abstract?
        [Browsable(false)]
        public virtual string UniqueString {
            get {
                return GetType().FullName;
            }
        }
    }
}
