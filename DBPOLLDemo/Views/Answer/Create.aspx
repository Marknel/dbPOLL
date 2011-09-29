<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Models.answerModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create <%=Html.Encode(ViewData["error1"])%></h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Create Answer</legend>
             <p>
                <label for="weight">Answer Number:</label>
                <%= Html.TextBox("ansnum")%>
                <%= Html.ValidationMessage("ansum", "*")%>
            </p>
            <p style ="color: Red;"><%=ViewData["ansnumerror"]%></p>
            <p>
                <label for="answer">Answer Text:</label>
                <%= Html.TextBox("answer")%>
                <%= Html.ValidationMessage("answer", "*")%>
            </p>
            <p style ="color: Red;"><%=ViewData["answererror"]%></p>
            <p>
                <label for="correct">Correct Answer:</label>
                <select id="correct" name="correct">
                <option value="0">No</option>
                <option value="1">yes</option>
                </select>
                <%= Html.ValidationMessage("correct", "*")%>
                <%=Html.Hidden("questionid", ViewData["questionid"])%>
            </p>
            <p>
                <label for="weight">Answer Weight:</label>
                <%= Html.TextBox("weight")%>
                <%= Html.ValidationMessage("weight", "*")%>
            </p>
            <p style ="color: Red;"><%=ViewData["weighterror"]%></p>
            <p><%=ViewData["created"]%></p>
            <p style ="color: Red;"><%=ViewData["mastererror"]%></p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to Answer List", "Index", new { id = ViewData["questionid"]})%>
    </div>

</asp:Content>

