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
		<h2>Question Index Help</h2><hr />
		    <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">In this page the users are 
                able to create, edit or delete the questions as well as answers and objects .the 
                questions along with question numbers, question type and creation date will be 
                displayed by default. <o:p></o:p></span>
            </p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Creating New Question:<o:p></o:p></span></b></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">Click on the <b>Create New 
                Question,</b> and then in the next page you are able to create short answer ror 
                multiple choices questions.<o:p></o:p></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Delete a question:<o:p></o:p></span></b></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">In <b>Actions</b> click on <b>
                Delete </b>for a question that you want to delete<o:p></o:p></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- Edit a Question: <o:p></o:p>
                </span></b>
            </p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">In <b>Actions</b> click on <b>
                Delete </b>for a question that you want to edit<o:p></o:p></span></p>
            <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- View answers:<o:p></o:p></span></b></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">In <b>Actions</b> click on <b>
                View answers </b>for a question, then in the next page you can view,edit or 
                create answers.<o:p></o:p></span></p>
		    <p class="MsoNormal">
                <b><span lang="EN-US" style="mso-ansi-language:EN-US">- View Objects:<o:p></o:p></span></b></p>
            <p class="MsoNormal">
                <span lang="EN-US" style="mso-ansi-language:EN-US">In <b>Actions</b> click on <b>
                View Objects</b> for a question, then in the next page you can view, edit or 
                create Objects.<o:p></o:p></span></p>
            <p class="MsoNormal">
                <o:p></o:p></p>
		    <p>
		
    <img src="../../Content/QuestionIndex.jpg" width="800" />
		
		</div> <!-- end of content div-->
	</div> <!-- end of contentwrapper div-->
</div> <!-- end of wrapper div -->

</body>
</html>
