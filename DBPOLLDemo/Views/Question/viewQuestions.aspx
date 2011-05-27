<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.questionModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	questionDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Questions for Poll: <%= Html.Encode(ViewData["name"]) %></h2>
    <h2>View Questions for Poll: <%= Html.Encode(ViewData["name"]) %> By Date (mm/dd/yyyy 00:00:00)</h2>
        <% using (Html.BeginForm("Edit","Edit", FormMethod.Post)) {%>
        <fieldset>
            <legend>Search</legend>
                <table>
                    <tr>
                         <th>Start Date and Time</th>
                         <th>End Date and Time</th>
                         <th></th>
                    </tr>
                    <tr>
                <td><%= Html.TextBox("date1")%><br /> <%= ViewData["date1"]%></td>
                 <td><%= Html.TextBox("date2")%><br /> <%= ViewData["date2"]%></td>
                 <td><input type="submit" value="Search" /></td>
                </tr>
                </table>
            </fieldset>
        
        
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
                <%= Html.Encode(item.QuestionType) %>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(String.Format("{0:g}", item.QuestionCreated)) %>
            </td>
            
            
        </tr>
    
    <% } %>

    </table>
<% } %>
   <p>
        <%= Html.ActionLink("Back to Menu", "../Home/Home") %>
    </p>
    

</asp:Content>

