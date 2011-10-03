<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Reset Password
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Reset Password</h2>
        <% using (Html.BeginForm())
       { %>
    <div>
        <fieldset>
            <legend>Please enter the email address for which the account is registered</legend>
            <p>
                <label for="Email">
                    Email Address:</label>
                <%= Html.TextBox("Email")%>
                <%= Html.ValidationMessage("Email")%>
            </p>
            <p>
                <input type="submit" value="Reset Password" />
            </p>
        </fieldset>
    </div>
    <% } %>
    <p style="color: Red;">
        <%=ViewData["emailError"] %>
    </p>
    <p style="color: Red;">
        <%=ViewData["outcome"]%>
    </p>
</asp:Content>
