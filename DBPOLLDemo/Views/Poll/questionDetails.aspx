<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.questionModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	questionDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Questions for Poll: <%= Html.Encode(ViewData["name"]) %></h2>

    <table>
        <tr>
            <th></th>
            <th>
                Question Number
            </th>
            <th>
                Question
            </th>
            <th>
                Question Type
            </th>
            <th>
                Date Question Created
            </th>
            
            
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Delete", "answerDetails", new { id = item.QuestionID})%> |
                <%= Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
                <%= Html.ActionLink("View Answers", "answerDetails", new { id = item.QuestionID, name = item.Question })%> 
                
            </td>
            <td>
                <%= Html.Encode(item.QuestionNumber) %>
            </td>
            <td>
                <%= Html.Encode(item.Question) %>
            </td>
            <td>
                <%= Html.Encode(item.QuestionType) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.QuestionCreated)) %>
            </td>
            
            
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

