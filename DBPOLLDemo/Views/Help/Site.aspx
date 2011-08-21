<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
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
		<h2>Index Help</h2><hr />
          <form id="form1" runat="server">

      <h3>TreeView Declarative Syntax Example</h3>
      <a href="file.pdf"><img src="help_book-icon.gif" alt="Manual" title="Manual" /></a>

      <a href="javascript:window.print()"><img src="help_book-icon.gif" alt="Print" title="Print"/> </a>
      
      
      <a href="Site.aspx">Site.aspx</a>
     
<a href="QuestionCreateMultipleChoice.aspx">QuestionCreateMultipleChoice.aspx</a>


      <asp:TreeView id="SampleTreeView" 
        runat="server">
        
        <Nodes>
            
          <asp:TreeNode Value="Home" 
            NavigateUrl="/Help/Site?idi=HomeHome" 
            Text="  Getting Started"
            Target="Content" 
            Expanded="True" ImageUrl="../../Content/help_manual.png">

            <asp:TreeNode Value="Page 1" 
              NavigateUrl="/Help/Site?idi=HomeHome" 
              Text="Homepage Help"
              Target="Content" Expanded="False" ImageUrl="../../Content/help_manual.png"> 
              

              <asp:TreeNode Value="Section 1" 
                NavigateUrl="http://www.google.com" 
                Text="Section 1"
                Target="Content"/>

            </asp:TreeNode>              

            <asp:TreeNode Value="Page 2" 
              NavigateUrl="http://www.google.com"
              Text="Page 2"
              Target="Content">

            </asp:TreeNode> 

          </asp:TreeNode>

        </Nodes>

      </asp:TreeView>

    </form>
       
       </div> <!-- end of content div-->
	</div> <!-- end of contentwrapper div-->
</div> <!-- end of wrapper div -->

</body>
</html>
