<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<fieldset>
    <legend>Please provide some feedback</legend>
    <p>
        <label for="msg">Message:</label>
        <%= Html.TextBox("msg")%>
        <p style="color: Red;">
            <%= Html.Encode(ViewData["msgError"])%></p>
    </p>
</fieldset>
