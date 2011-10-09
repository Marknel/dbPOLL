<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Controllers.PollAndQuestions>" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	StartSession
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <p class="style1"> <strong>You are now participating in session: <%=Html.Encode(Model.sessionData[0].sessionname) %>
    
        </strong></p>


    <% 
        int currentQuestion = (int)Session["currentQuestionNumber"];
    %>
    <% using (Html.BeginForm()){%>

            <div style="text-align: center">
            <fieldset>
            <legend> <%=Html.Encode("Question " + Model.questionData.questnum)%></legend>

                    
                <%=Html.Encode(Model.questionData.question)%>
                <br />
                <br />
                <br />

                <%--if there is answerdata, then it's multiple choice type of question--%>
                <% 
           //if (Model.answerData.Count() != 0){ 
           
                if (Model.questionData.questiontype >= 3 && Model.questionData.questiontype <6) {%>

                    <%foreach (var answers in Model.answerData)
                        {


                            if (answers.questionid == Model.questionData.questionid)
                            { 
                                
                                if ((String)Session["selectedAnswer"] != null || (String)Session["selectedAnswer"] != "")
                                { 
                                        
                                    if ((String)Session["selectedAnswer"] == answers.answer)
                                    { %>    

                                    <center>
                                        <p>
                                            <label>
                                            <%=Html.RadioButton("MCQAnswer", answers.answerid, true)%>     
                                            <%=Html.Encode(answers.answer)%> 
                                            </label>
                                        </p>
                                    </center>

                                    <%} 
                                        
                                    else 
                                    {%>
                                        <center>
                                            <p>
                                                <label>
                                                <%=Html.RadioButton("MCQAnswer", answers.answerid)%>     
                                                <%=Html.Encode(answers.answer)%> 
                                                </label>
                                            </p>
                                        </center>
                                    <%} %>

                                <%} 
                                else {%>
                                <center>
                                    <p>
                                        <label>
                                        <%=Html.RadioButton("MCQAnswer", answers.answerid)%>     
                                        <%=Html.Encode(answers.answer)%> 
                                        
                                        </label>
                                    </p>
                                </center>
                                <%} %>

                                <br />
                            <%}

                        }%>
                        <br />

                <%} %>
                <% 
                else if (Model.questionData.questiontype <= 2){ %>
                        <center>
                            <br />
                            <br />
                            <% if ((String)Session["shortAnswer"] != null || (String)Session["shortAnswer"] != "") { %>
                                <%=Html.TextArea("ShortQuestionAnswer", (String)Session["shortAnswer"]) %>
                            <%} %>
                        </center>
                <%} 
                else if (Model.questionData.questiontype == 6){%>

                <% 
                    
                    int numOfPossibleAnswers = (int)ViewData["numOfPossibleAnswers"];
                    for (int i = 0; i < numOfPossibleAnswers; i++) {  %>

                    <center>
                        <p>
                            Preference <%=i+1 %>:   <%= Html.DropDownList("RankingAnswerList")%>
                        </p>
                        <br />
                        
                    </center>
                    <%}%>
                 <hr style="width: 431px" />
                 <center>
                 <p style="width: 467px"><%= ViewData["RankingAnswerHistory"] %></p>
                 </center>

                <%} %>
                    

            <%= Session["completed"]%>
            <%= Html.ValidationMessage("webpollingError")%>
            <br />
            <br />
            <br />

            <center>
                <% Html.RenderPartial("../Message/sendFeedback"); %>
            </center>
            <br />     

            <% 
           if (currentQuestion == 0 && (Boolean)Session["endOfQuestion"] == false){%>

                    <button type="submit" name = "button" value="Previous Question" disabled = true> Previous Question </button>
                    <button type="submit" name = "button" value="Next Question"> Next Question </button>

            <% } else if (currentQuestion == 0 && (Boolean)Session["endOfQuestion"] == true)

            { %>
                        <%--this is how to disable a button--%>
                       <%-- <button type="submit" name = "button" value="Next Question" disabled = true> Next Question </button>--%>

                    <button type="submit" name = "button" value="Previous Question" disabled = true> Previous Question </button>
                    <button type="submit" name = "button" value="Submit Last Answer"> Submit Last Answer </button>

            <% }  else if ((Boolean)Session["endOfQuestion"] == true)

            { %>

                    <button type="submit" name = "button" value="Previous Question"> Previous Question </button>
                    <button type="submit" name = "button" value="Submit Last Answer"> Submit Last Answer </button>
            <%} else 
           {%>

                    <button type="submit" name = "button" value="Previous Question"> Previous Question </button>
                    <button type="submit" name = "button" value="Next Question"> Next Question </button>

             <%} %>

            </fieldset>
            </div>

            <br />
            <center>
                <%= Html.ActionLink("Choose other sessions", "../Session/ViewAvailableSession", new { userid = (int)Session["uid"] })%>
            </center>

    <%} %>

    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: medium;
        }
    </style>
</asp:Content>
