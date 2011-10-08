<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Change Password
</asp:Content>
<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Change Password</h2>
    <p>
        Use the form below to change your password.
    </p>
    <p>
        New passwords are required to be a minimum of
        <%=Html.Encode(ViewData["PasswordLength"])%>
        characters in length.
    </p>
    <% using (Html.BeginForm())
       { %>
    <div>
        <fieldset>
            <legend>Account Information</legend>
            <p>
                <label for="currentPassword">
                    Current password:</label>
                <%= Html.Password("currentPassword") %>
                <p style="color: Red;">
                    <%= ViewData["currentPassword"]%></p>
            </p>
            <p>
                <label for="newPassword">
                    New password:</label>
                <%= Html.Password("newPassword") %>
                <%= Html.ValidationMessage("newPassword") %>
            </p>
            <p>
                <label for="confirmPassword">
                    Confirm new password:</label>
                <%= Html.Password("confirmPassword") %>
                <p style="color: Red;">
                    <%= ViewData["confirmPassword"]%></p>
            </p>
            <p>
                <input type="submit" value="Change Password" />
            </p>
            <br />
              <p style="color: Red;">
                <%= ViewData["confirmation"]%></p>
        </fieldset>
    </div>
    <% } %>
</asp:Content>
