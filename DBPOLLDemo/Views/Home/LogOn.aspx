<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Log On
</asp:Content>
<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Log On
    </h2>
    <%= Html.ValidationSummary("Login was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       { %>
    <%--<% using (Html.BeginForm("Login","Account", FormMethod.Post)) {%>--%>
    <div>
        <fieldset>
            <legend>Account Information</legend>
            <p>
                <label for="username">
                    Username:</label>
                <%= Html.TextBox("username") %>
                <%= Html.ValidationMessage("username") %>
            </p>
            <p>
                <label for="password">
                    Password:</label>
                <%= Html.Password("password") %>
                <%= Html.ValidationMessage("password") %>
            </p>
            <%=Html.ActionLink("Forgot your password?", "ResetPassword")%>
            <%--<p>
                    <%= Html.CheckBox("rememberMe") %> <label class="inline" for="rememberMe">Remember me?</label>
                </p>--%>
            <br /><br />
            <p style="color: Red;">
                <%= ViewData["Message"]%></p>
                <br /><br />
            <p>
                <input type="submit" value="Log On" />
            </p>
        </fieldset>
    </div>
    <% } %>
</asp:Content>
