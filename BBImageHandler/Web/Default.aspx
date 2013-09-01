﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-family:Verdana,Arial,sans-serif;font-size:12px;">
    <form id="form1" runat="server">
    <div>
		<h1>Installation</h1>
		<p>Add the following to your web.config’s appSettings section:<br/>
		<span style="color: black; background: white;"><span style="color:blue;">&lt;</span><span style="color:maroon;">add</span>&nbsp;<span 
			style="color:red;">key</span><span style="color:blue;">=&quot;BBImageHandler&quot;</span>&nbsp;<span 
			style="color:red;">value</span><span style="color:blue;">=&quot;EnableClientCache=true;EnableServerCache=true;EnableSecurity=true;EnableSecurityExceptions=trueImageCompression=90;AllowedDomains=localhost,www.mydomain.com&quot;</span>&nbsp;<span 
			style="color:blue;">/&gt;</span>
		</span></p>
		<h3>Parameters:</h3>
		<ul>
			<li><strong>EnableClientCache</strong>: Set to true if client caching should be enabled</li>
			<li><strong>EnableServerCache</strong>: Set to true if server caching should be enabled</li>
			<li><strong>EnableSecurity</strong>: If set to true, only using bbimagehandler in a web page is allowed. Direct usage (eg: entering address in browser, no referer) is prohibited.  Also using it in a different domain is not possible (web page from http://www.domain-a.com uses http://www.domain-b.com/bbimagehandler.ashx...)</li>
			<li><strong>EnableSecurityExceptions</strong>: Set to true if security exceptions should be thrown</li>
			<li><strong>ImageCompression</strong>: quality of resulting image (0..100)</li> 
			<li><strong>AllowedDomains</strong>: Comma separated list of Domains that should be permitted to use the imagehandler. Use "domain.com" to permit including all subdomains an "www.domain.com" to allow only referers from this special subdomain</li>
		</ul>
		<hr/>
		<h1>Usage</h1>
		<h2>Original Image:</h2>
		<p>Image in original size: <br />
		<span style="color:blue;">&lt;</span><span 
				style="color:maroon;">img</span>&nbsp;<span style="color:red;">src</span><span 
				style="color:blue;">=&quot;Images/Winter.jpg&quot;</span>&nbsp;<span 
				style="color:blue;">/&gt;</span><br />
		</p>
		<img src="Winter.jpg" />
		<hr />
		<h2>Resizing Image:</h2>
		<p>Resized Image including width-Tag: <br />
		<span style="color:blue;">&lt;</span><span 
				style="color:maroon;">img</span>&nbsp;<span style="color:red;">src</span><span 
				style="color:blue;">=&quot;bbimagehandler.ashx?File=Winter.jpg&amp;width=150&quot;</span>&nbsp;<span 
				style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?File=Winter.jpg&width=150" />
		<p>Resized Image including height-Tag: <br />
		<span style="color:blue;">&lt;</span><span 
				style="color:maroon;">img</span>&nbsp;<span style="color:red;">src</span><span 
				style="color:blue;">=&quot;bbimagehandler.ashx?File=Winter.jpg&amp;height=150&quot;</span>&nbsp;<span 
				style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?File=Winter.jpg&height=150" />
		<p>Resized Image with Border, BackColor and ResizeMode: <br />
		<span style="color:blue;">&lt;</span><span 
				style="color:maroon;">img</span>&nbsp;<span style="color:red;">src</span><span 
				style="color:blue;">=&quot;bbimagehandler.ashx?File=Winter.jpg&amp;width=150&amp;ResizeMode=FitSquare&amp;BackColor=#F58719&amp;border=10&quot;</span>&nbsp;<span 
				style="color:blue;">/&gt;</span> (Escape &#39;#&#39; with &#39;%23&#39; in Url!)<br />
		</p>
		<img src="bbimagehandler.ashx?File=Winter.jpg&width=150&ResizeMode=FitSquare&BackColor=%23F58719&border=10" />
		<h3>Parameters:</h3>
		<ul>
			<li>
				<strong>width</strong>: width in pixel of resulting image</li>
			<li>
				<strong>height</strong>: height in pixel of resulting image</li>
			<li>
				<strong>resizemode</strong>:
				<ul>
					<li><strong>fit</strong>: Fit mode maintains the aspect ratio of the original image while ensuring that the dimensions of the result do not exceed the maximum values for the resize transformation. 
						(Needs <em>width</em> or <em>height</em> parameter)</li>
					<li><strong>fitsquare</strong>: Resizes the image with the given width as its 
						longest side (depending on image direction) and maintains the aspect ratio. The image will be centered in a square area of the chosen background color 
						(Needs <em>width</em> parameter, <em>backcolor</em> optional)</li>
					<li><strong>crop</strong>: Crop resizes the image and removes parts of it to ensure that the dimensions of the result are exactly as specified by the transformation.(Needs 
						<em>width</em> 
						and <em>height</em> parameter)</li>
				</ul>
			</li>
			<li><strong>backcolor</strong>:
				color of background or/and&nbsp; border when <em>resizemode</em> is <em>fitsquare</em> or
				f<em>it</em>.</li>
			<li><strong>border</strong>: border width in pixels around the image (added to width 
				/ height) when <em>resizemode</em> is f<em>itsquare</em> or f<em>it</em>.</li>
			<li><strong>nocache</strong>: If present, the globally defined cache settings in web.config are disabled and no caching takes place for this image. Add any value to enable nocache (e.g. nocache=1)</li>
		</ul>
		<hr/>
		<h2>Gamma correction</h2>
		<p><span style="color:blue;">&lt;</span><span style="color:maroon;">img</span>&nbsp;<span 
			style="color:red;">src</span><span style="color:blue;">=&quot;bbimagehandler.ashx?File=Winter.jpg&amp;height=150&amp;gamma=2.5&quot;</span>&nbsp;<span 
			style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?File=Winter.jpg&height=150&Gamma=2.5" />
		<h3>Parameters:</h3>
		<ul>
			<li><strong>Gamma</strong>: Value for gamma adjustment between 0.2 and 5</li>
		</ul>
		<hr/>
		<h2>Brightness correction</h2>
		<p><span style="color:blue;">&lt;</span><span style="color:maroon;">img</span>&nbsp;<span 
			style="color:red;">src</span><span style="color:blue;">=&quot;bbimagehandler.ashx?File=Winter.jpg&amp;height=150&amp;brightness=128&quot;</span>&nbsp;<span 
			style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?File=Winter.jpg&height=150&Brightness=128" />
		<h3>Parameters:</h3>
		<ul>
			<li><strong>Brightness</strong>: Value for brightness adjustment between -255 and +255</li>
		</ul>
		<hr/>
		<h2>Contrast correction</h2>
		<p><span style="color:blue;">&lt;</span><span style="color:maroon;">img</span>&nbsp;<span 
			style="color:red;">src</span><span style="color:blue;">=&quot;bbimagehandler.ashx?File=Winter.jpg&amp;height=150&amp;contrast=75&quot;</span>&nbsp;<span 
			style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?File=Winter.jpg&height=150&Contrast=75" />
		<h3>Parameters:</h3>
		<ul>
			<li><strong>Contrast</strong>: Value for contrast adjustment between -100 and +100</li>
		</ul>
		<hr />
		<h2>Greyscale</h2>
		<p><span style="color:blue;">&lt;</span><span style="color:maroon;">img</span>&nbsp;<span 
			style="color:red;">src</span><span style="color:blue;">=&quot;bbimagehandler.ashx?File=Winter.jpg&amp;height=150&amp;greyscale=1&quot;</span>&nbsp;<span 
			style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?File=Winter.jpg&height=150&Greyscale=1" />
		<h3>Parameters:</h3>
		<ul>
			<li><strong>Greyscale</strong>: Add any value</li>
		</ul>
		<hr />
		<h2>Invert</h2>
		<p><span style="color:blue;">&lt;</span><span style="color:maroon;">img</span>&nbsp;<span 
			style="color:red;">src</span><span style="color:blue;">=&quot;bbimagehandler.ashx?File=Winter.jpg&amp;height=150&amp;invert=1&quot;</span>&nbsp;<span 
			style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?File=Winter.jpg&height=150&Invert=1" />
		<h3>Parameters:</h3>
		<ul>
			<li><strong>Invert</strong>: Add any value</li>
		</ul>
		<hr />
		<h2>Rotate / Flip</h2>
		<p><span style="color:blue;">&lt;</span><span style="color:maroon;">img</span>&nbsp;<span 
			style="color:red;">src</span><span style="color:blue;">=&quot;bbimagehandler.ashx?File=Winter.jpg&amp;height=150&amp;rotateflip=RotateNoneFlipY&quot;</span>&nbsp;<span 
			style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?File=Winter.jpg&height=150&rotateflip=RotateNoneFlipY" />
		<h3>Parameters:</h3>
		<ul>
			<li><strong>RotateNoneFlipNone</strong>: Specifies no clockwise rotation and no flipping.</li>
			<li><strong>Rotate90FlipNone</strong>: Specifies a 90-degree clockwise rotation without flipping.</li>
			<li><strong>Rotate180FlipNone</strong>: Specifies a 180-degree clockwise rotation without flipping.</li>
			<li><strong>Rotate270FlipNone</strong>: Specifies a 270-degree clockwise rotation without flipping.</li>
			<li><strong>RotateNoneFlipX</strong>: Specifies no clockwise rotation followed by a horizontal flip.</li>
			<li><strong>Rotate90FlipX</strong>: Specifies a 90-degree clockwise rotation followed by a horizontal flip.</li>
			<li><strong>Rotate180FlipX</strong>: Specifies a 180-degree clockwise rotation followed by a horizontal flip.</li>
			<li><strong>Rotate270FlipX</strong>: Specifies a 270-degree clockwise rotation followed by a horizontal flip.</li>
			<li><strong>RotateNoneFlipY</strong>: Specifies no clockwise rotation followed by a vertical flip.</li>
			<li><strong>Rotate90FlipY</strong>: Specifies a 90-degree clockwise rotation followed by a vertical flip.</li>
			<li><strong>Rotate180FlipY</strong>: Specifies a 180-degree clockwise rotation followed by a vertical flip.</li>
			<li><strong>Rotate270FlipY</strong>: Specifies a 270-degree clockwise rotation followed by a vertical flip.</li>
			<li><strong>RotateNoneFlipXY</strong>: Specifies no clockwise rotation followed by a horizontal and vertical flip.</li>
			<li><strong>Rotate90FlipXY</strong>: Specifies a 90-degree clockwise rotation followed by a horizontal and vertical flip.</li>
			<li><strong>Rotate180FlipXY</strong>: Specifies a 180-degree clockwise rotation followed by a horizontal and vertical flip.</li>
			<li><strong>Rotate270FlipXY</strong>: Specifies a 270-degree clockwise rotation followed by a horizontal and vertical flip.</li>
		</ul>
		<hr/>
		<h2>Using watermarks:</h2>
		<p><span style="color:blue;">&lt;</span><span style="color:maroon;">img</span>&nbsp;<span 
			style="color:red;">src</span><span style="color:blue;">=&quot;bbimagehandler.ashx?File=Winter.jpg&amp;height=150&amp;watermarktext=watermark&amp;watermarkfontcolor=white&amp;watermarkposition=topleft&quot;</span>&nbsp;<span 
			style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?File=Winter.jpg&height=150&watermarktext=watermark&watermarkfontcolor=white&watermarkposition=topleft" />
		<h3>Parameters:</h3>
		<ul>
			<li><strong>watermarkposition</strong>: TopLeft, TopCenter, TopRight, CenterLeft, Center, CenterRight, BottomLeft, BottomCenter, BottomRight</li>
			<li><strong>watermarktext</strong>: The text to display as watermark</li>
			<li><strong>watermarkfontfamily</strong>: the font name used for watermarktext. (Default:Verdana)</li>
			<li><strong>watermarkfontcolor</strong>: color name or html-color with leading '#' (eg. 'red', '#F0F0F0') (Default:Black)</li>
			<li><strong>watermarkfontsize</strong>: font size (Default: 14)</li>
			<li><strong>watermarkopacity</strong>: grade of opacity (0..100) </li>
		</ul>
		<hr />
		<h2>Images stored in Database</h2>
		<p>If your image is stored in a database field (SqlDbType:Image), you can use the following syntax:<br />
		<span 
			style="color:blue;">&lt;</span><span style="color:maroon;">img</span>&nbsp;<span 
			style="color:red;">src</span><span style="color:blue;">=&quot;bbimagehandler.ashx?db=SiteSqlServer&amp;table=MyImages&amp;ImageField=ImageData&amp;idField=ImageID&amp;idValue=3&amp;height=150&quot;</span>&nbsp;<span 
			style="color:blue;">/&gt;</span>
		</p>
		<img src="bbimagehandler.ashx?db=SiteSqlServer&Table=MyImages&ImageField=ImageData&idField=ImageID&idValue=3&height=150" />
		<h3>Parameters:</h3>
		<ul>
			<li><strong>db</strong>: key of connectionstring section in web.config (for DNN normally 'SiteSqlServer')</li>
			<li><strong>table</strong>: Name of the table in database</li>
			<li><strong>imagefield</strong>: name of the image-field containing the image data</li>
			<li><strong>idfield</strong>: name of the field containing the primary key (must be integer-id!)</li>
			<li><strong>idvalue</strong>: value of id</li>
		</ul>
		<p style="border:3px #a31515 solid; padding:10px;">
			<strong>Exposing connection strings (even just by name), database names, table names, ID 
			field names, and ID field values may be a security concern in internet 
			scenarios. It doesn&#39;t open up a direct attack vector but exposes more to the 
			public than necessary. </strong> </p>
		<p>
			Alternatively you can add the needed infos in your 
			web.config in the appsettings section:</p>
			<span style="color: black;"><span style="color:blue;">&lt;</span><span style="color:#a31515;">add</span><span style="color:blue;">&nbsp;</span><span style="color:red;">key</span>
			<span style="color:blue;">=</span>&quot;<span style="color:blue;">BBDatabase</span>&quot;<span style="color:blue;">&nbsp;</span><span style="color:red;">value</span>
			<span style="color:blue;">=</span>&quot;<span style="color:blue;">Connectionstring=SiteSqlServer;table=MyImages;ImageField=ImageData;idField=ImageID</span>&quot;
			<span style="color:blue;">/&gt;</span></span>
		<p>
			with this defined in web.config your link has much less information for 
			attackers:</p>
		<span style="color: black; background: white;"><span 
			style="color:blue;">&lt;</span><span style="color:maroon;">img</span>&nbsp;<span 
			style="color:red;">src</span><span style="color:blue;">=&quot;bbimagehandler.ashx?db=BBDatabase&amp;idValue=3&amp;height=150&quot;</span>&nbsp;<span 
			style="color:blue;">/&gt;</span>&nbsp;</span>
		<h3>Parameters:</h3>
		<ul>
			<li><strong>db</strong>: key of appsetting entry in web.config (e.g. "BBDatabase")</li>
			<li><strong>idvalue</strong>: value of id</li>
		</ul>
		<hr />
		<h2>Webpage Thumbnail:</h2>
		<p>Url Thumbnail of Webpage: <br />
		<span style="color:blue;">&lt;</span><span 
				style="color:maroon;">img</span>&nbsp;<span style="color:red;">src</span><span 
				style="color:blue;">=&quot;bbimagehandler.ashx?Url=http:\\www.ebay.de&amp;width=250&amp;ratio=screen&quot;</span>&nbsp;<span 
				style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?Url=http:\\www.ebay.de&ratio=screen&width=250&Format=png" />
		<h3>Parameters:</h3>
		<ul>
			<li><strong>url</strong>: Url of the Web page from which the thumbnail should be done</li>
			<li><strong>ratio</strong>:
				<ul>
					<li><strong>full</strong>: Make a thumb of the full web page (Default)</li>
					<li><strong>screen</strong>: Make a thumb with dimensions 3:2 (cut of rest)</li>
					<li><strong>cinema</strong>: Make a thumb with dimensions 16:9 (cut of rest)</li>
				</ul>
			</li>
		</ul>
		<hr />
		<h2>Counter</h2>
		<p>Using as counter:<br />
		<span style="color:blue;">&lt;</span><span style="color:maroon;">img</span>&nbsp;<span 
			style="color:red;">src</span><span style="color:blue;">=&quot;bbimagehandler.ashx?File=Counter.gif&amp;digits=8&amp;counter=32477&quot;</span>&nbsp;<span 
			style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?File=Counter.gif&digits=8&counter=32477" />
		<h3>Parameters:</h3>
		<ul>
			<li><strong>File</strong>: Must be special counter image file with digits 0 to 9 with similar width per digit: <img src="counter.gif" height="14px"/></li>
			<li><strong>digits</strong>: No of digits. </li>
			<li><strong>counter</strong>: Value to display</li>
		</ul>
		<hr />
		<h2>Percentage</h2>
		<p>Using as radial percentage indicator:<br />
		<span style="color:blue;">&lt;</span><span style="color:maroon;">img</span>&nbsp;<span 
			style="color:red;">src</span><span style="color:blue;">=&quot;bbimagehandler.ashx?Percentage=40&amp;backcolor=green&quot;</span>&nbsp;<span 
			style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?Percentage=40&backcolor=green" />
		<h3>Parameters:</h3>
		<ul>
			<li><strong>percentage</strong>: Percentage value </li>
			<li><strong>backcolor</strong>: Color of percentage indicator</li>
		</ul>
		<hr />
		<h2>Placeholder</h2>
		<p>Generates a placeholder image:<br />
		<span style="color:blue;">&lt;</span><span style="color:maroon;">img</span>&nbsp;<span 
			style="color:red;">src</span><span style="color:blue;">=&quot;bbimagehandler.ashx?placeholder=1&amp;width=150&amp;height=100&amp;color=green&amp;backcolor=lightgreen&quot;</span>&nbsp;<span 
			style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?Placeholder=1&width=150&Height=100&Color=green&backcolor=lightgreen" />
		<h3>Parameters:</h3>
		<ul>
			<li><strong>Placeholder</strong>: Any value (e.g. Placeholder=1)</li>
			<li><strong>Width</strong>: Width of resulting image (this or Height must be defined)</li>
			<li><strong>Height</strong>: Height of resulting image (this or Width must be defined)</li>
			<li><strong>Color</strong>: Color of text and border (Default: dark grey, optional)</li>
			<li><strong>BackColor</strong>: Backcolor of Image (Default:light grey, optional)</li>
			<li><strong>Text</strong>: Text to show on image. If not used, image dimensions will be shown</li>
		</ul>
    	<p>
			&nbsp;</p>
    </div>
    </form>
</body>
</html>