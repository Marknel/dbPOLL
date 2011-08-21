<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Controllers.PollAndSessionData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Poll Index </h2>

    <table>
        <tr>
            <th nowrap="nowrap">Actions</th>
             <th nowrap="nowrap">Poll Name</th>
             <th nowrap="nowrap">Creation Date</th>
        </tr>

    <% foreach (var item in Model.pollData) { 
           
           %>
    
        <tr>
            <td nowrap="nowrap">
                <%= Html.ActionLink("Delete", "Delete", new {pollid=item.pollID}) %> |
                <%= Html.ActionLink("Edit", "Edit", new {name=item.Name, id = item.pollID}) %> |
                <%= Html.ActionLink(" View Questions", "Details", new {id=item.pollID, name=item.Name})%> |
                <%= Html.ActionLink("Create New Session", "CreateSession", new {pollID = item.pollID, pollName = item.pollname})%>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.Name) %>
            </td>
              <td nowrap="nowrap">
                <%= Html.Encode(String.Format("{0:g}", item.CreateDate)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New Poll", "Create", new {createdby = (int)Session["uid"] })%>
    </p>

    <h2>Session Index </h2>

    <table>
        <tr>
            <th class="style2">Actions</th>
             <th class="style2">Session Name</th>
             <th class="style2">Poll Name</th>
        </tr>

    <% foreach (var item in Model.sessionData)
       { 
           
           %>
    
        <tr>
            <td nowrap="nowrap">
                <%= Html.ActionLink("Delete", "DeleteSession", new {sessionid=item.sessionid}) %> |
                <%= Html.ActionLink("Edit", "EditSession", new {sessionname=item.sessionName, pollid = item.pollID, sessionid = item.sessionid, longitude = item.longitude, latitude = item.latitude, time = item.time}) %>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.sessionName) %>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.Name) %>
            </td>
        </tr>
    
    <% } %>

    </table>
</asp:Content>


