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
using ZXing;
using ZXing.Common;

namespace Bitboxx.Web.GeneratedImage.Transform
{
	public class ImageBarcodeTransform : ImageTransform
	{
		/// <summary>
		/// Sets the barcode type 
        /// (upca,ean8,ean13,code39,code128,itf,codabar,plessey,msi,qrcode,pdf417,aztec,datamatrix)
		/// </summary>
		[DefaultValue("")]
		[Category("Behavior")]
		public string Type { get; set; }

        /// <summary>
        /// Sets the barcode content 
        /// (upca,ean8,ean13,code39,code128,itf,codabar,plessey,msi,qrcode,pdf417,aztec,datamatrix)
        /// </summary>
        [DefaultValue("")]
        [Category("Behavior")]
        public string Content { get; set; }

		/// <summary>
		/// Sets the Width of the generated barcode
		/// </summary>
		[DefaultValue("")]
		[Category("Behavior")]
		public int Width { get; set; }

        /// <summary>
        /// Sets the Height of the generated barcode
        /// </summary>
        [DefaultValue("")]
        [Category("Behavior")]
        public int Height { get; set; }

        /// <summary>
        /// Sets the Border Width (not pixels, depends on barcode type)
        /// </summary>
        [DefaultValue("")]
        [Category("Behavior")]
        public int Border { get; set; }

		public override string UniqueString
		{
			get { return base.UniqueString + this.Type + "-" + this.Width.ToString() + "-" + this.Height.ToString() + this.Content + "-" + this.Border.ToString(); }
		}

        public ImageBarcodeTransform()
		{
			InterpolationMode = InterpolationMode.HighQualityBicubic;
			SmoothingMode = SmoothingMode.Default;
			PixelOffsetMode = PixelOffsetMode.Default;
			CompositingQuality = CompositingQuality.HighSpeed;
		}

		public override Image ProcessImage(Image image)
		{
		    BarcodeWriter barcodeWriter = new BarcodeWriter();
		    switch (Type)
		    {
                case "upca":
                    barcodeWriter.Format = BarcodeFormat.UPC_A;
		            break;
                case "ean8":
                    barcodeWriter.Format = BarcodeFormat.EAN_8;
		            break;
                case "ean13":
                    barcodeWriter.Format = BarcodeFormat.EAN_13;
		            break;
                case "code39":
                    barcodeWriter.Format = BarcodeFormat.CODE_39;
		            break;
                case "code128":
                    barcodeWriter.Format = BarcodeFormat.CODE_128;
		            break;
                case "itf":
                    barcodeWriter.Format = BarcodeFormat.ITF;
		            break;
                case "codabar":
                    barcodeWriter.Format = BarcodeFormat.CODABAR;
		            break;
                case "plessey":
                    barcodeWriter.Format = BarcodeFormat.PLESSEY;
		            break;
                case "msi":
                    barcodeWriter.Format = BarcodeFormat.MSI;
		            break;
                case "qrcode":
                    barcodeWriter.Format = BarcodeFormat.QR_CODE;
		            break;
                case "pdf417":
                    barcodeWriter.Format = BarcodeFormat.PDF_417;
		            break;
                case "aztec":
                    barcodeWriter.Format = BarcodeFormat.AZTEC;
		            break;
                case "datamatrix":
                    barcodeWriter.Format = BarcodeFormat.DATA_MATRIX;
		            break;
		    }
		    barcodeWriter.Options = new EncodingOptions
		                            {
		                                Height = Height,
		                                Width = Width,
		                                Margin = Border
		                            };

		    Bitmap bitmap = barcodeWriter.Write(Content);
		    return (Image) bitmap;
		}
	}
}
