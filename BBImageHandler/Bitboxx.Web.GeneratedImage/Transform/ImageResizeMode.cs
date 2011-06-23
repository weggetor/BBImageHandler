/* 
  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF 
  ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
  THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A 
  PARTICULAR PURPOSE. 
  
    This is sample code and is freely distributable. 
*/
namespace Bitboxx.Web.GeneratedImage.Transform
{
	public enum ImageResizeMode {
		/// <summary>
		/// Fit mode maintains the aspect ratio of the original image while ensuring that the dimensions of the result
		/// do not exceed the maximum values for the resize transformation.
		/// </summary>
		Fit,
		/// <summary>
		/// Crop resizes the image and removes parts of it to ensure that the dimensions of the result are exactly 
		/// as specified by the transformation.
		/// </summary>
		Crop,
		/// <summary>
		/// Resizes the image with the given width or height and maintains the aspect ratio. The image will be centered in a 
		/// square area of the chosen background color
		/// </summary>
		FitSquare

	}
}