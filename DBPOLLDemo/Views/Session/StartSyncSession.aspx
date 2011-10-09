<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Controllers.PollAndQuestions>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	StartSyncSession
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%--    <script type="text/javascript">

    myInterval = setInterval(function () {
        $.get('/Session/caniseethegraph', function(data) {
        if(data == "True") {window.open("http://www.google.com");}
        });
    }

    </script>--%>


    <h2>StartSyncSession</h2>

    <% using (Html.BeginForm())
       {%>

        <div style="text-align: center">
            <fieldset>
            <legend> <%=Html.Encode("Question " + Model.questionData.questnum)%></legend>
            

            <%=Html.Encode(Model.questionData.question)%>
            <br />
            <br />
            <br />

            <% 
            if (Model.questionData.questiontype >= 3 && Model.questionData.questiontype < 6)
            {%>

                <%foreach (var answers in Model.answerData)
                    {%>

                        <center>
                            <p>
                                <label>
                                <%=Html.RadioButton("MCQAnswer", answers.answerid)%>     
                                <%=Html.Encode(answers.answer)%> 
                                </label>
                            </p>
                        </center>
                                   
                <% }
                    
            }
            else if (Model.questionData.questiontype <= 2)
            { %>

                <br />
                <br />
                <center>

                    <%=Html.TextArea("ShortQuestionAnswer")%>

                </center>
            <%}
            else if (Model.questionData.questiontype == 6)
            {
                    
                int numOfPossibleAnswers = (int)Session["numOfPossibleAnswers"];
                for (int i = 0; i < numOfPossibleAnswers; i++)
                {  %>

                <center>
                    <p>
                        Preference <%=i + 1%>:   <%= Html.DropDownList("RankingAnswerList")%>
                    </p>
                    <br />
                        
                </center>
                <%}%>
                

            <%} %>

            <%= Session["wait"]%>
            <%= Html.ValidationMessage("webpollingError")%>
            <br />
            <br />

            <button type="submit" name = "button" value="load"> Submit Answer </button>

            <br />
            <br />
            <br />

            <center>
                <% Html.RenderPartial("../Message/sendFeedback"); %>
            </center>
            <br />    
                    
            </fieldset>
    <%} %>

</asp:Content>


