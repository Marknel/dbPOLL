<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLContext.POLL>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index2
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index2</h2>

    <table>
        <tr>
            <th></th>
            <th>
                POLLID
            </th>
            <th>
                POLLNAME
            </th>
            <th>
                LONGITUDE
            </th>
            <th>
                LATITUDE
            </th>
            <th>
                CREATEDBY
            </th>
            <th>
                EXPIRESAT
            </th>
            <th>
                CREATEDAT
            </th>
            <th>
                MODIFIEDAT
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.POLLID }) %> |
                <%= Html.ActionLink("Details", "Details", new { id=item.POLLID })%>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.POLLID)) %>
            </td>
            <td>
                <%= Html.Encode(item.POLLNAME) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.LONGITUDE)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.LATITUDE)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.CREATEDBY)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.EXPIRESAT)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.CREATEDAT)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.MODIFIEDAT)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

