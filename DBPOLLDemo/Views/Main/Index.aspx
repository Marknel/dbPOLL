<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLL.Models.pollModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Poll Index</h2>

    <table>
        <tr>
            <th>Actions</th>
             <th>Poll Name</th>
             <th>Creation Date</th>
        </tr>

    <% foreach (var item in Model) { 
           
           %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Delete", "Delete", new {pollid=item.pollID}) %> |
                <%= Html.ActionLink("Edit", "Edit", new {name=item.Name, id = item.pollID, longitude = item.longitude, latitude = item.latitude, createdby = item.createdby, createdat = item.createdAt, expiresat = item.expiresat,  modifiedat = item.modifiedat}) %> |
                
                <%= Html.ActionLink(" View Questions", "Details", new {id=item.pollID, name=item.Name})%>
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

