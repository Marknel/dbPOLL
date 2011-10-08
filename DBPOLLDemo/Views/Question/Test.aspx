<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.answerModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Testing: 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Test: </h2>

    <p><%= Html.Encode(ViewData["name"]) %></p>
    <% if (Model.Count() < 1)
       {%>
            <p style ="color: Red;"><%=ViewData["message"]%></p>
        <%}%>

    <% else
        {%>
            <% int[] id = new int[Model.ToArray().Length]; %>
            <% int i = 0; %>
            <% foreach (var item in Model)
               { %>
                        <% id[i] = item.answerid; %>
                        <% i++; %>
                        <%= Html.ActionLink(item.ansnum.ToString(), "Edit", new { id = item.answerid })%>

                        <%= Html.Encode(item.answer)%>

                        <br />
            <% } %>
            <% int r = new Random().Next(0, i); %>

            <hr />
            <%= Html.ActionLink("Random Answer", "Result", new { aid = id[r] })%>
            <br />
            <%= Html.ActionLink("Test via Keypad", "Keypad", new { qid = ViewData["qid"] })%>
    <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
