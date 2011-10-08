<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.pollModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ViewAvailableSession
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Sessions Available</h2>


    <% 
        if (Model.Count() != 0)
       { %>
            <table>
                <tr>
                    <th> Actions </th>
                    <th> Session Name </th>
                    <th> Session Expires at </th>
                    <th> Status </th>
                </tr>

                <% foreach (var item in Model)
                   { %>
                    <tr>
                        <%-- link is still broken, no StartSession page just yet--%>
                        <td><%= Html.ActionLink("Open Session", "../Session/StartSession", new { sessionid = item.sessionid, pollid = item.pollid })%></td>
                        <td> <%=Html.Encode(item.sessionname)%></td>
                        <td> <%=Html.Encode(item.expiresat)%></td>

                        <% 
        if (item.expiresat == null)
        { %>
                            <td> <%=Html.Encode("Open, undefined")%></td>
                
                        <%}
        else if (item.expiresat < DateTime.Now)
        {%>
                            <td> <%=Html.Encode("Expired, closed")%></td>
                        <%}

        else if (item.expiresat >= DateTime.Now)
        {%>
                            <td> <%=Html.Encode("Open")%></td>
                        <%}

        else
        {%>
                            <td> <%=Html.Encode("Unknown")%></td>
                        <%} %>
                    </tr>
                    
                <%} %>
            </table>

     <%} %>

     <% else
        { %>

        <p> You have not been invited to any of the session</p>
     <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
