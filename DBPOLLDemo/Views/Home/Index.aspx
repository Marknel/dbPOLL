<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="DBPOLLDemo.Models" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    DBPOLL Home Page
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%= Html.Encode(ViewData["Message"])%></h2>
<<<<<<< HEAD
    <h3>
        Account</h3>
    <% if ((int)Session["user_type"] > 1)
       { %>
    <%= Html.ActionLink("Edit My Details", "Edit", "User")%>
    <br />
=======
    <h2>
        Everyone can see</h2>
    
     <br />
    <%= Html.ActionLink("Answer Poll", "../Session/ViewAvailableSession", new { userid = ((USER)ViewData["User"]).USER_ID })%>
>>>>>>> 2a0ee0666a8817a3d8d7d3df9a0fce0399566447
    <br />
    <% } %>
    <%= Html.ActionLink("Change Password", "ChangePassword", "User")%>
    <br />
    <br />
    <%= Html.ActionLink("My Messages", "Index", "Message")%>
    <br />
    <br />
    <hr />
<<<<<<< HEAD
    <br />
    <%= Html.ActionLink("Answer Poll", "../Session/ViewAvailableSession", new { userid = ((USER)ViewData["User"]).USER_ID })%>
    <%-- <h2>
        Poll Masters</h2>--%>
    <% if ((int)Session["user_type"] > 1)
       { %>
    <br />
=======
  
    <h2>
        Poll Masters</h2>
    <% if (((USER)ViewData["User"]).USER_TYPE > User_Type.POLL_USER)
           { %>
    <%= Html.ActionLink("Edit My Details", "Edit", "User")%>
>>>>>>> 2a0ee0666a8817a3d8d7d3df9a0fce0399566447
    <br />
    <%= Html.ActionLink("Create New User", "RegisterUser", "User")%>
    <br />
    <br />
    <hr />
    <h3>
        Administer Poll</h3>
    <%= Html.ActionLink("View Polls and Questions", "viewPolls", "Poll")%>
    <%--<%= Html.ActionLink("View Polls and Questions", "../Poll/viewPolls")%>--%>
    <br />
    <br />
    <%= Html.ActionLink("Edit Create and Delete Polls", "Index", "Poll")%>
    <br />
    <br />
    <% if ((int)Session["user_type"] > 2)
       { %>
    <%= Html.ActionLink("Create Questions", "Create", "Question")%>
    <br />
    <br />
<<<<<<< HEAD
    <% } %>
    <%--   <h2>
        Poll Administrators</h2>--%>
    <% if ((int)Session["user_type"] > 3)
       { %>
    <%= Html.ActionLink("Define new Poll", "Create", "Poll")%>
    <br />
    <br />
    <% } %>
    <hr />
    <br />
    <%= Html.ActionLink("Reports", "Index", "Report")%>
    <br />
    <br />
=======
    <br />
    <%= Html.ActionLink("Run Keypad Poll", "../Poll/RunDevices")%>
    <br />
   

   <p>
    Building Participant List
    Administering Polls
    Generate statistical reports + Graphs
   </p>
    <% } %>


    <%--<% Html.RenderPartial("PollsIManage"); %>
    <% Html.RenderPartial("PollsToDo"); %>--%>



    <h2>
        Poll Creators</h2>
    <% if (((USER)ViewData["User"]).USER_TYPE > User_Type.POLL_MASTER)
           { %>
    <%--<% Html.RenderPartial("PollsImCreating"); %>--%>
    <% } %>



    <h2>
        Poll Administrators</h2>
    <% if (((USER)ViewData["User"]).USER_TYPE > User_Type.POLL_CREATOR)
           { %>
>>>>>>> 2a0ee0666a8817a3d8d7d3df9a0fce0399566447
    <% } %>
    <%--  <h2>
        Poll Creators</h2>--%>
    <%= Html.ActionLink("Test Recievers", "../Poll/TestDevices")%>
</asp:Content>
