<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.answerHistoryModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Answer History for : <%= Html.Encode(ViewData["name"]) %></h2>

    <table>
        <tr>
            <th>Actions</th>
            <th>Number</th>
            <th>Answer</th>
            <th>Weight</th>
            <th>Correct</th>
            <th>
                Created Date
            </th>
            <th>
                Modified Date
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("Revert", "Revert", new { answerid = item.aid, answer = item.answer, correct = item.correct, weight = item.weight, ansnum = item.ansnum }) %> | 
                <%: Html.ActionLink("Delete", "Delete", new { aid = item.aid, ahid = item.answerhid })%>
            </td>
            <td>
                <%: item.ansnum %>
            </td>
            <td>
                <%: item.answer %>
            </td>

            <td>
                <%: item.weight %>
            </td>

            <td>
                <%String correct;
                  if (item.correct == 0) { correct = ""; }
                  else { correct = "Yes"; }
            %>
                <%= Html.Encode(correct)%>
            </td>


            <td>
                <%= Html.Encode(String.Format("{0:g}", item.createdat)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.modifiedat)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


