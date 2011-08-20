<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Models.userModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit User:
        <%="i need to be fixed!" /*Model.question*/%></h2>
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Fields</legend>
        <p>
            <%= Html.Hidden("userid", Model.UserID)%>
            <%= Html.Hidden("createdat", Model.Created_At)%>
        </p>
        <p>
            <label for="Name">
                Name:</label>
            <%= Html.TextBox("Name", Model.Name)%>
            <%= Html.ValidationMessage("Name", "*")%>
        </p>
        <p>
            <label for="Expires_At">
                Expiry date:</label>
            <%= Html.TextBox("Expires_At", Model.Expires_At)%>
            <%= Html.ValidationMessage("Expiry date", "*")%>
        </p>
        <p>
            <label for="username">
                Email:</label>
            <%= Html.TextBox("username", Model.username)%>
            <%= Html.ValidationMessage("Username", "*")%>
        </p>
        <p>
            <input type="submit" value="Save Changes" />
        </p>
    </fieldset>
    <%--</form>--%>
    <% } %>
    <div>
        <%=Html.ActionLink("Back to User List", "Index")%>
    </div>
</asp:Content>
