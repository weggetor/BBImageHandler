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
using System.IO;
using System.Threading;
using System.Web;
using Microsoft.ApplicationBlocks.Data;

namespace Bitboxx.Web.GeneratedImage.Transform
{
	public class ImageDbTransform : ImageTransform
	{
		/// <summary>
		/// Sets the Connectionstring Descriptor from web.config. Defaultvalue is empty
		/// </summary>
		[DefaultValue("")]
		[Category("Behavior")]
		public string ConnectionString { get; set; }

		/// <summary>
		/// Sets the Table to select from. Defaultvalue is empty
		/// </summary>
		[DefaultValue("")]
		[Category("Behavior")]
		public string Table { get; set; }

		/// <summary>
		/// Sets the ID Field name to select from. Defaultvalue is empty
		/// </summary>
		[DefaultValue("")]
		[Category("Behavior")]
		public string IdFieldName { get; set; }

		/// <summary>
		/// Sets the ID Field value to select from. Defaultvalue is empty
		/// </summary>
		[DefaultValue("")]
		[Category("Behavior")]
		public int IdFieldValue { get; set; }

        /// <summary>
        /// Sets the ID Field value to select from. Defaultvalue is empty
        /// </summary>
        [DefaultValue("")]
        [Category("Behavior")]
        public int UserId { get; set; }

        /// <summary>
        /// Sets the ID Field value to select from. Defaultvalue is empty
        /// </summary>
        [DefaultValue("")]
        [Category("Behavior")]
        public int PortalId { get; set; }

		/// <summary>
		/// Sets the Image Field name to select from. Defaultvalue is empty
		/// </summary>
		[DefaultValue("")]
		[Category("Behavior")]
		public string ImageFieldName { get; set; }

		public override string UniqueString
		{
			get
			{
				return base.UniqueString + "-" + this.ConnectionString + "-" + this.Table + "-" +
                       this.IdFieldName + "-" + this.IdFieldValue + "-" + this.ImageFieldName + "-" + this.UserId.ToString() + this.PortalId.ToString();
			}
		}

		public ImageDbTransform()
		{
			InterpolationMode = InterpolationMode.HighQualityBicubic;
			SmoothingMode = SmoothingMode.Default;
			PixelOffsetMode = PixelOffsetMode.Default;
			CompositingQuality = CompositingQuality.HighSpeed;
		}

		public override Image ProcessImage(Image image)
		{
		    string sqlCmd;
            
            if (UserId > -1)
            {
                sqlCmd = "SELECT RTRIM(Files.Folder) + RTRIM(LTRIM(Files.FileName)) AS profilepic"+
                         " FROM UserProfile " +
                         " INNER JOIN ProfilePropertydefinition on ProfilePropertydefinition.PropertyDefinitionID = UserProfile.PropertyDefinitionID" +
                         " INNER JOIN Files on UserProfile.PropertyValue = Files.FileId" +
                         " WHERE ProfilePropertydefinition.Propertyname = 'Photo'" +
                         " AND ProfilePropertydefinition.PortalId = " + this.PortalId.ToString() +
                         " AND Userprofile.UserId = " + this.UserId.ToString();
                object result = SqlHelper.ExecuteScalar(this.ConnectionString, CommandType.Text, sqlCmd);
                if (result != null)
                {
                    string imgFile = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "Portals\\" + this.PortalId.ToString());
                    imgFile = Path.Combine(imgFile, ((string) result).Replace('/', '\\'));
                    if (File.Exists(imgFile) == true)
                    {
                        return new Bitmap(imgFile);
                    }
                }
            }
            else
            {
                sqlCmd = "SELECT " + this.ImageFieldName + " FROM " +
                         this.Table + " WHERE " + this.IdFieldName + " = @Value";


		        object result = SqlHelper.ExecuteScalar(this.ConnectionString, CommandType.Text, sqlCmd, new SqlParameter("Value", this.IdFieldValue));
		        if (result != null)
		        {
		            MemoryStream ms = new MemoryStream((byte[]) result);
		            return Image.FromStream(ms);
		        }
		    }
            Bitmap emptyBmp = new Bitmap(1, 1, PixelFormat.Format1bppIndexed);
            emptyBmp.MakeTransparent();
            return emptyBmp;
		}
	}
}
