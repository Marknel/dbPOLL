<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Controllers.PollAnswerAndSession>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ConfirmationPage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Thank you</h2>

    <%foreach (var item in Model.sessionData)
      { %>

    <p> Thank you for participating on this session. </p>
    <p> This is the end of the question. Session <strong><%=Html.Encode(item.sessionname) %></strong> is now closed </p>
    <p> The below graphs display the number of users who responded to the questions.</p>

    <br />
    <br />

    <%--<p> The following is a collated graph from all the participants in this session </p>--%>
   <%-- <img src="<%= Url.Action("TwoDimensionalChart", new {chartParameter = "abc"}) %>" alt="image" /> --%>

    <%} %>

    <% int i = 0; %>
    <% foreach (var question in Model.questionData){ %>

            <fieldset>
            <br />
            <br />
               <p style="width: 720px"> Question: <%=Html.Encode(question.question) %></p>
            
            <br />
            <br />
            <br />

            <% if (question.questiontype > 2)
               {%>
                    <% 
           
                List<String> xValues = new List<String>();
                List<int> yValues = new List<int>();

                foreach (var answers in Model.answerData[i])
                {
                    xValues.Add(answers.answer);
                }

                foreach (var response in Model.responseData[i])
                {
                    yValues.Add(response);
                }

                i++;
           
                   %>

                   <%
                String finalResult = "";
                int j = 0;
                foreach (String x in xValues)
                {
                    finalResult += x;
                    if (j + 1 < xValues.Count())
                    {
                        finalResult += "/";
                    }

                    j++;
                }

                finalResult += ",";
                j = 0;

                foreach (int y in yValues)
                {
                    finalResult += Convert.ToInt32(y);
                    if (j + 1 < yValues.Count())
                    {
                        finalResult += "/";
                    }
                    j++;
                }
                
            
                %>


                <% if(finalResult != ","){ %>
                
                    <img src="<%= Url.Action("TwoDimensionalChart", new {chartParameter = finalResult}) %>" alt="image" /> 
                
                <%} %>

          <%} %>

          
          <br />
          <br />
          <br />
          <br />
          <br />
          </fieldset>
           
    <%} %>

    <br />
    <br />

    <%= Html.ActionLink("Choose other sessions", "../Session/ViewAvailableSession", new { userid = (int)Session["uid"] })%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
