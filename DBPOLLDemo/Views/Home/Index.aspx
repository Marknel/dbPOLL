<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLContext.USER>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Login</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <p><%= Html.Encode(ViewData["Message"]) %></p>

    <% using (Html.BeginForm("Login","Index", FormMethod.Post)) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="USERNAME">USERNAME:</label>
                <%= Html.TextBox("USERNAME") %>
                <%= Html.ValidationMessage("USERNAME", "*") %>
            </p>
            <p>
                <label for="PASSWORD">PASSWORD:</label>
                <%= Html.TextBox("PASSWORD") %>
                <%= Html.ValidationMessage("PASSWORD", "*") %>
            </p>
            <p>
                <input type="submit" value="Login" />
            </p>
        </fieldset>

    <% } %>
</asp:Content>

