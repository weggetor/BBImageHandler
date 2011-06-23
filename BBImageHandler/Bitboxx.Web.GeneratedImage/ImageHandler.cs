using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Security;
using System.Web;
using Bitboxx.Web.GeneratedImage.Transform;

namespace Bitboxx.Web.GeneratedImage
{
    public abstract class ImageHandler : IHttpHandler {
        private ImageHandlerInternal Implementation { get; set; }

        /// <summary>
        /// Enables server-side caching of the result
        /// </summary>
        public bool EnableServerCache {
            get {
                return Implementation.EnableServerCache;
            }
            set {
                Implementation.EnableServerCache = value;
            }
        }

        /// <summary>
        /// Enables client-side caching of the result
        /// </summary>
        public bool EnableClientCache {
            get {
                return Implementation.EnableClientCache;
            }
            set {
                Implementation.EnableClientCache = value;
            }
        }

        /// <summary>
        /// Sets the client-side cache expiration time
        /// </summary>
        public TimeSpan ClientCacheExpiration {
            get {
                return Implementation.ClientCacheExpiration;
            }
            set {
                Implementation.ClientCacheExpiration = value;
            }
        }

        /// <summary>
        /// Sets the type of the result image. The handler will return ouput with MIME type matching this content
        /// </summary>
        public ImageFormat ContentType {
            get {
                return Implementation.ContentType;
            }
            set {
                Implementation.ContentType = value;
            }
        }

		/// <summary>
		/// Sets the image compression encoding for the result image. Default is 50L
		/// </summary>
		public long ImageCompression
		{
			get
			{
				return Implementation.ImageCompression;
			}
			set
			{
				Implementation.ImageCompression = value;
			}
		}

    	public bool EnableSecurity = true;

        /// <summary>
        /// A list of image transforms that will be applied successively to the image
        /// </summary>
        protected List<ImageTransform> ImageTransforms {
            get {
                return Implementation.ImageTransforms;
            }
        }

        protected ImageHandler()
            : this(new ImageHandlerInternal()) {
        }

        private ImageHandler(ImageHandlerInternal implementation) {
            Implementation = implementation;
        }

        internal ImageHandler(IImageStore imageStore, DateTime now)
            : this(new ImageHandlerInternal(imageStore, now)) {
        }

        public abstract ImageInfo GenerateImage(NameValueCollection parameters);

        public virtual bool IsReusable {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context) {
			if (context == null) 
			{
                throw new ArgumentNullException("context");
            }
			else if (EnableSecurity && context.Request.Url.Host != "localhost")
			{
				if (context.Request.UrlReferrer == null)
				{
					throw new SecurityException("not allowed to use standalone (only localhost)");
				}
				if (context.Request.Url.Host != context.Request.UrlReferrer.Host)
				{
					throw new SecurityException("not allowed to use from other domain (only localhost)");
				}
			}
        	HttpContextBase contextWrapper = new HttpContextWrapper(context);
            ProcessRequest(contextWrapper);
        }

        internal void ProcessRequest(HttpContextBase context) 
		{
            Debug.Assert(context != null);
            Implementation.HandleImageRequest(context, delegate(NameValueCollection queryString) 
			{
                return GenerateImage(queryString);
            }, this.ToString());
        }
    }
}
