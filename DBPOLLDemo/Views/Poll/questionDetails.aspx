<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.questionModel>>" %>

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
                <%= Html.ActionLink("Delete", "answerDetails", new { id = item.questionid})%> |
                <%= Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
                <%= Html.ActionLink("View Answers", "answerDetails", new { id = item.questionid, name = item.question })%> 
                
            </td>
            <td>
                <%= Html.Encode(item.questnum) %>
            </td>
            <td>
                <%= Html.Encode(item.question) %>
            </td>
            <td>
                <%= Html.Encode(item.questiontype) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.createdat)) %>
            </td>
            
            
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

