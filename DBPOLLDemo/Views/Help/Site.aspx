<%@ Page Language="C#" Culture="en-AU" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    protected void SampleTreeView_SelectedNodeChanged(object sender, EventArgs e)
    {

    }
</script>
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
		<h2>Index Help</h2><hr />
          <form id="form1" runat="server">


      <asp:TreeView id="SampleTreeView" 
        runat="server" onselectednodechanged="SampleTreeView_SelectedNodeChanged">
        
        <Nodes>
            
          <asp:TreeNode Value="HomeHome" 
            NavigateUrl="/Help/Site?idi=HomeHome" 
            Text=" Getting Started"
           
            Expanded="True" ImageUrl="../../Content/help_manual.png">

            <asp:TreeNode Value=" Poll Component" 
              NavigateUrl="/Help/Site?idi=Pollindex" 
              Text="  Poll Help"
              Expanded="False" ImageUrl="../../Content/help_manual.png"> 
              
            <asp:TreeNode Value=" Pollindex" 
              NavigateUrl="/Help/Site?idi=Pollindex" 
              Text="  Poll Index "
              Expanded="False" ImageUrl="../../Content/help_manual.png"/> 


              <asp:TreeNode Value="PollView" 
                NavigateUrl="/Help/Site?idi=PollViewPolls"
                Text=" Poll View " ImageUrl="../../Content/help_manual.png"/>  

              <asp:TreeNode Value="SessionCreate" 
                NavigateUrl="/Help/Site?idi=SessionCreate"
                Text=" Session Create" ImageUrl="../../Content/help_manual.png"/>  
  
              <asp:TreeNode Value="SessionEdit" 
                NavigateUrl="/Help/Site?idi=SessionEdit"
                Text=" Session Edit " ImageUrl="../../Content/help_manual.png"/>  

              <asp:TreeNode Value="PollAssignPoll" 
                NavigateUrl="/Help/Site?idi=PollAssignPoll"
                Text=" Assign Poll " ImageUrl="../../Content/help_manual.png"/>  

            </asp:TreeNode>              

            <asp:TreeNode Value="Question Component" 
              NavigateUrl="/Help/Site?idi=Questionindex"
              Text=" Question Help" 
              Expanded="False" ImageUrl="../../Content/help_manual.png" >

            <asp:TreeNode Value="Question Index" 
              NavigateUrl="/Help/Site?idi=Questionindex"
              Text=" Question Index" 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />

            <asp:TreeNode Value="QuestionviewQuestions" 
              NavigateUrl="/Help/Site?idi=QuestionviewQuestions"
              Text=" Question viewQuestions" 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />

            <asp:TreeNode Value="QuestionEdit" 
              NavigateUrl="/Help/Site?idi=QuestionEdit"
              Text=" Question Edit " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />
     
            <asp:TreeNode Value="QuestionCreate" 
              NavigateUrl="/Help/Site?idi=QuestionCreate"
              Text=" Question Create " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />

            <asp:TreeNode Value="QuestionCreateMultipleChoice" 
              NavigateUrl="/Help/Site?idi=QuestionCreateMultipleChoice"
              Text=" Question CreateMultiple Choice " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />
    
    
            <asp:TreeNode Value="QuestionCreateShortAnswer" 
              NavigateUrl="/Help/Site?idi=QuestionCreateShortAnswer"
              Text=" Question Create ShortAnswer " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />

              </asp:TreeNode>    

          
            <asp:TreeNode Value="Answer" 
              NavigateUrl="/Help/Site?idi=AnswerIndex"
              Text=" Answer Help " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" >



            <asp:TreeNode Value="AnswerIndex" 
              NavigateUrl="/Help/Site?idi=AnswerIndex"
              Text=" Answer Index " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />
        
            <asp:TreeNode Value="AnswerEdit" 
              NavigateUrl="/Help/Site?idi=AnswerEdit"
              Text=" Answer Edit " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />

              </asp:TreeNode>    

            <asp:TreeNode Value="ObjectIndex" 
              NavigateUrl="/Help/Site?idi=ObjectIndex"
              Text=" Object Help " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" >


            <asp:TreeNode Value="ObjectIndex" 
              NavigateUrl="/Help/Site?idi=ObjectIndex"
              Text=" Object Index " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />

              </asp:TreeNode>    

            <asp:TreeNode Value="ReportIndex" 
              NavigateUrl="/Help/Site?idi=ReportIndex"
              Text=" Report Help " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" >

            <asp:TreeNode Value="ReportIndex" 
              NavigateUrl="/Help/Site?idi=ReportIndex"
              Text=" ReportIndex " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />
             
                         
              
            <asp:TreeNode Value="ReportSessionHistoryReport" 
              NavigateUrl="/Help/Site?idi=ReportSessionHistoryReport"
              Text=" Session History Report " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />
                
              
            <asp:TreeNode Value="ReportOneStatisticalReport" 
              NavigateUrl="/Help/Site?idi=ReportOneStatisticalReport"
              Text=" Statistical Report " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />

            <asp:TreeNode Value="ReportSessionParticipation" 
              NavigateUrl="/Help/Site?idi=ReportSessionParticipation"
              Text=" Session Participation Report " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />

              </asp:TreeNode>   
                

            <asp:TreeNode Value="PollTestDevices" 
              NavigateUrl="/Help/Site?idi=PollTestDevices "
              Text="  Test Devices Help " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" >

            <asp:TreeNode Value="PollTestDevices" 
              NavigateUrl="/Help/Site?idi=PollTestDevices "
              Text="  Test Devices " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />
              </asp:TreeNode>   
              
            <asp:TreeNode Value="PollRunDevices" 
              NavigateUrl="/Help/Site?idi=PollRunDevices "
              Text="  Run Devices Help " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" >

            <asp:TreeNode Value="PollRunDevices" 
              NavigateUrl="/Help/Site?idi=PollRunDevices "
              Text="  Run Devices " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />
              </asp:TreeNode>

            <asp:TreeNode Value="UserRegisterUser" 
              NavigateUrl="/Help/Site?idi=UserRegisterUser "
              Text="  Account Help " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" >

            <asp:TreeNode Value="UserRegisterUser" 
              NavigateUrl="/Help/Site?idi=UserRegisterUser "
              Text="  Register User " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />

            <asp:TreeNode Value="UserEdit" 
              NavigateUrl="/Help/Site?idi=UserEdit "
              Text="  User Edit " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />

            <asp:TreeNode Value="UserChangePassword" 
              NavigateUrl="/Help/Site?idi=UserChangePassword "
              Text="   Change Password " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />
              </asp:TreeNode>   
          
            <asp:TreeNode Value="Message" 
              NavigateUrl="/Help/Site?idi=Message "
              Text="  Message Help " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" >

            <asp:TreeNode Value="Message" 
              NavigateUrl="/Help/Site?idi=Message "
              Text="  Message " 
              Expanded="False" ImageUrl="../../Content/help_manual.png" />
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
