<%@ WebHandler Language="C#" Class="BBImageHandler" %>

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web;
using System.IO;

using Bitboxx.Web.GeneratedImage;
using Bitboxx.Web.GeneratedImage.Transform;

public class BBImageHandler : ImageHandler {

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
		ImageCompression = 95;

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
        int imgIndex = 0; ;
        string imgPath = "";
        string imgFile = "";
		ContentType = ImageFormat.Jpeg;
		Color backColor;
    	
        try
        {
			// Lets determine the 3 types of Image Source
			if (!String.IsNullOrEmpty(parameters["File"]))
			{
				imgFile = parameters["File"].ToString();

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
			else if (String.IsNullOrEmpty(parameters["Url"]) && String.IsNullOrEmpty(parameters["db"]))
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
			
			if (string.IsNullOrEmpty(parameters["BackColor"]))	
				backColor = Color.White;
			else
			{
				string color = parameters["BackColor"];
				backColor = color.StartsWith("#") ? ColorTranslator.FromHtml(color) : Color.FromName(color);
			}
        }
        catch (Exception)
        {
            return EmptyImage;
        }

		// Db Transform
		if (!string.IsNullOrEmpty(parameters["Db"]))
		{
			ImageDbTransform dbTrans = new ImageDbTransform();

			dbTrans.InterpolationMode = InterpolationMode.HighQualityBicubic;
			dbTrans.PixelOffsetMode = PixelOffsetMode.HighQuality;
			dbTrans.SmoothingMode = SmoothingMode.HighQuality;
			dbTrans.CompositingQuality = CompositingQuality.HighQuality;

			ConnectionStringSettings conn = ConfigurationManager.ConnectionStrings[parameters["Db"]];
			
			if (conn == null || string.IsNullOrEmpty(parameters["Table"]) || string.IsNullOrEmpty(parameters["IdField"]) || 
				string.IsNullOrEmpty(parameters["IdValue"]) || string.IsNullOrEmpty(parameters["ImageField"]))
				return EmptyImage;
			dbTrans.ConnectionString = conn.ConnectionString;
			dbTrans.Table = parameters["Table"];
			dbTrans.IdFieldName = parameters["IdField"];
			dbTrans.IdFieldValue = Convert.ToInt32(parameters["IdValue"]);
			dbTrans.ImageFieldName = parameters["ImageField"];
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
				urlTrans.Ratio = (UrlRatioMode)Enum.Parse(typeof(UrlRatioMode), parameters["Ratio"], true);
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
	
		// Resize-Transformation
		if (!string.IsNullOrEmpty(parameters["Width"]) || !string.IsNullOrEmpty(parameters["Height"]))
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
				resizeTrans.Mode = (ImageResizeMode)Enum.Parse(typeof(ImageResizeMode), parameters["ResizeMode"]);

			if (!string.IsNullOrEmpty(parameters["Width"]))
			{
				resizeTrans.Width = Convert.ToInt32(parameters["Width"]);
				
			}
			if (!string.IsNullOrEmpty(parameters["Height"]))
			{
				resizeTrans.Height = Convert.ToInt32(parameters["Height"]);
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
				Type enumType = typeof(WatermarkPositionMode);	
				string pos = parameters["WatermarkPosition"];
				watermarkTrans.WatermarkPosition = (WatermarkPositionMode)Enum.Parse(enumType, pos, true);
			}
			if (!String.IsNullOrEmpty(parameters["WatermarkOpacity"]))
				watermarkTrans.WatermarkOpacity = Convert.ToInt32(parameters["WatermarkOpacity"]);
			
            ImageTransforms.Add(watermarkTrans);
        }
		if (imgFile == String.Empty)
		{
			Bitmap dummy = new Bitmap(1, 1, PixelFormat.Format24bppRgb);	
			MemoryStream ms = new MemoryStream();
			dummy.Save(ms,ImageFormat.Jpeg);
			return new ImageInfo(ms.ToArray());
		}
		else
		{
			return new ImageInfo(File.ReadAllBytes(imgFile));
		}
		
    }
}