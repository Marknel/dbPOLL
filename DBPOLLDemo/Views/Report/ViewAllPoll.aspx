<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.pollModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ViewAllPoll
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm()){%>
         <fieldset>
            <legend>Select Type:</legend>
            <p>
                <label for="graphType">Graph Type:</label>
                <select id="graphType" name="graphType">
                <option value="Bar">Bar</option>
                <option value="Column">Column</option>
                </select>
            </p>
            <p class="style1"> Note: To generate report with a different chart, please select 
                one above. </p>
            <p class="style1"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; A default graph will be shown if no value is chosen </p>
            <input type="submit" value="Save graph type" />

        </fieldset>
    <%} %>

    <% String pnamecheck = ""; %>
    <table>
        <tr>
            <th>Actions</th>
             <th>Poll Name</th>
             <th>Date Poll Created</th>
        </tr>
        <tr>
            <td><%= Html.ActionLink("View Report for all poll", "../Report/StatisticalReport")%></td>
            <td></td>
            <td></td>
        </tr>

    <%--Check this! Poll Admin could see ALL poll but Poll Master could only see those he created--%>
    <%
        foreach (var item in Model) {
           %>
    
        <% if (!pnamecheck.Contains(item.pollname))
           { %>
            <tr>
                <td>
                    <%= Html.ActionLink("View Report", "../Report/OneStatisticalReport", new { pollid = item.pollid })%>
                </td>
                <td>
                    <%= Html.Encode(item.pollname)%>
                    <% pnamecheck += item.pollname;%>
                </td>
                  <td>
                    <%= Html.Encode(String.Format("{0:g}", item.createdAt))%>
                </td>
            </tr>
        <%} %>
    
    <% } %>

    </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: x-small;
            width : 470px;
        }
    </style>
</asp:Content>
