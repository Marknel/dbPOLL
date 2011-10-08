<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Models.userModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit User:</h2>
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
            <p style="color: Red;">
                <%=ViewData["nameError"]%></p>
        </p>
        <p>
            <label for="expiry">
                Number of months for account to be valid (if left blank will be 12 months):</label>
            <%= Html.TextBox("expiry", Model.monthsLeft)%>
            <p style="color: Red;">
                <%=ViewData["expiryError"]%></p>
        </p>
        <p>
            <label for="email">
                Email:</label>
            <%= Html.TextBox("email", Model.username)%>
            <p style="color: Red;">
                <%=ViewData["emailError"]%></p>
        </p>
        <p style="color: Red;">
                <%=ViewData["edited"]%></p>
        <p>
            <input type="submit" value="Save Changes" />
        </p>
    </fieldset>
    <%
        }%>
    <div>
        <%=Html.ActionLink("Back to User List", "Index")%>
    </div>
</asp:Content>
