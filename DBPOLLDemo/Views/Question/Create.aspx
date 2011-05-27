<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLContext.QUESTION>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create <%=ViewData["error1"]%></h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="QUESTIONID">QUESTIONID:</label>
                <%= Html.TextBox("QUESTIONID") %>
                <%= Html.ValidationMessage("QUESTIONID", "*") %>
            </p>
            <p>
                <label for="QUESTIONTYPE">QUESTIONTYPE:</label>
                <%= Html.TextBox("QUESTIONTYPE") %>
                <%= Html.ValidationMessage("QUESTIONTYPE", "*") %>
            </p>
            <p>
                <label for="QUESTION1">QUESTION1:</label>
                <%= Html.TextBox("QUESTION1") %>
                <%= Html.ValidationMessage("QUESTION1", "*") %>
            </p>
            <p>
                <label for="NUMBEROFRESPONSES">NUMBEROFRESPONSES:</label>
                <%= Html.TextBox("NUMBEROFRESPONSES") %>
                <%= Html.ValidationMessage("NUMBEROFRESPONSES", "*") %>
            </p>
            <p>
                <label for="CHARTSTYLE">CHARTSTYLE:</label>
                <%= Html.TextBox("CHARTSTYLE") %>
                <%= Html.ValidationMessage("CHARTSTYLE", "*") %>
            </p>
            <p>
                <label for="SHORTANSWERTYPE">SHORTANSWERTYPE:</label>
                <%= Html.TextBox("SHORTANSWERTYPE") %>
                <%= Html.ValidationMessage("SHORTANSWERTYPE", "*") %>
            </p>
            <p>
                <label for="NUM">NUM:</label>
                <%= Html.TextBox("NUM") %>
                <%= Html.ValidationMessage("NUM", "*") %>
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
                <label for="POLLID">POLLID:</label>
                <%= Html.TextBox("POLLID") %>
                <%= Html.ValidationMessage("POLLID", "*") %>
            </p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

