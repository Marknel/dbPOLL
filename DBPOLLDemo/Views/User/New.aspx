<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="DBPOLLDemo.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	New
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm("Create","Create", FormMethod.Post)) {%>

        <fieldset>
            <legend>Create New User</legend>
            <p>
                <label for="email">Email:</label>
                <%= Html.TextBox("email") %>
                <%= Html.ValidationMessage("email", "*") %>
            </p>
            <p>
                <label for="NAME">Name:</label>
                <%= Html.TextBox("NAME") %>
                <%= Html.ValidationMessage("NAME", "*") %>
            </p>
            <p>
            
                <label for="USER_TYPE">User Type:</label>
                <select>
                    <option value="Poll User">Poll User</option>
                    <% if (((USER)ViewData["User"]).USER_TYPE > 1) { %>
                        <option value="Poll Master">Poll Master</option>
                    <% } %>
                    <% if (((USER)ViewData["User"]).USER_TYPE > 1) { %>
                        <option value="Poll Creator">Poll Creator</option>
                    <% } %>
                </select>
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
