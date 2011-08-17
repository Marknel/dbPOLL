<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLL.Models.pollModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SessionHistoryReport</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <p>
    </p>
    <h2>Session History Report</h2>

    <table>
        <tr>
            <th nowrap="nowrap">
                Poll Name
            </th>
           
            <th nowrap="nowrap">
                Date Poll Created
            </th>
            <th nowrap="nowrap">
                Poll Master Assigned
            </th>
            <th nowrap="nowrap">
                Poll Creator
            </th>
            <th nowrap="nowrap">
                Total Number of Participants
            </th>
            
            
        </tr>

   <% foreach (var item in Model) { %>
    
        <tr>
            
            <td nowrap="nowrap">
                <%= Html.Encode(item.pollname)%>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(String.Format("{0:g}", item.createdAt))%>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.createdmaster) %>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.createdcreator1) %>
            </td>
            
            <td nowrap="nowrap">
                <%= Html.Encode(item.total) %>
            </td>
            
            
        </tr>
    
    <% } %>

    </table>
    </asp:Content>
