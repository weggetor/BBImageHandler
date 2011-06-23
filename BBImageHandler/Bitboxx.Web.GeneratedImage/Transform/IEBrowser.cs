/* 
  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF 
  ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
  THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A 
  PARTICULAR PURPOSE. 
  
    This is sample code and is freely distributable. 
*/

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Bitboxx.Web.GeneratedImage.Transform
{
	public class IEBrowser : ApplicationContext
	{
		public Image Thumb; 
		private string _html;
		private UrlRatioMode _ratio;
		AutoResetEvent ResultEvent;

		public IEBrowser(string target,  UrlRatioMode ratio, AutoResetEvent resultEvent)
		{
			ResultEvent = resultEvent;
			Thread thrd = new Thread(new ThreadStart(
			                         	delegate {
			                         	         	Init(target,ratio);
			                         	         	System.Windows.Forms.Application.Run(this);
			                         	})); 
			// set thread to STA state before starting
			thrd.SetApartmentState(ApartmentState.STA);
			thrd.Start(); 
		}

		private void Init(string target,UrlRatioMode ratio)
		{
			// create a WebBrowser control
			WebBrowser ieBrowser = new WebBrowser();
			ieBrowser.ScrollBarsEnabled = false;
			ieBrowser.ScriptErrorsSuppressed = true;
        
			// set WebBrowser event handle
			ieBrowser.DocumentCompleted += IEBrowser_DocumentCompleted;

			_ratio = ratio;

			if (target.ToLower().StartsWith("http:"))
			{
				_html = "";
				ieBrowser.Navigate(target);
			}
			else
			{
				ieBrowser.Navigate("about:blank");
				_html = target;
			}
		}

		// DocumentCompleted event handle
		void IEBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			try
			{
				WebBrowser browser = (WebBrowser)sender;
				HtmlDocument doc = browser.Document;
				if (_html != string.Empty)
				{
					doc.OpenNew(true);
					doc.Write(_html);
				}
				switch (_ratio)
				{
					case UrlRatioMode.Full:
						browser.Width = doc.Body.ScrollRectangle.Width;
						browser.Height = doc.Body.ScrollRectangle.Height;
						break;
					case UrlRatioMode.Screen:
						browser.Width = doc.Body.ScrollRectangle.Width;
						browser.Height = Convert.ToInt32(browser.Width / 3 * 2);
						break;
					case UrlRatioMode.Cinema:
						browser.Width = doc.Body.ScrollRectangle.Width;
						browser.Height = Convert.ToInt32(browser.Width / 16 * 9);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
	
				Bitmap bitmap = new Bitmap(browser.Width, browser.Height);
				browser.DrawToBitmap(bitmap, new Rectangle(0, 0, browser.Width, browser.Height));
				browser.Dispose();
				Thumb = (Image) bitmap;
			}
			catch (System.Exception)
			{
			}
			finally
			{
				ResultEvent.Set();
			}

		}
	}
}