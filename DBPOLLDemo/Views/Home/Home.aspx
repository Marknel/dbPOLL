<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    DBPOLL Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <p>
    <%= Html.ActionLink("Edit Create and Delete Polls", "../Poll/Index") %> <br />
    <%= Html.ActionLink("View Polls and Questions", "../Poll/viewPolls")%> <br />
    <%= Html.ActionLink("Report", "../Report/Index")%> <br />
    <%= Html.ActionLink("Test Recievers", "../Poll/TestDevices")%> <br />
    </p>

    <% Html.RenderPartial("PollsToDo"); %>
</asp:Content>
