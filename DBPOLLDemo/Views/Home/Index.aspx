<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="DBPOLLDemo.Models" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    DBPOLL Home Page
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%= Html.Encode(ViewData["Message"])%></h2>
    <h2>
        Everyone can see</h2>
    
     <br />
    <%= Html.ActionLink("Answer Poll", "../Session/ViewAvailableSession", new { userid = ((USER)ViewData["User"]).USER_ID })%>
    <br />
    <%= Html.ActionLink("Change Password", "ChangePassword", "User")%>
    <br />
    <%= Html.ActionLink("My Messages", "Index", "Message")%>
    <br />
       <% Html.RenderPartial("../Message/sendMessage"); %>
    <hr />
    <hr />


    <h2>
        Here for development only</h2>
    <p>
        <%= Html.ActionLink("Edit Create and Delete Polls", "../Poll/Index")%>
        <br />
        <%= Html.ActionLink("View Polls and Questions", "../Poll/viewPolls")%>
        <br />
    </p>
    <hr />
  
    <h2>
        Poll Masters</h2>
    <% if (((USER)ViewData["User"]).USER_TYPE > 1)
           { %>
    <%= Html.ActionLink("Edit My Details", "Edit", "User")%>
    <br />
    <%= Html.ActionLink("Create New User", "RegisterUser", "User")%>
    <br />
    <%= Html.ActionLink("Reports", "Index", "Report")%><br />
    <br />
    <%= Html.ActionLink("Test Recievers", "../Poll/TestDevices")%>
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
    <% if (((USER)ViewData["User"]).USER_TYPE > 2)
           { %>
    <%--<% Html.RenderPartial("PollsImCreating"); %>--%>
    <% } %>



    <h2>
        Poll Administrators</h2>
    <% if (((USER)ViewData["User"]).USER_TYPE > 3)
           { %>
    <% } %>
</asp:Content>
