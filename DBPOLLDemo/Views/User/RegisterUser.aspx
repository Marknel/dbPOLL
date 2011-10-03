<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="DBPOLLDemo.Models" %>
<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Register
</asp:Content>
<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create a New Account</h2>
    <p>
        Use the form below to create a new account.
    </p>
    <%= Html.ValidationSummary("Account creation was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       { %>
    <div>
        <fieldset>
            <legend>Create New User</legend>
            <p>
                <label for="email">
                    Email:</label>
                <%= Html.TextBox("email") %>
                <%= Html.ValidationMessage("email", "*") %>
                <p style="color: Red;">
                <%= Html.Encode(ViewData["emailError"])%></p>
            </p>
            <p>
                <label for="NAME">
                    Name:</label>
                <%= Html.TextBox("NAME") %>
                <%= Html.ValidationMessage("NAME", "*") %>
                 <p style="color: Red;">
                <%= Html.Encode(ViewData["nameError"])%></p>
            </p>
            <p>
                <%= Html.DropDownList("USER_TYPE")%>
            </p>
            <p>
                <%--<label for="USER_TYPE">
                    User Type:</label>
                <select id="USER_TYPE">
                    <% if (((USER)ViewData["User"]).USER_TYPE > 1)
                       { %>
                    <option value="Poll User">Poll User</option>
                    <% } %>
                    <% if (((USER)ViewData["User"]).USER_TYPE > 2)
                       { %>
                    <option value="Poll Master">Poll Master</option>
                    <% } %>
                    <% if (((USER)ViewData["User"]).USER_TYPE > 3)
                       { %>
                    <option value="Poll Creator">Poll Creator</option>
                    <% } %>
                </select>--%>
            </p>
            <p style="color: Red;">
                <%= Html.Encode(ViewData["Message"]) %></p>
            <p>
                <input type="submit" value="Create User" />
            </p>
        </fieldset>
    </div>
    <% } %>
</asp:Content>
