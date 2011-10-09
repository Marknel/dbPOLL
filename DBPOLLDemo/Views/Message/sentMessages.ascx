<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<% using (Html.BeginForm())
   {%>
<fieldset>
    <legend>Sent Messages</legend>
    <table>
        <tr>
            <th nowrap="nowrap">
                Message
            </th>
            <th nowrap="nowrap">
                Date sent
            </th>
            <th nowrap="nowrap">
                Poll Name
            </th>
          
            <th nowrap="nowrap">
                Reciever
            </th>
        </tr>
        <%
            DBPOLLDemo.Models.messageModel msgModel = new DBPOLLDemo.Models.messageModel();
            List<DBPOLLDemo.Models.messageModel> Model = msgModel.sentMessages((int)Session["uid"]);
            %>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td nowrap="nowrap">
                <%= Html.Encode(item.Message)%>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.Created_at)%>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.pollName)%>
            </td>
           
            <td nowrap="nowrap">
                <%= Html.Encode(item.recieverName)%>
            </td>
        </tr>
        <% } %>
    </table>
</fieldset>
<% } %>