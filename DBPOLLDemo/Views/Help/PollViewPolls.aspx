<%@ Page Language="C#" Culture="en-AU" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>DBPOLL Help System</title>
<link href="../../Content/Help.css" rel="stylesheet" type="text/css" />
</head>


<body>
<div id="wrapper">

<div id="navwrapper">

	<div id="nav">
	
		<ul>
            
    
	    <li><a href="javascript:history.back();"><img src="../../Content/Back.jpg" alt="Back" title="Back" style="margin-right:40px;" /></a></li>
        <li><a href="<%= Url.Action("Index", "Home" ) %>"> <img src="<%= Url.Content("../../Content/Home.jpg") %>" alt="Home" title="Home"style="margin-right:40px;" /></a></li>
        <li><a href="<%= Url.Action("Site", "Help",new {idi="Site"} ) %>"> <img src="<%= Url.Content("../../Content/Index.jpg") %>" alt="Index Help" title="Index Help"style="margin-right:40px;" /></a></li>
        <li><a href="javascript:window.print()"><img src="../../Content/Print.jpg" alt="Print" title="Print" style="margin-right:40px;" /> </a></li>
        <li><a href="../../Content/Manual.pdf"><img src="../../Content/Manual.jpg" alt="Manual" title="Manual" style="margin-right:40px;" /> </a></li>
	</ul>
	
	</div> <!-- end of nav div -->
</div> <!-- end of navwrapper div -->
	<div id="contentwrapper">
		<div id="content">
		<a name="top"></a>
		<h2>ViewPolls Help</h2><hr />
		    <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">In this page user can search 
                polls and questions by date and time. By default all poll names and creation 
                dates will be displayed.<o:p></o:p></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Searching the polls by 
                date:<o:p></o:p></span></b></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Enter start date and end date 
                in the related fields (e.g. start date: 01/01/2009 </span>and end date: 
                01/01/2010)<span lang="EN-US" style="mso-ansi-language:EN-US">, then click <b>
                Search <span style="mso-spacerun:yes">&nbsp;</span></b><o:p></o:p></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Searching the polls by 
                time:<o:p></o:p></span></b></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Enter start date, end date 
                along with time in the related fields (e.g. start date: 01/01/2009 11:00 AM and</span><span 
                    lang="EN-US"> </span>end date: 01/01/2010 12:30 PM),<span 
                    style="mso-ansi-language:EN-US"> <span lang="EN-US">then click <b>Search
                <span style="mso-spacerun:yes">&nbsp;</span></b></span></span><o:p></o:p></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Displaying all 
                questions: <o:p></o:p></span></b>
            </p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">
                <span style="mso-spacerun:yes">&nbsp;</span>In <b>Actions</b>, Click on <b>Search All 
                Questions.<o:p></o:p></b></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Displaying the questions 
                of a poll:<o:p></o:p></span></b></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">
                <span style="mso-spacerun:yes">&nbsp;</span>In <b>Actions</b> Click on <b>Search 
                Questions</b></span><b><span dir="RTL" lang="EN-US" style="font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;mso-ascii-font-family:Calibri;
mso-ascii-theme-font:minor-latin;mso-hansi-font-family:Calibri;mso-hansi-theme-font:
minor-latin;mso-bidi-font-family:Arial;mso-bidi-theme-font:minor-bidi;
mso-ansi-language:EN-US"> </span></b><span lang="EN-US" style="mso-ansi-language:EN-US">which 
                belongs to a specific poll<b>.</b></span><span lang="EN-US" 
                    style="mso-ansi-language:EN-US;mso-bidi-language:FA"><o:p></o:p></span></p>
		<p> &nbsp;<p>
		
    <img src="../../Content/PollViewPolls.jpg" width="800" />
		
		</div> <!-- end of content div-->
	</div> <!-- end of contentwrapper div-->
</div> <!-- end of wrapper div -->

</body>
</html>
