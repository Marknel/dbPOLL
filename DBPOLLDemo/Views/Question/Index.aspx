<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.questionModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	questionDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <script type = "text/javascript">
         // Allows deletion of items to be checked and confimed        
         var clicked = false;
         function check(id) {
             document.getElementById(id + "yes").innerHTML = "Yes";
             document.getElementById(id + "no").innerHTML = "No";
             if (clicked != true) {
                 document.getElementById(id).style.display = "none";

                 clicked = true;
                 return false;
             } else {
                 clicked = false;
                 return true;
             }
         }    
     </script>   
    <h2>Questions for Poll: <%=Html.Encode(ViewData["name"]) %></h2>

    <table>
        <tr>
            <th nowrap="nowrap">Actions</th>
            <th nowrap="nowrap">
                Question Number
            </th>
            <th nowrap="nowrap">
                Question
            </th>
            <th nowrap="nowrap">
                Question Type
            </th>
            <th nowrap="nowrap">
                Date Question Created
            </th>
            
            
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td nowrap="nowrap">

                <a id= "<%= item.questionid %>" href="/Question/Index/<%= ViewData["id"]%>?name=<%= ViewData["name"]%>" onclick="return check(<%=item.questionid%>);"> Delete</a> 
                <a id = "<%= item.questionid + "yes" %>" href="/Question/Delete/<%= ViewData["id"]%>?questionid=<%=item.questionid%>&name=<%= ViewData["name"]%>"></a>
                <a id = "<%= item.questionid + "no" %>" href="/Question/Index/<%= ViewData["id"]%>?name=<%= ViewData["name"]%>"></a> |

                <%= Html.ActionLink("Edit", "Edit", new { questionid = item.questionid })%> |
                <%= Html.ActionLink("View Answers", "Details", new { id = item.questionid, name = item.question })%> |
                <%= Html.ActionLink("View Objects", "../Object/Index", new { questionid = item.questionid})%> |
                <%= Html.ActionLink("Run Test", "Test", new { questionid = item.questionid, name = item.question, num_response = item.numberofresponses})%> 
                
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.questnum) %>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.question) %>
            </td>
            <td nowrap="nowrap">
                <%  
                string test;
                switch(item.questiontype)       
                  {         
                     case 1:   
                        test = "Short Answer: Numeric Responses Only";
                        break;                  
                     case 2:            
                        test = "Short Answer: Alphanumeric Responses Only";
                        break;           
                     case 3:            
                        test = "Multiple Choice: Standard";
                        break; 
                     case 4:            
                        test = "Multiple Choice: Demographic";
                        break;   
                     case 5:            
                        test = "Multiple Choice: Comparative";
                        break;  
                     case 6:            
                        test = "Multiple Choice: Ranking";
                        break;         
                     default:            
                        test ="";         
                        break;      
                   }
           %>
                 <%=Html.Encode(test)%>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(String.Format("{0:g}", item.createdat)) %>
            </td>
            
            
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New Question", "Create", new { pollid = ViewData["id"], name = ViewData["name"] })%>
    </p>

</asp:Content>

