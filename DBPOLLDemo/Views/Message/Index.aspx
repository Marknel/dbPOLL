<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<DBPOLLDemo.Models.messageModel>>" %>

<%@ Import Namespace="DBPOLLDemo.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	My Messages
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>
    My Messages</h2>
     <p>
        <%= Html.ActionLink("Send a message to another user", "sendPrivateMessage")%>
    </p>
     <br /><br />
     <% Html.RenderPartial("recievedMessages"); %>
     <br /><br />
     <% Html.RenderPartial("publicMessages"); %>
     <br /><br />
     <% Html.RenderPartial("sentMessages"); %>     
     <br /><br />
     

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
