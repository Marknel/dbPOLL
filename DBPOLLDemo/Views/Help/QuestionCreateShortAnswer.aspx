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
		<h2>Create Shortanswer Help</h2><hr />
		    <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">In this page the user can 
                create a short answer question for a poll.<o:p></o:p></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Short Answer Type:
                </span></b><span lang="EN-US" style="mso-ansi-language:EN-US">
                <span style="mso-spacerun:yes">&nbsp;</span>choose of these options:<o:p></o:p></span></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Short Answer Numeric 
                Responses Only , Short Answer Alphanumeric Responses only <o:p></o:p></span>
            </p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Number of Question:<o:p></o:p></span></b></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Enter number of question<o:p></o:p></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Question Text:<o:p></o:p></span></b></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Enter a question text<o:p></o:p></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Response Chart: </span>
                </b><span lang="EN-US" style="mso-ansi-language:EN-US">choose of these options:<b><o:p></o:p></b></span></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Horizantal Bar Graph , 
                Vertical Bar Graph , Line Graph , Pie Chart<o:p></o:p></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Creating the question:<o:p></o:p></span></b></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Click on Create Question<o:p></o:p></span></p>
		<p> &nbsp;<p>
		
    <img src="../../Content/QuestionCreateShortAnswer.jpg" width="800" />
		
		</div> <!-- end of content div-->
	</div> <!-- end of contentwrapper div-->
</div> <!-- end of wrapper div -->

</body>
</html>
