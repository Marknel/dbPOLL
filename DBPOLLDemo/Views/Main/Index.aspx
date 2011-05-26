<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLL.Models.pollModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Poll Index</h2>

    <table>
        <tr>
            <th>Possible Actions</th>
             <th>Poll Name</th>
             <th>Creation Date</th>
        </tr>

    <% foreach (var item in Model) { 
           
           %>
    
        <tr>
            <td>
                
                <%= Html.ActionLink("Edit Poll", "Edit", new {id=item.pollID}) %> |
                <%= Html.ActionLink(" View Questions", "questionDetails", new {id=item.pollID, name=item.Name})%>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
              <td>
                <%= Html.Encode(String.Format("{0:g}", item.CreateDate)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

