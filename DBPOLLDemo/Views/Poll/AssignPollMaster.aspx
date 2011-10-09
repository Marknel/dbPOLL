<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Controllers.Assign_PollMasters>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AssignPoll
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Assign Poll Masters To: <%=ViewData["pollname"] %></h2>

    <h3>Poll Masters Assigned to: <%=ViewData["pollname"] %></h3>
    <table>
    <tr>
             <th class="style2">Remove Poll Master</th>
             <th class="style2">User ID</th>
             <th class="style2">Username</th>
             <th class="style2">First Name</th>
        </tr>
        <% foreach (var o in Model.assigned) { %>
        <tr>
         <td class="style2">
           <%= Html.ActionLink("Delete", "UnassignPollUser", new {pollname = ViewData["pollname"], userid = o.UserID, pollid = ViewData["pollid"], master = true})%>
            </td>
            <td>
            <%= o.UserID %>
            </td>
            <td>
            <%= o.username %>
            </td>
            <td>
            <%= o.Name %>
            </td>
            </tr>
        <%}%>
        </table>


<h3><%=ViewData["emailError"] %></h3>
    <h3>Poll Masters Unassigned to: <%=ViewData["pollname"] %></h3>
    <table>
    <tr>
             <th class="style2">Select Poll Master</th>
             <th class="style2">User ID</th>
             <th class="style2">Username</th>
             <th class="style2">First Name</th>
        </tr>
    <% using (Html.BeginForm("AssignPoll", "Poll")) {  %>
        <% foreach (var o in Model.unassigned) { %>
        <tr>
            <td class="style2">
            <input type="checkbox" name="selectedObjects" value="<%= o.UserID%> "/>
            </td>
            <td>
            <%= o.UserID %>
            </td>
            <td>
            <%= o.username %>
            </td>
            <td>
            <%= o.Name %>
            </td>
            </tr>
        <%}%>
        </table>
        <input type="hidden" id="pollid" name="pollid" value="<%=ViewData["pollid"] %>" />
        <input type="hidden" id="pollname" name="pollname" value="<%=ViewData["pollname"] %>" />
        <input type="submit" value="Assign Poll Masters" />
        
    <%}%>

    <p>
        <%= Html.ActionLink("Back to Polls", "Index")%>
    </p>
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .style2
        {
            width: 141px;
        }
    </style>
</asp:Content>


