<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Models.questionModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit <%=ViewData["quest"] %></h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <%= Html.Hidden("questionid", Model.questionid)%>
            </p>
            <p>
                <label for="QUESTIONTYPE">QUESTIONTYPE:</label>
                <%= Html.TextBox("questiontype", Model.questiontype)%>
                <%= Html.ValidationMessage("QUESTIONTYPE", "*") %>
            </p>
            <p>
                <label for="QUESTION1">QUESTION1:</label>
                <%= Html.TextBox("question", Model.question)%>
                <%= Html.ValidationMessage("QUESTION1", "*") %>
            </p>
            <p>
                <label for="CHARTSTYLE">CHARTSTYLE:</label>
                <%= Html.TextBox("chartstyle", Model.chartstyle.ToString())%>
                <%= Html.ValidationMessage("CHARTSTYLE", "*") %>
            </p>
            <p>
                <label for="SHORTANSWERTYPE">SHORTANSWERTYPE:</label>
                <%= Html.TextBox("SHORTANSWERTYPE", Model.questiontype.ToString()) %>
                <%= Html.ValidationMessage("SHORTANSWERTYPE", "*") %>
            </p>
            <p>
                <label for="NUM">NUM:</label>
                <%= Html.TextBox("num", Model.questnum)%>
                <%= Html.ValidationMessage("NUM", "*") %>
            </p>
            <p>
                <%= Html.Hidden("createdat", Model.createdat)%>
            </p>
            <p>
 
                <%= Html.Hidden("pollid", Model.pollid)%>
            </p>
            <p>
                <input type="Submit" value="Save Changes" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

