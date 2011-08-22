<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>DBPOLL Help System</title>
<link href="../../Content/Help.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: medium;
        }
        .style2
        {
            font-weight: bold;
        }
        .style3
        {
            font-family: Arial;
        }
    </style>
</head>


<body>
<div id="wrapper">

<div id="navwrapper">

	<div id="nav">
	
		<ul>
            
    
	    <li><a href="javascript:history.back();"><img src="../../Content/Back.jpg" alt="Back" title="Back" style="margin-right:40px;" /></a></li>
        <li><a href="<%= Url.Action("Home", "Home" ) %>"> <img src="<%= Url.Content("../../Content/Home.jpg") %>" alt="Home" title="Home"style="margin-right:40px;" /></a></li>
        <li><a href="<%= Url.Action("Site", "Help",new {idi="Site"} ) %>"> <img src="<%= Url.Content("../../Content/Index.jpg") %>" alt="Index Help" title="Index Help"style="margin-right:40px;" /></a></li>
        <li><a href="javascript:window.print()"><img src="../../Content/Print.jpg" alt="Print" title="Print" style="margin-right:40px;" /> </a></li>
        <li><a href="../../Content/Manual.pdf"><img src="../../Content/Manual.jpg" alt="Manual" title="Manual" style="margin-right:40px;" /> </a></li>
	</ul>
	
	</div> <!-- end of nav div -->
</div> <!-- end of navwrapper div -->
	<div id="contentwrapper">
		<div id="content">
		<a name="top"></a>
		<h2>Answer Edit</h2><hr />
		    <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">In this page the users are 
                able to edit an answer.<o:p></o:p></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Answer text:</span></b><span 
                    lang="EN-US" style="mso-ansi-language:EN-US"><span style="mso-spacerun:yes">&nbsp;
                </span>Enter answer text <b>
                <o:p>
                </o:p>
                </b></span>
            </p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Correct Answer:<span 
                    style="mso-spacerun:yes">&nbsp; </span></span></b>
                <span lang="EN-US" style="mso-ansi-language:
EN-US">choose whether this answer is correct or not<o:p></o:p></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Answer Weight:</span></b><span 
                    lang="EN-US" style="mso-ansi-language:EN-US"> <span style="mso-spacerun:yes">&nbsp;</span>Enter answer weight<b><o:p></o:p></b></span></p>
            <p>
                <span lang="EN-US" style="line-height:115%;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;mso-hansi-theme-font:minor-latin;mso-bidi-font-family:Arial;
mso-bidi-theme-font:minor-bidi;mso-ansi-language:EN-US;mso-fareast-language:
EN-US;mso-bidi-language:AR-SA" class="style2">- Save: </span>
                <span lang="EN-US" style="line-height:115%;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;mso-hansi-theme-font:minor-latin;mso-bidi-font-family:Arial;
mso-bidi-theme-font:minor-bidi;mso-ansi-language:EN-US;mso-fareast-language:
EN-US;mso-bidi-language:AR-SA" class="style3">to save the edit , click on</span><span 
                    lang="EN-US" style="line-height:115%;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;mso-hansi-theme-font:minor-latin;mso-bidi-font-family:Arial;
mso-bidi-theme-font:minor-bidi;mso-ansi-language:EN-US;mso-fareast-language:
EN-US;mso-bidi-language:AR-SA" class="style1"><b> Save </b></span>
            </p>
		<p> &nbsp;<p>
		
    <img src="../../Content/AnswerEdit.jpg" width="800" />
		
		</div> <!-- end of content div-->
	</div> <!-- end of contentwrapper div-->
</div> <!-- end of wrapper div -->

</body>
</html>
