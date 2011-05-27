<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLL.Models.pollModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
 
    <h2>View Polls By Date (mm/dd/yyyy 00:00:00)</h2>
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
            <th>Actions</th>
             <th>Poll Name</th>
             <th>Creation Date</th>
        </tr>
        <tr>
        <td><%= Html.ActionLink(" All Questions By Date", "Details", new { id = 0 })%></td>
        </tr>

    <%
        foreach (var item in Model) {
           %>
    
        <tr>
            <td>
                <%= Html.ActionLink(" Poll Questions By Date", "Details", new {id=item.pollID})%>
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
<% } %>
    <p>
        <%= Html.ActionLink("Back to Menu", "../Home/Home") %>
    </p>
    

</asp:Content>

