<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.ANSWER_HISTORY>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th></th>
            <th>
                ANSWERH_ID
            </th>
            <th>
                ANSWER
            </th>
            <th>
                CORRECT
            </th>
            <th>
                WEIGHT
            </th>
            <th>
                NUM
            </th>
            <th>
                CREATED_AT
            </th>
            <th>
                MODIFIED_AT
            </th>
            <th>
                ANSWER_ID
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id=item.ANSWERH_ID }) %> |
                <%: Html.ActionLink("Details", "Details", new { id=item.ANSWERH_ID })%> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.ANSWERH_ID })%>
            </td>
            <td>
                <%: item.ANSWERH_ID %>
            </td>
            <td>
                <%: item.ANSWER %>
            </td>
            <td>
                <%: item.CORRECT %>
            </td>
            <td>
                <%: item.WEIGHT %>
            </td>
            <td>
                <%: item.NUM %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.CREATED_AT) %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.MODIFIED_AT) %>
            </td>
            <td>
                <%: item.ANSWER_ID %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

