<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="DBPOLLDemo.Models" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    DBPOLL Home Page
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%= Html.Encode(ViewData["Message"])%></h2>
    <h3>
        Account</h3>
    <% if ((int)Session["user_type"] > 1)
       { %>
    <%= Html.ActionLink("Edit My Details", "Edit", "User")%>
    <br />
    <br />
    <% } %>
    <%= Html.ActionLink("Change Password", "ChangePassword", "User")%>
    <br />
    <br />
    <%= Html.ActionLink("My Messages", "Index", "Message")%>
    <br />
    <br />
    <hr />
    <br />
    <%= Html.ActionLink("Answer Poll", "../Session/ViewAvailableSession", new { userid = (int)Session["uid"] })%>
    <% if ((int)Session["user_type"] > 1)
       { %>
    <br />
    <br />
    <%= Html.ActionLink("Create New User", "RegisterUser", "User")%>
    <br />
    <br />
    <%= Html.ActionLink("Reports", "Index", "Report")%>
    <br />
    <br />
    <%= Html.ActionLink("Test Recievers", "../Poll/TestDevices")%>
    <br />
    <br />
    <%= Html.ActionLink("Run Devices", "../Poll/RunDevices")%>
    <br />
    <br />
    <hr />
    <h3>
        Administer Poll</h3>
    <%= Html.ActionLink("View Polls and Questions", "viewPolls", "Poll")%>
    <br />
    <br />
    <%= Html.ActionLink("Edit Create and Delete Polls", "Index", "Poll")%>
    <br />
    <br />
    <% if ((int)Session["user_type"] > 2)
       { %>
<%--    <%= Html.ActionLink("Create Questions", "Create", "Question")%>--%>
    <br />
    <br />
    <% } %>
    <% if ((int)Session["user_type"] > 3)
       { %>
    <%= Html.ActionLink("Define new Poll", "Create", "Poll")%>
    <br />
    <br />
    <% } %>
    <hr />
    <br />
    <% } %>
    <br />
    <br />
    
</asp:Content>
