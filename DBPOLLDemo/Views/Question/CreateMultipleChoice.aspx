<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLContext.QUESTION>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Multiple Choice Question
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create Multiple Choice Question <%=ViewData["error1"]%></h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Question Type">QUESTIONTYPE:</label>
                <select id="QUESTIONTYPE" name="QUESTIONTYPE">
                <option value="2">Multiple Choice</option>
                <option value="3">Demographic</option>
                <option value="4">Comparative</option>
                <option value="5">Ranking</option>
                </select>
                
                <%= Html.ValidationMessage("QUESTIONTYPE", "*") %>
                
            </p>
            
            <p>
                <label for="NUM">Question in sequence:</label>
                <%= Html.TextBox("NUM") %>
                <%= Html.ValidationMessage("NUM", "*") %>
            <p>
            
            <p>
                <label for="QUESTION1">Question Text:</label>
                <%= Html.TextBox("QUESTION1") %>
                <%= Html.ValidationMessage("QUESTION1", "*") %>
            </p>
            <p>
                <label for="CHARTSTYLE">Response Chart</label>
                <select id="CHARTSTYLE" name="CHARTSTYLE">
                <option value="">(Select one)</option>
                <option value="1">Horizontal Bar Graph</option>
                <option value="2">Vertical Bar Graph</option>
                <option value="3">Line Graph</option>
                <option value="4">Pie Chart</option>
                </select>
  
                <%= Html.ValidationMessage("CHARTSTYLE", "*") %>
            </p>
            
                <input type="submit" value="Create Question" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to Question List", "Index", new { pollid = ViewData["id"] })%>
    </div>

</asp:Content>

