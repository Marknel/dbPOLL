<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.answerModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Answers for : <%= Html.Encode(ViewData["name"]) %>
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


    <h2>Answers for : <%= Html.Encode(ViewData["name"]) %></h2>

    <table>
        <tr>
            <th>Actions</th>
            <th>Number</th>
            <th>Answer</th>
            <th>Weight</th>
            <th>Correct</th>
            <th>Created On</th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>

            <a id= "<%= item.answerid %>" href="/Answer/Index/<%= ViewData["questionid"]%>" onclick="return check(<%=item.answerid%>);">Delete</a> 
            <a id = "<%= item.answerid + "yes" %>" href="/Answer/Delete/?answerid=<%=item.answerid%>&questionid=<%=ViewData["questionid"]%>"></a>
            <a id = "<%= item.answerid + "no" %>" href="/Answer/Index/<%=ViewData["questionid"]%>"></a> |

                <%= Html.ActionLink("Edit", "Edit", new { answerid = item.answerid, questionid = ViewData["questionid"] })%> | 
                <%= Html.ActionLink("View Answer History", "Details", new {id = item.answerid, name = item.answer})%>
            </td>
            
            <td>
                <%= Html.Encode(item.ansnum) %>
            </td>

            <td>
                <%= Html.Encode(item.answer) %>
            </td>
            
            <td>
                <%= Html.Encode(item.weight) %>
            </td>
            
            <td><%String correct;
                  if (item.correct == 0) { correct = ""; }
                  else { correct = "Yes"; }
            %>
                <%= Html.Encode(correct)%>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(String.Format("{0:g}", item.createdat)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New Answer", "CreateMethod", new { questionid = ViewData["questionid"], name = ViewData["name"] })%>
    </p>

</asp:Content>

