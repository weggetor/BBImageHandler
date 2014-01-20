using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Security;
using System.Web;
using System.IO;
using Bitboxx.Web.GeneratedImage.Transform;

namespace Bitboxx.Web.GeneratedImage
{
	public class BBImageHandler : ImageHandler
	{

		private ImageInfo EmptyImage
		{
			get
			{
				Bitmap emptyBmp = new Bitmap(1, 1, PixelFormat.Format1bppIndexed);
				emptyBmp.MakeTransparent();
				ContentType = ImageFormat.Png;
				return new ImageInfo(emptyBmp);
			}
		}

		public BBImageHandler()
		{
			// Set settings here
			EnableClientCache = true;
			EnableServerCache = true;
			EnableSecurity = true;
			EnableSecurityExceptions = true;
			ImageCompression = 95;
			AllowedDomains = new string[] {"localhost"};

			try
			{
				string settings = ConfigurationManager.AppSettings["BBImageHandler"];
				if (!String.IsNullOrEmpty(settings))
				{
					string[] values = settings.Split(';');
					foreach (string value in values)
					{
						string[] setting = value.Split('=');
						string name = setting[0].ToLower();
						switch (name)
						{
							case "enableclientcache":
								EnableClientCache = Convert.ToBoolean(setting[1]);
								break;
							case "enableservercache":
								EnableServerCache = Convert.ToBoolean(setting[1]);
								break;
							case "enablesecurity":
								EnableSecurity = Convert.ToBoolean(setting[1]);
								break;
							case "imagecompression":
								ImageCompression = Convert.ToInt32(setting[1]);
								break;
							case "alloweddomains":
								AllowedDomains = setting[1].Split(',');
								break;
							case "enablesecurityexceptions":
								EnableSecurityExceptions = Convert.ToBoolean(setting[1]);
								break;
							default:
								break;
						}
					}
				}
			}
			catch (System.Exception)
			{
			}
		}

		public override ImageInfo GenerateImage(NameValueCollection parameters)
		{
			// Add image generation logic here and return an instance of ImageInfo
			int imgIndex = 0;
			string imgPath = "";
			string imgFile = "";
			ContentType = ImageFormat.Jpeg;
			Color backColor = Color.White; 

			try
			{
				// Do we override caching for this image ?
				if (!String.IsNullOrEmpty(parameters["NoCache"]))
				{
					EnableClientCache = false;
					EnableServerCache = false;
				}

				// Lets determine the 3 types of Image Source
				if (!String.IsNullOrEmpty(parameters["File"]))
				{
					imgFile = parameters["File"].Trim();

					if (File.Exists(imgFile) != true)
					{
						imgFile = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, imgFile);
						if (File.Exists(imgFile) != true)
							return EmptyImage;
					}
				}
				else if (!String.IsNullOrEmpty(parameters["Path"]))
				{
					imgIndex = Convert.ToInt32(parameters["Index"]);
					imgPath = parameters["Path"];

					if (Directory.Exists(imgPath) != true)
					{
						imgPath = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, imgPath);
						if (Directory.Exists(imgPath) != true)
							return EmptyImage;
					}

					string[] Files = Directory.GetFiles(imgPath, "*.*");
					if (Files.Length > 0 && Files.Length - 1 >= imgIndex)
					{
						Array.Sort(Files);
						imgFile = Files[imgIndex];
						if (File.Exists(imgFile) != true)
							return EmptyImage;
					}
				}
				else if (String.IsNullOrEmpty(parameters["Url"]) &&
				         String.IsNullOrEmpty(parameters["db"]) &&
				         String.IsNullOrEmpty(parameters["percentage"]) &&
				         String.IsNullOrEmpty(parameters["placeholder"]) &&
                         String.IsNullOrEmpty(parameters["barcode"]))
				{
					return EmptyImage;
				}

				// We need to determine the output format		
				if (!String.IsNullOrEmpty(parameters["Format"]))
				{
					string format = parameters["Format"].ToLower();
					switch (format)
					{
						case "jpg":
						case "jpeg":
							ContentType = ImageFormat.Jpeg;
							break;
						case "bmp":
							ContentType = ImageFormat.Bmp;
							break;
						case "gif":
							ContentType = ImageFormat.Gif;
							break;
						case "png":
							ContentType = ImageFormat.Png;
							break;
						default:
							return EmptyImage;
					}
				}
				else if (imgFile != string.Empty)
				{
					FileInfo fi = new FileInfo(imgFile);
					switch (fi.Extension.ToLower())
					{
						case ".jpg":
							ContentType = ImageFormat.Jpeg;
							break;
						case ".gif":
							ContentType = ImageFormat.Gif;
							break;
						case ".png":
							ContentType = ImageFormat.Png;
							break;
						default:
							return EmptyImage;
					}
				}

				if (!string.IsNullOrEmpty(parameters["BackColor"]))
				{
					string color = parameters["BackColor"];
					backColor = color.StartsWith("#") ? ColorTranslator.FromHtml(color) : Color.FromName(color);
				}
			}
			catch (SecurityException)
			{
				if (EnableSecurityExceptions)
					throw;
			}
			catch (Exception)
			{
				return EmptyImage;
			}

			// Db Transform
			if (!string.IsNullOrEmpty(parameters["Db"]))
			{
				//First let us check if the Db value is a key or a connectionstring name

				string settings = ConfigurationManager.AppSettings[parameters["Db"]];
				string connectionstring = "", table = "", imageField = "", idField = "";
				if (!String.IsNullOrEmpty(settings))
				{
					string[] values = settings.Split(';');
					foreach (string value in values)
					{
						string[] setting = value.Split('=');
						string name = setting[0].ToLower();
						switch (name)
						{
							case "connectionstring":
								connectionstring = setting[1];
								break;
							case "table":
								table = setting[1];
								break;
							case "imagefield":
								imageField = setting[1];
								break;
							case "idfield":
								idField = setting[1];
								break;
							default:
								break;
						}
					}
				}
			    int userId = -1;
			    if (!string.IsNullOrEmpty(parameters["userid"]))
			        userId = Convert.ToInt32(parameters["userid"]);

			    int portalId = -1;
                if (!string.IsNullOrEmpty(parameters["portalid"]))
                    portalId = Convert.ToInt32(parameters["portalid"]);

				ImageDbTransform dbTrans = new ImageDbTransform();

				dbTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
				dbTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
				dbTrans.SmoothingMode = SmoothingMode.HighQuality;
				dbTrans.CompositingQuality = CompositingQuality.HighQuality;

				if (userId < 0 && (connectionstring == string.Empty || (table == string.Empty || imageField == string.Empty || idField == string.Empty )) || 
                    (userId >= 0  && connectionstring == string.Empty))
				{
					connectionstring = parameters["Db"];
					table = parameters["Table"];
					imageField = parameters["ImageField"];
					idField = parameters["IdField"];
				}
				
				ConnectionStringSettings conn = ConfigurationManager.ConnectionStrings[connectionstring];

				if (conn == null || 
                    ((string.IsNullOrEmpty(table) || string.IsNullOrEmpty(idField) || string.IsNullOrEmpty(parameters["IdValue"]) || string.IsNullOrEmpty(imageField)) && string.IsNullOrEmpty(parameters["userid"]))) 
					return EmptyImage;
				dbTrans.ConnectionString = conn.ConnectionString;
				dbTrans.Table = table;
				dbTrans.IdFieldName = idField;
				dbTrans.IdFieldValue = Convert.ToInt32(parameters["IdValue"]);
				dbTrans.ImageFieldName = imageField;
                dbTrans.UserId = userId;
			    dbTrans.PortalId = portalId;
				ImageTransforms.Add(dbTrans);
			}


			// Url Transform
			if (!string.IsNullOrEmpty(parameters["Url"]))
			{
				ImageUrlTransform urlTrans = new ImageUrlTransform();

				urlTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
				urlTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
				urlTrans.SmoothingMode = SmoothingMode.HighQuality;
				urlTrans.CompositingQuality = CompositingQuality.HighQuality;

				urlTrans.Url = parameters["Url"];
				if (!String.IsNullOrEmpty(parameters["Ratio"]))
					urlTrans.Ratio = (UrlRatioMode) Enum.Parse(typeof (UrlRatioMode), parameters["Ratio"], true);
				else
					urlTrans.Ratio = UrlRatioMode.Full;
				ImageTransforms.Add(urlTrans);
			}

			// Counter Transform
			if (!string.IsNullOrEmpty(parameters["Counter"]))
			{
				ImageCounterTransform counterTrans = new ImageCounterTransform();

				counterTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
				counterTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
				counterTrans.SmoothingMode = SmoothingMode.HighQuality;
				counterTrans.CompositingQuality = CompositingQuality.HighQuality;

				if (!String.IsNullOrEmpty(parameters["Counter"]))
					counterTrans.Counter = Convert.ToInt32(parameters["Counter"]);
				if (!String.IsNullOrEmpty(parameters["Digits"]))
					counterTrans.Digits = Convert.ToInt32(parameters["Digits"]);
				ImageTransforms.Add(counterTrans);
			}

			// Radial Indicator
			if (!string.IsNullOrEmpty((parameters["percentage"])))
			{
				ImagePercentageTransform percentTrans = new ImagePercentageTransform();
				
				percentTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
				percentTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
				percentTrans.SmoothingMode = SmoothingMode.HighQuality;
				percentTrans.CompositingQuality = CompositingQuality.HighQuality;

				if (!String.IsNullOrEmpty(parameters["Percentage"]))
					percentTrans.Percentage = Convert.ToInt32(parameters["Percentage"]);
				if (!String.IsNullOrEmpty(parameters["BackColor"]))
					percentTrans.Color = backColor;
				else
					percentTrans.Color = Color.Orange;

				ImageTransforms.Add(percentTrans);

			}

            // Barcode 
		    if (!string.IsNullOrEmpty((parameters["barcode"])))
		    {
		        ImageBarcodeTransform barcodeTrans = new ImageBarcodeTransform();

		        barcodeTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
		        barcodeTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
		        barcodeTrans.SmoothingMode = SmoothingMode.HighQuality;
		        barcodeTrans.CompositingQuality = CompositingQuality.HighQuality;
		        barcodeTrans.Border = 0;
		        barcodeTrans.Width = 100;
                barcodeTrans.Height = 100;

		        if (!String.IsNullOrEmpty(parameters["type"]) && "upca,ean8,ean13,code39,code128,itf,codabar,plessey,msi,qrcode,pdf417,aztec,datamatrix,".LastIndexOf(parameters["type"].ToLower() + ",") > -1)
		        {
		            barcodeTrans.Type = parameters["type"].ToLower();
		        }
                if (!String.IsNullOrEmpty(parameters["content"]))
                {
                    barcodeTrans.Content = parameters["content"];
                }
                if (!string.IsNullOrEmpty(parameters["Width"]))
                {
                    barcodeTrans.Width = Convert.ToInt32(parameters["Width"]);
                }
                if (!string.IsNullOrEmpty(parameters["Height"]))
                {
                    barcodeTrans.Height = Convert.ToInt32(parameters["Height"]);
                }
                if (!string.IsNullOrEmpty(parameters["Border"]))
                {
                    barcodeTrans.Border = Convert.ToInt32(parameters["Border"]);
                }
                ImageTransforms.Add(barcodeTrans);
		    }

		    // Resize-Transformation (only if not placeholder or barcode)
            if (string.IsNullOrEmpty(parameters["placeholder"]) && string.IsNullOrEmpty(parameters["barcode"]) &&
                (!string.IsNullOrEmpty(parameters["Width"]) || !string.IsNullOrEmpty(parameters["Height"]) || 
                 (!string.IsNullOrEmpty(parameters["MaxWidth"]) || !string.IsNullOrEmpty(parameters["MaxHeight"]))))
			{
				ImageResizeTransform resizeTrans = new ImageResizeTransform();
				resizeTrans.Mode = ImageResizeMode.Fit;
				resizeTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
				resizeTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
				resizeTrans.SmoothingMode = SmoothingMode.HighQuality;
				resizeTrans.CompositingQuality = CompositingQuality.HighQuality;
				resizeTrans.BackColor = backColor;

				// Parameter 'Mode' is obsolete. New is 'ResizeMode'	
				if (!string.IsNullOrEmpty(parameters["Mode"]))
					resizeTrans.Mode = (ImageResizeMode) Enum.Parse(typeof (ImageResizeMode), parameters["Mode"]);

				if (!string.IsNullOrEmpty(parameters["ResizeMode"]))
					resizeTrans.Mode = (ImageResizeMode) Enum.Parse(typeof (ImageResizeMode), parameters["ResizeMode"]);

				if (!string.IsNullOrEmpty(parameters["Width"]))
				{
					resizeTrans.Width = Convert.ToInt32(parameters["Width"]);
				}
				if (!string.IsNullOrEmpty(parameters["Height"]))
				{
					resizeTrans.Height = Convert.ToInt32(parameters["Height"]);
				}
                if (!string.IsNullOrEmpty(parameters["MaxWidth"]))
                {
                    resizeTrans.MaxWidth = Convert.ToInt32(parameters["MaxWidth"]);
                }
                if (!string.IsNullOrEmpty(parameters["MaxHeight"]))
                {
                    resizeTrans.MaxHeight = Convert.ToInt32(parameters["MaxHeight"]);
                }
				if (!string.IsNullOrEmpty(parameters["Border"]))
				{
					resizeTrans.Border = Convert.ToInt32(parameters["Border"]);
				}
				ImageTransforms.Add(resizeTrans);
			}

			// Watermark Transform
			if (!string.IsNullOrEmpty(parameters["WatermarkText"]))
			{
				ImageWatermarkTransform watermarkTrans = new ImageWatermarkTransform();

				watermarkTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
				watermarkTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
				watermarkTrans.SmoothingMode = SmoothingMode.HighQuality;
				watermarkTrans.CompositingQuality = CompositingQuality.HighQuality;

				watermarkTrans.WatermarkText = parameters["WatermarkText"];
				if (!String.IsNullOrEmpty(parameters["WatermarkFontFamily"]))
					watermarkTrans.FontFamily = parameters["WatermarkFontFamily"];
				if (!String.IsNullOrEmpty(parameters["WatermarkFontColor"]))
				{
					string color = parameters["WatermarkFontColor"];
					watermarkTrans.FontColor = color.StartsWith("#") ? ColorTranslator.FromHtml(color) : Color.FromName(color);
				}
				if (!String.IsNullOrEmpty(parameters["WatermarkFontSize"]))
					watermarkTrans.FontSize = Convert.ToSingle(parameters["WatermarkFontSize"]);
				if (!String.IsNullOrEmpty(parameters["WatermarkPosition"]))
				{
					Type enumType = typeof (WatermarkPositionMode);
					string pos = parameters["WatermarkPosition"];
					watermarkTrans.WatermarkPosition = (WatermarkPositionMode) Enum.Parse(enumType, pos, true);
				}
				if (!String.IsNullOrEmpty(parameters["WatermarkOpacity"]))
					watermarkTrans.WatermarkOpacity = Convert.ToInt32(parameters["WatermarkOpacity"]);

				ImageTransforms.Add(watermarkTrans);
			}

			// Gamma adjustment
			if (!string.IsNullOrEmpty(parameters["Gamma"]))
			{
				ImageGammaTransform gammaTrans = new ImageGammaTransform();
				gammaTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
				gammaTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
				gammaTrans.SmoothingMode = SmoothingMode.HighQuality;
				gammaTrans.CompositingQuality = CompositingQuality.HighQuality;
				double gamma = 1;
				if (Double.TryParse(parameters["Gamma"], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out gamma) && (gamma >= 0.2 && gamma <= 5))
				{
					gammaTrans.Gamma = gamma;
					ImageTransforms.Add(gammaTrans);
				}
			}

			// Brightness adjustment
			if (!string.IsNullOrEmpty(parameters["Brightness"]))
			{
				ImageBrightnessTransform brightnessTrans = new ImageBrightnessTransform();
				brightnessTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
				brightnessTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
				brightnessTrans.SmoothingMode = SmoothingMode.HighQuality;
				brightnessTrans.CompositingQuality = CompositingQuality.HighQuality;
				int brightness = 0;
				if (Int32.TryParse(parameters["Brightness"], out brightness))
				{
					brightnessTrans.Brightness = brightness;
					ImageTransforms.Add(brightnessTrans);
				}
			}

			// Contrast adjustment
			if (!string.IsNullOrEmpty(parameters["Contrast"]))
			{
				ImageContrastTransform contrastTrans = new ImageContrastTransform();
				contrastTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
				contrastTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
				contrastTrans.SmoothingMode = SmoothingMode.HighQuality;
				contrastTrans.CompositingQuality = CompositingQuality.HighQuality;
				double contrast = 0;
				if (Double.TryParse(parameters["Contrast"], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out contrast) && (contrast >= -100 && contrast <= 100))
				{
					contrastTrans.Contrast = contrast;
					ImageTransforms.Add(contrastTrans);
				}
			}

			// Greyscale
			if (!string.IsNullOrEmpty(parameters["Greyscale"]))
			{
				ImageGreyScaleTransform greyscaleTrans = new ImageGreyScaleTransform();
				greyscaleTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
				greyscaleTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
				greyscaleTrans.SmoothingMode = SmoothingMode.HighQuality;
				greyscaleTrans.CompositingQuality = CompositingQuality.HighQuality;
				ImageTransforms.Add(greyscaleTrans);
			}

			// Invert
			if (!string.IsNullOrEmpty(parameters["Invert"]))
			{
				ImageInvertTransform invertTrans = new ImageInvertTransform();
				invertTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
				invertTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
				invertTrans.SmoothingMode = SmoothingMode.HighQuality;
				invertTrans.CompositingQuality = CompositingQuality.HighQuality;
				ImageTransforms.Add(invertTrans);
			}

			// Rotate / Flip 
			if (!string.IsNullOrEmpty(parameters["RotateFlip"]))
			{
				ImageRotateFlipTransform rotateFlipTrans = new ImageRotateFlipTransform();
				rotateFlipTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
				rotateFlipTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
				rotateFlipTrans.SmoothingMode = SmoothingMode.HighQuality;
				rotateFlipTrans.CompositingQuality = CompositingQuality.HighQuality;
				RotateFlipType rotateFlipType = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), parameters["RotateFlip"]);
				rotateFlipTrans.RotateFlip = rotateFlipType;
				ImageTransforms.Add(rotateFlipTrans);
				
			}

			// Placeholder 
			if (!string.IsNullOrEmpty(parameters["placeholder"]))
			{
				ImagePlaceholderTransform placeHolderTrans = new ImagePlaceholderTransform();
				placeHolderTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
				placeHolderTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
				placeHolderTrans.SmoothingMode = SmoothingMode.HighQuality;
				placeHolderTrans.CompositingQuality = CompositingQuality.HighQuality;

				int width = 0, height = 0;
				if (Int32.TryParse(parameters["Width"], out width))
					placeHolderTrans.Width = width;
				if (Int32.TryParse(parameters["Height"], out height))
					placeHolderTrans.Height = height;
				if (!string.IsNullOrEmpty(parameters["Color"]))
				{
					string color = parameters["Color"];
					placeHolderTrans.Color = color.StartsWith("#") ? ColorTranslator.FromHtml(color) : Color.FromName(color);
				}
				if (!string.IsNullOrEmpty(parameters["Text"]))
					placeHolderTrans.Text = parameters["Text"];
				if (!string.IsNullOrEmpty(parameters["BackColor"]))
				{
					string color = parameters["BackColor"];
					placeHolderTrans.BackColor = color.StartsWith("#") ? ColorTranslator.FromHtml(color) : Color.FromName(color);
				}
				ImageTransforms.Add(placeHolderTrans);
			}

			if (imgFile == String.Empty)
			{
				Bitmap dummy = new Bitmap(1, 1, PixelFormat.Format24bppRgb);
				MemoryStream ms = new MemoryStream();
				dummy.Save(ms, ImageFormat.Jpeg);
				return new ImageInfo(ms.ToArray());
			}
			else
			{
				return new ImageInfo(File.ReadAllBytes(imgFile));
			}

		}
	}
}