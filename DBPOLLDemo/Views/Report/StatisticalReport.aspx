<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.questionModel>>" %>

<script runat="server">



    
    
    


  
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	StatisticalReport
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Statistical Report</h2>
    <p class = "p">    <%= Html.ActionLink("Go to Session Participation Report", "../Report/SessionParticipation")%> 
        <br />
        Report for multiple choice question:<br />
    </p>
    <table class = "style7">
        <tr>
            <th nowrap="nowrap" class="style5">
                Question
            </th>
            <th nowrap="nowrap" class="style4">
                Answers
            </th>
            <%--<% foreach (var item2 in Model){ %>
                    <%= Html.Encode(item2.User)%>
              </th>

            <%} %>--%>
            
        </tr>
    <% String result2 = "";
       int tempquestionnumber = 0;
     %>
   <% foreach (var item in Model) { %>
     <tr>
            <td nowrap="nowrap" class="style5">
                <%                   
                   int iquestion = item.questnum;
                   if (tempquestionnumber == iquestion) { result2 = ""; }
                   else { tempquestionnumber = iquestion; result2 = tempquestionnumber.ToString(); }
                 %>
                <%= Html.Encode(result2)%>
            </td>
            <td nowrap="nowrap" class="style4">
            <%
                String result;
                switch (item.answer)
                {
                    case 1:
                        result = "a";
                        break;
                    case 2:
                        result = "b";
                        break;
                    case 3:
                        result = "c";
                        break;
                    case 4:
                        result = "d";
                        break;
                    case 5:
                        result = "e";
                        break;
                    case 6:
                        result = "f";
                        break;
                    case 7:
                        result = "g";
                        break;
                    case 8:
                        result = "h";
                        break;
                    case 9:
                        result = "i";
                        break;
                    case 10:
                        result = "j";
                        break;
                    default:
                        result = "Answered";
                        break;
                }
             %>
                <%= Html.Encode(result)%>
            </td>     
        </tr>
    <% } %>
    </table>
    <br />
       <% using (Html.BeginForm()) {%>
             <input type ="submit" value = "click me" />
             <p><%=ViewData["test"]%></p>
       <% } %>
    <br />
    <br />
    <br />

    <p class = "p"> Report for short answer questions:</p>
    <br />


</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">

        .style7
        {
            width : 100px;
        }
        .p
        {
            font-weight:bold;
        }
    </style>
</asp:Content>

