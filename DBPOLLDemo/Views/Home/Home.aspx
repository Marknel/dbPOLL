<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="DBPOLLDemo.Models" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    DBPOLL Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <% if (ViewData["sysadmin"] == "false")
       { %>
        <h2><%= Html.Encode(ViewData["Message"])%></h2>
        <p>
        <%= Html.ActionLink("Edit Create and Delete Polls", "../Poll/Index")%> <br />
        <%= Html.ActionLink("View Polls and Questions", "../Poll/viewPolls")%> <br />    
        </p>
        <hr />
        <% if (((USER)ViewData["User"]).USER_TYPE > 1)
           { %>
            <%= Html.ActionLink("Create New User", "New", "User")%>
        <% } %>

        <br />
        <%= Html.ActionLink("Edit My Details", "Edit", "User")%>
        <br />
        <%= Html.ActionLink("Change Password", "Change_Password", "User")%>
        <br />
        <%= Html.ActionLink("My Messages", "Index", "Messages")%>
        <br />
        <% if (((USER)ViewData["User"]).USER_TYPE > 1)
           { %>
            <%= Html.ActionLink("Reports", "Index", "Report")%><br />
            <%= Html.ActionLink("Test Recievers", "../Poll/TestDevices")%> <br />
        <% } %>
        <% Html.RenderPartial("PollsToDo"); %>

        <% if (((USER)ViewData["User"]).USER_TYPE > 1)
           { %>
            <% Html.RenderPartial("PollsIManage"); %>
        <% } %>

        <% if (((USER)ViewData["User"]).USER_TYPE > 2)
           { %>
            <% Html.RenderPartial("PollsImCreating"); %>
        <% } %>
    
        <% if (((USER)ViewData["User"]).USER_TYPE > 3)
           { %>
        
        <% } %>
    <% } else { %>
        <%= Html.ActionLink("Continue to your control panel", "Index", "SysAdmin") %>
    <% } %>

</asp:Content>
