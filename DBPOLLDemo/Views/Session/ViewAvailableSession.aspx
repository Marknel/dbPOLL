<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.pollModel>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ViewAvailableSession
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Sessions you have been invited to</h2>


    <% 
        if (Model.Count() != 0)
       {%>
            <table>
                <tr>
                    <th> Session Name </th>
                    <th> Session Expires at </th>
                    <th> Type </th>
                    <th> Status </th>
                </tr>

                <% foreach (var item in Model)
                   { %>
                    <tr>
                        <td> <%=Html.Encode(item.sessionname)%></td>

                        <%   
                        if (item.expiresat != null)
                        {%> 

                            <td> <%=Html.Encode(item.expiresat)%></td>
                
                        <%}else{%>

                            <td> <%=Html.Encode("undefined")%></td>

                        <%} %>
                        
                        <td><%=Html.Encode(item.syncType + "hronous")%></td>

                        <% 
                        if (item.expiresat == null && item.currentquestion != 0)
                        { %>
                            
                            <% 
                            if (item.syncType == "async")
                               { %>
                                    <td> <%=Html.ActionLink("Open", "../Session/StartAsyncSession", new { sessionid = item.sessionid, pollid = item.pollid })%> </td>
                            <%} %>
                            <% else
                                { %>
                                    
                                    
                                    <td> <%=Html.ActionLink("Open", "../Session/StartSyncSession", new { sessionid = item.sessionid, pollid = item.pollid })%> </td>
                           
                            
                            <%} %>
                
                        <%}
                        else if (item.expiresat < DateTime.Now)
                        {%>
                            <td> <%=Html.Encode("Expired, closed")%></td>
                        <%}

                        else if (item.expiresat >= DateTime.Now && item.currentquestion != 0)
                        {%>

                        <% 
                            if (item.syncType == "async")
                               { %>
                                    <td> <%=Html.ActionLink("Open", "../Session/StartAsyncSession", new { sessionid = item.sessionid, pollid = item.pollid })%> </td>
                            <%} %>
                            <% else
                                { %>
                                    
                                    
                                    <td> <%=Html.ActionLink("Open", "../Session/StartSyncSession", new { sessionid = item.sessionid, pollid = item.pollid })%> </td>
                           
                            
                            <%} %>
                            <%--<td> <%=Html.ActionLink("Open", "../Session/StartAsyncSession", new { sessionid = item.sessionid, pollid = item.pollid })%> </td>--%>
                        <%}

                        else if (item.currentquestion == 0)
                        {%>
                            <td> <%=Html.Encode("Temporarily closed")%></td>
                        <%}


                        else { %>
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
