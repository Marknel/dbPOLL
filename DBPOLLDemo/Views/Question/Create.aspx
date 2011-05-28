<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLContext.QUESTION>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Type of Question to Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Type of Question to Create <%=ViewData["error1"]%></h2>

     <p>
    <%= Html.ActionLink("Short Answer", "CreateShortAnswer", new { pollid = ViewData["id"] })%> <br />
    <%= Html.ActionLink("Multiple Choice", "CreateMultipleChoice")%>
    </p>
    
    <div>
        <%=Html.ActionLink("Back to Question List", "Index") %>
    </div>

</asp:Content>

