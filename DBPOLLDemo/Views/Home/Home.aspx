﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    DBPOLL Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <p>
    <%= Html.ActionLink("Edit Create and Delete Polls", "../Main/Index") %> <br />
    <%= Html.ActionLink("View Polls and Questions", "../Main/viewPolls")%>
    </p>

    <% Html.RenderPartial("PollsToDo"); %>
</asp:Content>
