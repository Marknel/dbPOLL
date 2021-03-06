﻿<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.questionModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SessionParticipation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Session Participation</h2>
    <p> Tables below shows a record of attendance and level of participation during the available sessions</p>
    <p class="style1"> Note: sessions with zero participant will not be shown </p>

     <table>
        <tr>
            <th nowrap="nowrap">
                Poll Name
            </th>
            <th nowrap="nowrap">
                Session Name
            </th>
            <th nowrap="nowrap">
                Total Participant 
            </th>
            <th nowrap="nowrap">
                Participant id
            </th>
            
            
        </tr>
   <% Session["export"] = ""; %>
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
           
            <% Session["export"] += item.pollname; %>
            <% Session["export"] += item.sessionname; %>
            <% Session["export"] += item.totalparticipants.ToString(); %>
            <% Session["export"] += item.participants.ToString(); %>

            
        </tr>
    <% } %>

    </table>
    <%= Html.ActionLink("Generate report in excel", "StatisticalReportExport")%>
    <%--<asp:Button
        ID="clickme" runat="server" name="clickme" Text="Generate report in excel" />--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: x-small;
        }
    </style>
</asp:Content>
