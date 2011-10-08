<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<fieldset>
    <legend>Please provide some feedback</legend>
    <p>
        <label for="msg">
            Message:</label>
        <%= Html.TextBox("msg")%>
        <p style="color: Red;">
            <%=ViewData["msgError"]%></p>
    </p>
    <p style="color: Red;">
        <%=ViewData["edited"]%></p>
    


    <%--<p>
        <label for="msg">Message:</label>
        <%= Html.TextBox("msg")%>
        <p style="color: Red;">
            <%= Html.Encode(ViewData["msgError"])%></p>
    </p>--%>
</fieldset>
