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
		<h2>Question Edit Help</h2><hr />
		    <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">In this page you can edit a 
                question by editing question type, question text, response chart and question in 
                sequence.<o:p></o:p></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Question Type: </span>
                </b><span lang="EN-US" style="mso-ansi-language:EN-US">
                <span style="mso-spacerun:yes">&nbsp;</span>You can choose of these options:<o:p></o:p></span></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Short Answer Numeric 
                Responses Only<o:p></o:p></span></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Short Answer Alphanumeric 
                Responses only <o:p></o:p></span>
            </p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Multiple Choice: Standard<o:p></o:p></span></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Multiple Choice: Demographic <o:p></o:p>
                </span>
            </p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Multiple Choice:<span 
                    style="mso-spacerun:yes">&nbsp; </span>Comparative<o:p></o:p></span></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Multiple Choice:
                <span style="mso-spacerun:yes">&nbsp;</span>Ranking<o:p></o:p></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Response Chart: </span>
                </b><span lang="EN-US" style="mso-ansi-language:EN-US">You can choose of these 
                options:<b><o:p></o:p></b></span></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Horizantal Bar Graph , 
                Vertical Bar Graph , Line Graph , Pie Chart<o:p></o:p></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Saving the question: <o:p></o:p>
                </span></b>
            </p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">After editing, clock on <b>
                Save Changes</b> <o:p></o:p>
                </span>
            </p>
            <p class="MsoNormal">
                <o:p></o:p>
            </p>
            <p>
		
    <img src="../../Content/QuestionEdit.jpg" width="800" />
		
		</div> <!-- end of content div-->
	</div> <!-- end of contentwrapper div-->
</div> <!-- end of wrapper div -->

</body>
</html>
