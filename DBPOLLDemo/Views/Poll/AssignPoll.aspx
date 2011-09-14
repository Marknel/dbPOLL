<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<DBPOLLDemo.Models.userModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AssignPoll
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Assign Poll Masters To: <%=ViewData["pollname"] %></h2>

    <table>
    <tr>
             <th class="style2">Select Poll Master</th>
             <th class="style2">User ID</th>
             <th class="style2">Username</th>
             <th class="style2">First Name</th>
        </tr>
    <% using (Html.BeginForm("AssignPoll", "Poll")) {  %>
        <% foreach (var o in Model) { %>
        <tr>
            <td>
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
        <input type="submit" value="Assign Poll Masters" />
        
    <%}%>

    <p>
        <%= Html.ActionLink("Back to Polls", "Index")%>
    </p>
</asp:Content>

