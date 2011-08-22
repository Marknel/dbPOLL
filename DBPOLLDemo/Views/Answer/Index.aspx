<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.answerModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Answers for : <%= Html.Encode(ViewData["name"]) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Answers for : <%= Html.Encode(ViewData["name"]) %></h2>

    <table>
        <tr>
            <th>Actions</th>
            <th>Number</th>
            <th>Answer</th>
            <th>Weight</th>
            <th>Correct</th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td><%= Html.ActionLink("Delete", "Delete", new { answerid = item.answerid, questionid = ViewData["questionid"], name = ViewData["name"] })%> |
                <%= Html.ActionLink("Edit", "Edit", new { answerid = item.answerid, questionid = ViewData["questionid"] })%>
            </td>
            
            <td>
                <%= Html.Encode(item.ansnum) %>
            </td>

            <td>
                <%= Html.Encode(item.Answer) %>
            </td>
            
            <td>
                <%= Html.Encode(item.weight) %>
            </td>
            
            <td><%String correct;
                  if (item.correct == 0) { correct = ""; }
                  else { correct = "Yes"; }
            %>
                <%= Html.Encode(correct)%>
            </td>
            
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New Answer", "CreateMethod", new { questionid = ViewData["questionid"], name = ViewData["name"] })%>
    </p>

</asp:Content>

