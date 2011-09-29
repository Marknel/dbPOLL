<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	dbPOLL Login
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!-- 'Inherits="System.Web.Mvc.ViewPage<DBPOLLContext.USER>" -->
    <h2>Login</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm("Login","Index", FormMethod.Post)) {%>

        <fieldset>
            <legend>Please Login</legend>
            <p>
                <label for="USERNAME">Username:</label>
                <%= Html.TextBox("USERNAME") %>
                <%= Html.ValidationMessage("USERNAME", "*") %>
            </p>
            <p>
                <label for="PASSWORD">Password:</label>
                <%= Html.Password("PASSWORD") %>
                <%= Html.ValidationMessage("PASSWORD", "*") %>
            </p>
            <p style ="color: Red;"><%= Html.Encode(ViewData["Message"]) %></p>
            <p>
                <input type="submit" value="Login" />
            </p>
        </fieldset>

<% } %>
</asp:Content>

