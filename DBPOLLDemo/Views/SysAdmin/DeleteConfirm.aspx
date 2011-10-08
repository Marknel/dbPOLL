<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        Are you sure you want to delete this user?
        <br />
        <%= Html.ActionLink("Yes I want to delete the user", "DeleteSuccess", new { UserID = ViewData["delID"] })%>
        <br />
        <br />
        <%= Html.ActionLink("No I do not want to delete the user", "Index")%>
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
