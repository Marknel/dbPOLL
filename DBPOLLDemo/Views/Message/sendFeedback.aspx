<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Models.messageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Send Feedback
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        Send Feedback:</h2>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Fields</legend>
        <p>
            <label for="message">
                Message:</label>
            <%= Html.TextBox("message", Model.Message)%>
            <p style="color: Red;">
                <%=ViewData["messageError"]%></p>
        </p>
       
        <p style="color: Red;">
                <%=ViewData["edited"]%></p>
        <p>
            <input type="submit" value="Save Changes" />
        </p>
    </fieldset>
    <%
        }%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
