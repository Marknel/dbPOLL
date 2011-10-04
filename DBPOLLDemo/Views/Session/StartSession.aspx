<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Controllers.PollAndQuestions>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	StartSession
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>StartSession</h2>


    <% foreach (var item in Model.sessionData)
       { %>

       <p> <%=Html.Encode("Session name: " +item.sessionname) %></p>
       <p> <%=Html.Encode("Session id: "+ item.sessionid) %></p>
       <p> <%=Html.Encode("poll id: " + item.pollid)%></p>
       <p> <%=Html.Encode("Poll expires at: " + item.expiresat)%></p>

    <%} %>

    <br />
    <br />
    <br />
    <br />

    <% foreach (var item in Model.questionData)
       { %>

       <p> <%=Html.Encode("Question number: " + item.questnum)%></p>
       <p> <%=Html.Encode("Question: " +item.question) %></p>
       <p> <%=Html.Encode("Question id: "+ item.questionid) %></p>
       <p> <%=Html.Encode("Question type: " + item.questiontype)%></p>

       <% if (item.questiontype == 3){ %>
            
            <%foreach (var item2 in Model.questionData)
              { %>

                <% if (item2.question == item.question)
                   { %>
                      <p> <%=Html.Encode("Answer Choice: " + item2.answer)%></p>

                <% }%>
            <%} %>

        <%} %>

    <%} %>

    <br />
    <br />
    <br />
    <br />

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
