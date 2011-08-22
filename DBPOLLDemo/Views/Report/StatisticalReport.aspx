<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.questionModel>>" %>

<script runat="server">
 
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    StatisticalReport
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="style1">
        Statistical Report</h2>
    <% String pollnamecheck = "";
       String qcheck = "";
       String scheck = "";
       String acheck = "";
    %>
    <% 
        List<int> list = new List<int>();
        Session["test"] = "";
     %>        
    <% foreach (var item in Model)
       { %>
            
        <%if (pollnamecheck != item.pollname ){ %>  
           <% acheck = ""; %>  
            <table>
                    <tr>
                        <th nowrap="nowrap" class="style5">
                            Answer Choices
                        </th>   
                        <th nowrap="nowrap" class="style100">
                            <%= Html.Encode("Session " + item.sessionid)%>
                        </th>
                    </tr> 
           <p style="font-weight: bold;"><%= Html.Encode("Result for: " + item.pollname)%></p>
           <br />
            <%= Html.Encode("Question: " + item.question)%> 
            <br />
            <img src="<%= Url.Action("Chart") %>" alt="image" />  </br></br>
           
            
         
         <%}else if (pollnamecheck == item.pollname && !qcheck.Contains(item.question))
          {%>       
                     <% acheck = ""; %>
                     
                   <table>
                    <tr>
                        <th nowrap="nowrap" class="style5">
                            Answer Choices
                        </th>   
                        <th nowrap="nowrap" class="style100">
                            <%= Html.Encode(item.sessionname)%>
                            <% scheck += item.sessionname; %>
                        </th>
                        
                    </tr> 
                  <% pollnamecheck = item.pollname;
                     qcheck += (item.question);
                  %>    

                <br />
                <br />
               <%= Html.Encode("Question: " + item.question)%><br />
               <img src="<%= Url.Action("Chart") %>" alt="image" />  </br></br>
               
        <%} %>
                     <% if (!acheck.Contains(item.answer)) {%>
                    <tr> 
                        <td nowrap="nowrap" class="style5">
                            <%= Html.Encode(item.answer)%>
                             <%acheck += item.answer; %>
                        </td>
                        <td nowrap="nowrap" class="style5">
                            <%= Html.Encode(item.totalparticipants)%>
                            <% list.Add(item.totalparticipants); %>
                        </td>
                        
                    </tr>
                    <%} %>  
                  <% pollnamecheck = item.pollname; 
                     qcheck += (item.question);
                     scheck += (item.sessionid.ToString());
                     list.Add(item.totalparticipants);
                  %>   
                   
    <%}%>
    </table>
</asp:Content>
<%--<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
    </style>
</asp:Content>--%>

