<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<DBPOLLDemo.Models.userModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    sysAdmin
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Please select Poll Administrator to edit or delete, or use the button at the bottom to create a new one</h2>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Please select a Poll Administrator to manage</legend>
        <table>
            <tr>
                <th nowrap="nowrap">
                    Actions
                </th>
                <th nowrap="nowrap">
                    UserID
                </th>
                <th nowrap="nowrap">
                    UserName
                </th>
                <th nowrap="nowrap">
                    Name
                </th>
            </tr>
            <% foreach (var item in Model)
               { %>
            <tr>
                <td nowrap="nowrap">
                    <%= Html.ActionLink("Delete", "Delete", new { UserID = item.UserID })%>
                    |
                    <%= Html.ActionLink("Edit", "Edit", new { UserID = item.UserID })%>
                </td>
                <td nowrap="nowrap">
                    <%= Html.Encode(item.UserID)%>
                </td>
                <td nowrap="nowrap">
                    <%= Html.Encode(item.username)%>
                </td>
                <td nowrap="nowrap">
                    <%= Html.Encode(item.Name)%>
                </td>
            </tr>
            <% } %>
        </table>
        <p>
            <%= Html.ActionLink("Create new Poll Administrator", "Create")%>
        </p>
    </fieldset>
    <% } %>
</asp:Content>
