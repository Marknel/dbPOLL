<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.questionModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	questionDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Questions for Poll: <%= Html.Encode(ViewData["name"]) %></h2>

    <table>
        <tr>
            <th nowrap="nowrap">Actions</th>
            <th nowrap="nowrap">
                Question Number
            </th>
            <th nowrap="nowrap">
                Question
            </th>
            <th nowrap="nowrap">
                Question Type
            </th>
            <th nowrap="nowrap">
                Date Question Created
            </th>
            
            
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td nowrap="nowrap">
                <%= Html.ActionLink("Delete", "Delete", new { questionid = item.QuestionID, id = ViewData["id"], name = ViewData["name"] })%> |
                <%= Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
                <%= Html.ActionLink("View Answers", "Details", new { id = item.QuestionID, name = item.Question })%> 
                
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.QuestionNumber) %>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.Question) %>
            </td>
            <td nowrap="nowrap">
                <%  string test;
                if(item.QuestionType == 1){test = "Multiple Choice";}else{test = "Short Answer";}%>
                 <%=Html.Encode(test) %>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(String.Format("{0:g}", item.QuestionCreated)) %>
            </td>
            
            
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create", new { pollid = ViewData["id"] })%>
    </p>

</asp:Content>

