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
		<h2>Report Help</h2><hr />
		    <p class="MsoNormal">
                This component will generate session history report, statistical report and 
                system utilisation report.<o:p></o:p></p>
            <p class="MsoNormal">
                <b>- History Report: <o:p></o:p></b>
            </p>
            <p class="MsoNormal">
                This report includes the detail of Poll Users participated each poll, the 
                geographical location of the poll on a google map and the dates and times the 
                polls were conducted. The reports will also show the details of the Poll Master 
                and Poll Creator that was assigned to each poll.<o:p></o:p></p>
            <p class="MsoNormal">
                <b>- Statistical Report: <o:p></o:p></b>
            </p>
            <p class="MsoNormal">
                Reports consists the raw data accumulation and also a graphical representation 
                in the form of a chart<o:p></o:p></p>
            <p class="MsoNormal">
                results by question, demographic comparison, session participation
<o:p></o:p>
            </p>
            <p class="MsoNormal">
                <b>- System Utilisation Report :<o:p></o:p></b></p>
            <p class="MsoNormal">
                The report includes information relating to all users of the system sorted by 
                access privilege. It will also detail the dates that each Poll Administrator 
                account was created<o:p></o:p></p>
            <p class="MsoNormal">
                <o:p></o:p></p>
		    <p>
		
    <img src="../../Content/ReportIndex.jpg" width="800" />
		
		</div> <!-- end of content div-->
	</div> <!-- end of contentwrapper div-->
</div> <!-- end of wrapper div -->

</body>
</html>
