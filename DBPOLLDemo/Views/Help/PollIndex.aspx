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
		<h2>Poll Index Help</h2><hr />
            <p class="MsoNormal">
                In this Page authorised users have the ability to create, edit and delete polls. 
                By Default poll names and Creation Date will be displayed.
            </p>
            <p class="MsoNormal">
                -  <b>Deleting/Editing a poll: </b>
            </p>
            <p class="MsoNormal">
                In <b>Poll Index </b>click on <b>Delete/Edit</b> for a poll that you want to 
                delete/edit
            </p>
            <p class="MsoNormal">
                - <b>Viewing, editing questions: </b>
            </p>
            <p class="MsoNormal">
                In <b>Poll Index</b> click on <b>View Questions</b>, in the next page you are 
                able to Delete, Edit or Create Questions, as well as View Answers and Objects
            </p>
            <p class="MsoNormal">
                - <b>Creating a new session :</b></p>
            <p class="MsoNormal">
                In <b>Poll Index</b> click on <b>Create New Session</b> for a specific poll
            </p>
            <p class="MsoNormal">
                - <b>Delete/Edit a session:</b></p>
            <p class="MsoNormal">
                &nbsp;In <b>Session Index</b> click on <b>Delete/Edit</b> for a specific session
            </p>
            <p class="MsoNormal">
                - <b>Assigning poll masters:</b></p>
            <p class="MsoNormal">
                In <b>Poll Index </b>click on <b>Assign Poll Masters </b>
            </p>
            <p class="MsoNormal">
                - <b>Creating/Editing participant list :</b></p>
            <p class="MsoNormal">
                In <b>Session Index</b> click on <b>Create/Edit participant list</b for 
                specific session
            </p>
            <p>
		
    <img src="../../Content/PollIndex.jpg" width="800" />
		
		</div> <!-- end of content div-->
	</div> <!-- end of contentwrapper div-->
</div> <!-- end of wrapper div -->

</body>
</html>
