﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	TestDevices
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="http://java.com/js/deployJava.js"></script>

    <script type="text/javascript">
        var attributes = { code: '/Content/pollapplet.PollApplet', width: 800, height: 600 };
        var parameters = { jnlp_href: '/Content/launch.jnlp' };
        deployJava.runApplet(attributes, parameters, '1.6'); 
    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
