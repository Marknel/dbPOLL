<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLContext.ANSWER>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="ANSWERID">ANSWERID:</label>
                <%= Html.TextBox("ANSWERID") %>
                <%= Html.ValidationMessage("ANSWERID", "*") %>
            </p>
            <p>
                <label for="ANSWER1">ANSWER1:</label>
                <%= Html.TextBox("ANSWER1") %>
                <%= Html.ValidationMessage("ANSWER1", "*") %>
            </p>
            <p>
                <label for="CORRECT">CORRECT:</label>
                <%= Html.TextBox("CORRECT") %>
                <%= Html.ValidationMessage("CORRECT", "*") %>
            </p>
            <p>
                <label for="WEIGHT">WEIGHT:</label>
                <%= Html.TextBox("WEIGHT") %>
                <%= Html.ValidationMessage("WEIGHT", "*") %>
            </p>
            <p>
                <label for="NUM">NUM:</label>
                <%= Html.TextBox("NUM") %>
                <%= Html.ValidationMessage("NUM", "*") %>
            </p>
            <p>
                <label for="UPDATEDTO">UPDATEDTO:</label>
                <%= Html.TextBox("UPDATEDTO") %>
                <%= Html.ValidationMessage("UPDATEDTO", "*") %>
            </p>
            <p>
                <label for="CREATEDAT">CREATEDAT:</label>
                <%= Html.TextBox("CREATEDAT") %>
                <%= Html.ValidationMessage("CREATEDAT", "*") %>
            </p>
            <p>
                <label for="MODIFIEDAT">MODIFIEDAT:</label>
                <%= Html.TextBox("MODIFIEDAT") %>
                <%= Html.ValidationMessage("MODIFIEDAT", "*") %>
            </p>
            <p>
                <label for="QUESTIONID">QUESTIONID:</label>
                <%= Html.TextBox("QUESTIONID") %>
                <%= Html.ValidationMessage("QUESTIONID", "*") %>
            </p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to Answer List", "Index", new { id = ViewData["id"], name = ViewData["name"]})%>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

