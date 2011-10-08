<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<DBPOLLDemo.Models.messageModel>>" %>

<%@ Import Namespace="DBPOLLDemo.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Send Private Message
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>
        Edit User:</h2>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Fields</legend>
        <p>
            <%= Html.Hidden("userid")%>
            <%= Html.Hidden("createdat")%>
        </p>
        <p>
            <label for="msg">
                Message:</label>
            <%= Html.TextBox("msg")%>
            <p style="color: Red;">
                <%=ViewData["msgError"]%></p>
        </p>
        <p>
            <%= Html.DropDownList("USER_LIST")%>
        </p>
        <p style="color: Red;">
            <%=ViewData["edited"]%></p>
        <p>
            <input type="submit" value="Save Changes" />
        </p>
    </fieldset>
    <%
        }%>
    <p>
        <%=Html.ActionLink("Back to messages", "Index")%>
    </p>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
