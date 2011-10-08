<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CreateMethod
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Type of Question to Create for <%=ViewData["name"]%></h2>

     <p>
    <%= Html.ActionLink("Custom Answer", "Create", new { questionid = ViewData["questionid"] })%> <br />
    <%= Html.ActionLink("True/False", "CreateTrueFalse", new { questionid = ViewData["questionid"] })%> <br />
    <%= Html.ActionLink("Yes/No/Abstain", "CreateYesNoAbstain", new { questionid = ViewData["questionid"] })%> <br />
    <%= Html.ActionLink("5 Opinion Scale", "OpinionScale", new { questionid = ViewData["questionid"] })%> <br />
    <%= Html.ActionLink("7 Opinion Scale", "SevenOpinionScale", new { questionid = ViewData["questionid"] })%> <br />
    </p>

    <div>
        <%=Html.ActionLink("Back to Answer List", "Index", new { id = ViewData["questionid"]})%>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
