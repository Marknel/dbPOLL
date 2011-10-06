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
        int currentQuestion = (int)Session["currentwebpollingQuestion"];
      
    %>
    <% using (Html.BeginForm()){%>

            <div style="text-align: center">
            <fieldset>
            <legend> <%=Html.Encode("Question " + Model.questionData.questnum)%></legend>

                    
                <%=Html.Encode(Model.questionData.question)%>
                <br />

<%--                <p>
                    <%=Html.RadioButton("ANSWER_LIST", "VALUE_LIST")%>
                </p>--%>

                <%foreach (var answers in Model.answerData)
                    {
                        int i = 1;
                        foreach (var answer in answers)
                        {

                            if (answer.questionid == Model.questionData.questionid)
                            { %>
                                
                                <% 
                                if (Session["selectedAnswer"] != null){ %>    
                                    <% 
                                       if ((int)Session["selectedAnswer"] == answer.answerid)
                                       { %>    
                                        <center>
                                            <p>
                                                <label>
                                                <%=Html.RadioButton("UserAnswer", answer.answerid, true)%>     
                                                <%=Html.Encode(answer.answer)%> 
                                                </label>
                                            </p>
                                        </center>
                                    <%} else {%>
                                        <center>
                                            <p>
                                                <label>
                                                <%=Html.RadioButton("UserAnswer", answer.answerid)%>     
                                                <%=Html.Encode(answer.answer)%> 
                                                </label>
                                            </p>
                                        </center>
                                    <%} %>
                                <%} else {%>
                                 <center>
                                    <p>
                                        <label>
                                        <%=Html.RadioButton("UserAnswer", answer.answerid)%>     
                                        <%=Html.Encode(answer.answer)%> 
                                        </label>
                                    </p>
                                </center>
                                <%} %>
                                <br />
                             
                                    
                        <%}
                        }

                    } %>

            <%  if(currentQuestion == 0)

            { %>
                        <%--this is how to disable a button--%>
                       <%-- <button type="submit" name = "button" value="Next Question" disabled = true> Next Question </button>--%>

                    <button type="submit" name = "button" value="Previous Question" disabled = true> Previous Question </button>
                    <button type="submit" name = "button" value="Next Question"> Next Question </button>

            <% } else if ((Boolean)Session["endOfQuestion"] == true)

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
