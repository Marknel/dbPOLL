<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.questionModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SessionParticipation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Session Participation</h2>

     <table>
        <tr>
            <th nowrap="nowrap">
                Poll Name
            </th>
            <th nowrap="nowrap">
                Participant 
            </th>
            <th nowrap="nowrap">
                Total Participants 
            </th>
          
           <%-- <th nowrap="nowrap">
                Number of Poll Attendances
            </th>--%>
            
        </tr>

   <% foreach (var item in Model) { %>
     <tr>
            <td nowrap="nowrap">
                <%= Html.Encode(item.pollname)%>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.participants)%>
            </td>
           <td nowrap="nowrap">
                <%= Html.Encode(item.totalparticipants)%>
            </td>
  
            
        </tr>
    <% } %>

    </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
