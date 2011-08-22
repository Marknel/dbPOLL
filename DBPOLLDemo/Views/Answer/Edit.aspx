<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Models.answerModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Edit Answer</legend>
            <p>
                <label for="weight">Answer Number:</label>
                <%= Html.TextBox("ansnum", Model.ansnum)%>
                <%= Html.ValidationMessage("ansum", "*")%>
            </p>
            <p style ="color: Red;"><%=ViewData["ansnumerror"]%></p>
            <p>
                <label for="answer">Answer Text:</label>
                 <%= Html.TextBox("answer", Model.answer) %>
                <%= Html.ValidationMessage("ANSWER1", "*") %>
            </p>
            <p style ="color: Red;"><%=ViewData["answererror"]%></p>
            <%=Html.Hidden("questionid", ViewData["questionid"])%>
             <%=Html.Hidden("answerid", Model.answerid)%>
             <%=Html.Hidden("createdat", Model.createdat)%>
            <p>
               <label for="correct">Correct Answer:</label>
                <select id="correct" name="correct">
                <option value="0">No</option>
                <option value="1">yes</option>
                </select>
            </p>
            <p>
                <label for="weight">Answer Weight:</label>
                <%= Html.TextBox("weight", Model.weight)%>
                <%= Html.ValidationMessage("weight", "*")%>
            </p>
            <p><%=ViewData["created"]%></p>
            <p style ="color: Red;"><%=ViewData["mastererror"]%></p>



            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to Answer List", "Index", new { id = ViewData["questionid"]})%>
    </div>

</asp:Content>

