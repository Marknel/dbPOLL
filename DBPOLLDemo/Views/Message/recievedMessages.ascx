<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


<% using (Html.BeginForm())
   {%>
<fieldset>
    <legend>Recieved Messages</legend>
    <table>
        <tr>
        <th nowrap="nowrap">
                Actions
            </th>
            <th nowrap="nowrap">
                Message
            </th>
            <th nowrap="nowrap">
                Date sent (created_at)
            </th>
            <th nowrap="nowrap">
                Poll
            </th>
            <th nowrap="nowrap">
                Sender
            </th>
            <th nowrap="nowrap">
                Reciever
            </th>
        </tr>
        <%
            DBPOLLDemo.Models.messageModel msgModel = new DBPOLLDemo.Models.messageModel();
           List<DBPOLLDemo.Models.messageModel> Model = msgModel.recievedMessages((int)Session["uid"]);
            %>
        <% foreach (var item in Model)
           { %>
        <tr>
         <td nowrap="nowrap">
                    <%= Html.ActionLink("Reply", "ReplyConfirm", new { UserID = item.UserID })%>
                </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.Message)%>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.Created_at)%>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.poll_ID)%>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.senderName)%>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.recieverName)%>
            </td>
        </tr>
        <% } %>
    </table>
</fieldset>
<% } %>