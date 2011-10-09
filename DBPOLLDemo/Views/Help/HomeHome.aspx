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
		<h2>Home Page Help</h2><hr />
		    <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">This is Home page of DBPOLL 
                application. These options are available in this page: <o:p></o:p></span>
            </p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">1-<b>Home</b>:<span 
                    style="mso-spacerun:yes">&nbsp; </span>Return to Home page.<o:p></o:p></span></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">2- <b>View Polls and 
                Questions</b>: the users can search Polls and Questions by date and time.
<o:p></o:p></span>
            </p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">3- <b>Edit, Create and Delete 
                Polls (Modify Polls): </b><span style="mso-spacerun:yes">&nbsp;</span></span>Authorised<span style="mso-ansi-language:
EN-US"> <span lang="EN-US">users have the ability to create, edit and delete polls and questions 
                which the Poll Users will reply to later and to optionally enter the 
                geographical location of a poll via Google map. <o:p></o:p></span></span>
            </p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">4<b>- Help:</b> This is 
                context sensitive user help that is available on every page and also it includes 
                index with hierarchical categories and PDF user manual that can be printed as 
                well.<br style="mso-special-character:line-break" />
                <![if !supportLineBreakNewLine]>
                <br style="mso-special-character:line-break" />
                <![endif]><o:p></o:p></span>
            </p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">5- <b>Reports:</b> it will 
                generate session history report, statistical report and system </span>
                utilisation<span style="mso-ansi-language:EN-US"> <span lang="EN-US">report.<o:p></o:p></span></span></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">6- <b>Test Receivers:</b> 
                this component tests receivers, RF Channels and keypads.<o:p></o:p></span></p>
		<p> &nbsp;<p>
		
    <img src="../../Content/HomeHome.jpg" width="800" />
		
		</div> <!-- end of content div-->
	</div> <!-- end of contentwrapper div-->
</div> <!-- end of wrapper div -->

</body>
</html>
