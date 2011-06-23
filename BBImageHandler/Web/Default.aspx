<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-family:Verdana,Arial,sans-serif;font-size:12px;">
    <form id="form1" runat="server">
    <div>
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
				color of background or/and&nbsp; border when <em>resizemode</em> is f<em>itsquare</em> or
				f<em>it</em>.</li>
			<li><strong>border</strong>: border width in pixels around the image (added to width 
				/ height) when <em>resizemode</em> is f<em>itsquare</em> or f<em>it</em>.</li>
		</ul>

		<hr />
		<h2>Using watermarks: 
		</h2>
		<p><span style="color:blue;">&lt;</span><span style="color:maroon;">img</span>&nbsp;<span 
			style="color:red;">src</span><span style="color:blue;">=&quot;bbimagehandler.ashx?File=Winter.jpg&amp;height=150&amp;watermarktext=watermark&amp;watermarkfontcolor=white&amp;watermarkposition=topleft&quot;</span>&nbsp;<span 
			style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?File=Winter.jpg&height=150&watermarktext=watermark&watermarkfontcolor=white&watermarkposition=topleft" />
		<h3>Parameters:</h3>
		<ul>
			<li><strong>watermarkposition</strong>: TopLeft,	TopCenter, TopRight, CenterLeft, Center, CenterRight, BottomLeft, BottomCenter, BottomRight</li>
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
		<hr />
		<h2>Webpage Thumbnail:</h2>
		<p>Url Thumbnail of Webpage: <br />
		<span style="color:blue;">&lt;</span><span 
				style="color:maroon;">img</span>&nbsp;<span style="color:red;">src</span><span 
				style="color:blue;">=&quot;bbimagehandler.ashx?Url=http:\\www.ebay.de&amp;width=250&amp;ratio=screen&quot;</span>&nbsp;<span 
				style="color:blue;">/&gt;</span><br />
		</p>
		<img src="bbimagehandler.ashx?Url=http:\\www.ebay.de&ratio=screen&width=250" />
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
    </div>
    </form>
</body>
</html>
