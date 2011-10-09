<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Keypad Question Test
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="http://java.com/js/deployJava.js"></script>

    <script type="text/javascript">
        var attributes = { code: '/Applets/pollapplet.PollApplet', width: 810, height: 610 };
        var parameters = { quest_id: <%=(int)ViewData["qid"]%>, jnlp_href: '/Applets/launch2.jnlp'};
        deployJava.runApplet(attributes, parameters, '1.6'); 
    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>