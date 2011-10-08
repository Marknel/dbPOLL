<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Models.USER>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create New Poll Administrator
</asp:Content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">
    <h2>
        Create New Poll Administrator
        <%=ViewData["error1"]%></h2>
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Details</legend>
        <p>
            <label for="Name">
                Name:</label>
            <%= Html.TextBox("name")%>
             <p style="color: Red;">
            <%=ViewData["nameError"]%></p>
        </p>
        <p>
            <label for="email">
                Email Address:</label>
            <%= Html.TextBox("email")%>
             <p style="color: Red;">
            <%=ViewData["emailError"]%></p>
        </p>
        <p>
            <label for="expiry">
                Number of months for account to be valid (if left blank will be 12 months):</label>
            <%= Html.TextBox("expiry")%>
             <p style="color: Red;">
            <%=ViewData["expiryError"]%></p>
        </p>
 <p style="color: Red;">
            <%=ViewData["created"]%></p>
        <p style="color: Red;">
            <%=ViewData["mastererror"]%></p>
        <input type="submit" value="Create User" />
    </fieldset>
    <% } %>
    <p>
        <%= Html.ActionLink("Back to Poll Administrator List", "Index")%>
    </p>
</asp:content>