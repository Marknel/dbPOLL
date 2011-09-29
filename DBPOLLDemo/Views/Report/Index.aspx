<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Reports</h2>
    <p>Please choose a type of report you would like to generate:</p>
    <p> 
    <%-- <%= Html.ActionLink("Session History Report", "../Report/SessionHistoryReport") %> <br />--%>
    <%= Html.ActionLink("Session History Report", "../Report/SessionHistoryReport") %> <br />
    <%= Html.ActionLink("Statistical Report", "../Report/StatisticalReport")%> <br />
    
    <%= Html.ActionLink("Session Participation Report", "../Report/SessionParticipation")%> <br />
    
    
    </p>

</asp:Content>
