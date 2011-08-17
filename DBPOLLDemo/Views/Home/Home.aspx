<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="DBPOLLDemo.Models" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    DBPOLL Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <p>
    <%= Html.ActionLink("Edit Create and Delete Polls", "../Main/Index") %> <br />
    <%= Html.ActionLink("View Polls and Questions", "../Main/viewPolls")%>
    </p>

    <h1>MARKNEL's STUFF</h1>
    <h2>Poll User Stuff</h2>
    <button>edit user link</button>
    <button>change password link</button>
    <button>messages</button>
    <% Html.RenderPartial("PollsToDo"); %>

    <% if (((USER)ViewData["User"]).USER_TYPE > 1) { %>
        <h2>Poll Master Stuff</h2>
        <button>Create new Poll User</button>
        <h3>Polls I Manage</h3>
        <% Html.RenderPartial("PollsIManage"); %>
    <% } %>

    <% if (((USER)ViewData["User"]).USER_TYPE > 2) { %>
        <h2>Poll Creator Stuff</h2>
        <button>Create new Poll Manager</button>
        <h3>Polls I'm Creating</h3>
        <% Html.RenderPartial("PollsImCreating"); %>
    <% } %>
    
    <% if (((USER)ViewData["User"]).USER_TYPE > 3) { %>
        <h2>Poll Administrator Stuff</h2>
        <button>Create new Poll Creator</button>
        <button>View Reports</button>
      
    <% } %>

</asp:Content>
