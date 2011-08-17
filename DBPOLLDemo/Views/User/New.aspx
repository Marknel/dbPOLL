<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	New
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm("Create","Create", FormMethod.Post)) {%>

        <fieldset>
            <legend>Create New User</legend>
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
            <p>
                <label for="USER_TYPE">User Type:</label>
                <%= Html.TextBox("USER_TYPE") %>
            </p>
            <p style ="color: Red;"><%= Html.Encode(ViewData["Message"]) %></p>
            <p>
                <input type="submit" value="Create User" />
            </p>
        </fieldset>
<% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
