<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CreateTrueFalse
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>True/False Answer Template</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Create True False Answer</legend>

            <p>
                <label for="weight">Answer Number:</label>
                <%= Html.TextBox("ansnum")%>
                <%= Html.ValidationMessage("ansum", "*")%>
            </p>
            <p style ="color: Red;"><%=ViewData["ansnumerror"]%></p>
            <p>
                <label for="answer">Answer Text:</label>
                <%= Html.TextBox("answer", "true")%>
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

            <br />
            <hr />

            <p>
                <label for="weight">Answer Number:</label>
                <%= Html.TextBox("ansnum1")%>
                <%= Html.ValidationMessage("ansum", "*")%>
            </p>
            <p style ="color: Red;"><%=ViewData["ansnumerror"]%></p>
            <p>
                <label for="answer">Answer Text:</label>
                <%= Html.TextBox("answer1", "false")%>
                <%= Html.ValidationMessage("answer", "*")%>
            </p>
            <p style ="color: Red;"><%=ViewData["answererror"]%></p>
            <p>
                <label for="correct">Correct Answer:</label>
                <select id="Select1" name="correct1">
                <option value="0">No</option>
                <option value="1">yes</option>
                </select>
                <%= Html.ValidationMessage("correct", "*")%>
                <%=Html.Hidden("questionid", ViewData["questionid"])%>
            </p>
            <p>
                <label for="weight">Answer Weight:</label>
                <%= Html.TextBox("weight1")%>
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

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

