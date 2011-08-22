<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.questionModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SessionParticipation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Session Participation</h2>
    <p> Tables below shows a record of attendance and level of participation during the available session in all polls</p>

     <table>
        <tr>
            <th nowrap="nowrap">
                Poll Name
            </th>
            <th nowrap="nowrap">
                Session Name
            </th>
            <th nowrap="nowrap">
                Total Participants 
            </th>
            <th nowrap="nowrap">
                Participant 
            </th>
            
            
        </tr>

   <% String pnamecheck = "" ;%>
   <% String scheck = "" ;%>
   <% foreach (var item in Model) { %>
     <tr>
            <td nowrap="nowrap">
                    <% if (pnamecheck != item.pollname){ %>
                        <%= Html.Encode(item.pollname)%>
                        <% pnamecheck = item.pollname; %>
                    <%} %>
            </td>
            <td nowrap="nowrap">
                <% if (scheck != item.sessionname){ %>
                    <%= Html.Encode(item.sessionname)%>
                <%} %>
            </td>
            <td nowrap="nowrap">
                <% if (scheck != item.sessionname){ %>
                    <%= Html.Encode(item.totalparticipants)%>
                    <% scheck = item.sessionname; %>
                <%} %>
            </td>
            <td nowrap="nowrap">
                 
                <%= Html.Encode(item.participants)%>
            </td>
           
  
            
        </tr>
    <% } %>

    </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
