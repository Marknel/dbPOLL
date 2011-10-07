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
        //List<DBPOLLDemo.Models.questionModel> questionList = Model.questionData;
        //List<List<DBPOLLDemo.Models.answerModel>> answerList = Model.answerData;
        //List<DBPOLLDemo.Models.pollModel> sessionList = Model.sessionData;
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

                            foreach (var answer in answers)
                            {

                                if (answer.questionid == Model.questionData.questionid)
                                { %>
                                
                                    <% 
                                    if ((String)Session["selectedAnswer"] != null || (String)Session["selectedAnswer"] != "")
                                    { 
                                        
                                        if ((String)Session["selectedAnswer"] == answer.answer)
                                        { %>    

                                        <center>
                                            <p>
                                                <label>
                                                <%=Html.RadioButton("MCQAnswer", answer.answerid, true)%>     
                                                <%=Html.Encode(answer.answer)%> 
                                                </label>
                                            </p>
                                        </center>

                                        <%} 
                                        else {%>
                                            <center>
                                                <p>
                                                    <label>
                                                    <%=Html.RadioButton("MCQAnswer", answer.answerid)%>     
                                                    <%=Html.Encode(answer.answer)%> 
                                                    </label>
                                                </p>
                                            </center>
                                        <%} %>
                                    <%} else {%>
                                    <center>
                                        <p>
                                            <label>
                                            <%=Html.RadioButton("MCQAnswer", answer.answerid)%>     
                                            <%=Html.Encode(answer.answer)%> 
                                        
                                            </label>
                                        </p>
                                    </center>
                                    <%} %>
                                    <br />
                              
                                <%}
                            }

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

                <% int numOfPossibleAnswers = (int)ViewData["numOfPossibleAnswers"];
                   for (int i = 0; i < numOfPossibleAnswers; i++) { 
                    
                    %>

                    <center>
                        <p>
                            Preference <%=i+1 %>:   <%= Html.DropDownList("RankingAnswerList")%>
                        </p>
                    </center>
                    <%}
                %>

                

                <%} %>
                    


            <%= Html.ValidationMessage("webpollingError")%>


            
            <% if (currentQuestion == 0 && (Boolean)Session["endOfQuestion"] == false){%>

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
            <%} else {%>

                    <button type="submit" name = "button" value="Previous Question"> Previous Question </button>
                    <button type="submit" name = "button" value="Next Question"> Next Question </button>

             <%} %>

            </fieldset>
            </div>


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
