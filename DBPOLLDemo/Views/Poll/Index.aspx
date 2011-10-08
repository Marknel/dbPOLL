<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Controllers.PollAndSessionData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
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
                return true
            }
        }    
     </script>

    <h2>Poll Index </h2>

    <table>
        <tr>
            <th nowrap="nowrap">Actions</th>
             <th nowrap="nowrap">Poll Name</th>
             <th nowrap="nowrap">Creation Date</th>
        </tr>
    <% foreach (var item in Model.pollData) { %>    
        <tr>
            <td nowrap="nowrap">
                <a id= "<%=item.pollid%>" href="/Poll/Index/" onclick="return check(<%=item.pollid%>);"> Delete</a> 
                <a id = "<%= item.pollid + "yes" %>" href="/Poll/Delete?pollid=<%=item.pollid%>"></a>
                <a id = "<%= item.pollid + "no" %>" href="/Poll/Index/"></a> |
               
                <%= Html.ActionLink("Edit", "Edit", new {name=item.pollname, id = item.pollid}) %> |
                <%= Html.ActionLink("View Questions", "Details", new {id=item.pollid, name=item.pollname})%> |
                <%= Html.ActionLink("View Default Objects", "../PollObject/Index", new { pollid = item.pollid, pollname = item.pollname })%> |
                <%= Html.ActionLink("Create New Session", "CreateSession", new {pollID = item.pollname, pollName = item.pollname})%>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.pollname) %>
            </td>
              <td nowrap="nowrap">
                <%= Html.Encode(String.Format("{0:g}", item.createdAt)) %>
            </td>

            <%  //Check if the user is authorized to assign poll masters
            if (Int32.Parse(Session["user_type"].ToString()) > 2)
          { %>
            <td nowrap="nowrap">
                <%= Html.ActionLink("Assign Poll Masters", "AssignPoll", new {pollid=item.pollid, pollname = item.pollname}) %>
            </td>
            <%} %>


        </tr>
    
    <% } %>

    </table>

    <p>

        <%  //Check if the user is authorized to create polls
            if (Int32.Parse(Session["user_type"].ToString()) > 2)
          { %>
            <%= Html.ActionLink("Create New Poll", "Create", new { createdby = (int)Session["uid"] })%>
        <%} %>
    </p>

    <h2>Session Index </h2>

    <table>
        <tr>
            <th class="style2">Actions</th>
             <th class="style2">Session Name</th>
             <th class="style2">Poll Name</th>
        </tr>

    <% foreach (var item in Model.sessionData)
       { 
           
           %>
    
        <tr>
            <td nowrap="nowrap">
                <%= Html.ActionLink("Run", "Run", new {sessionid = item.sessionid}) %> |

                <a id= "<%= item.sessionid + "s"%>" href="/Poll/Index/" onclick="return check(<%=item.sessionid + "s"%>);">Delete</a> 
                <a id = "<%= item.sessionid + "syes" %>" href="/Poll/DeleteSession?sessionid=<%=item.sessionid%>"></a>
                <a id = "<%= item.sessionid + "sno" %>" href="/Poll/Index/"></a> |

                <%= Html.ActionLink("Edit", "EditSession", new {sessionname=item.sessionName, pollid = item.pollid, sessionid = item.sessionid, longitude = item.longitude, latitude = item.latitude, time = item.time}) %>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.sessionName) %>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.pollname) %>
            </td>
        </tr>
    
    <% } %>

    </table>
</asp:Content>


