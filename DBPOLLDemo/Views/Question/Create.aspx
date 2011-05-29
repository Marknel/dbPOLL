<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLContext.QUESTION>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Type of Question to Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Type of Question to Create for <%=ViewData["name"]%></h2>

     <p>
    <%= Html.ActionLink("Short Answer", "CreateShortAnswer", new { pollid = ViewData["id"] })%> <br />
    <%= Html.ActionLink("Multiple Choice", "CreateMultipleChoice", new { pollid = ViewData["id"] })%>
    </p>
    
    <div>
        <%=Html.ActionLink("Back to Question List", "Index", new { id = ViewData["id"] }) %>
    </div>

</asp:Content>

