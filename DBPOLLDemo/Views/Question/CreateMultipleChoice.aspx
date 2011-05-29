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
                <label for="questiontype">Multiple Choice Type:</label>
                <select id="questiontype" name="questiontype">
                <option value="3">Standard</option>
                <option value="4">Demographic</option>
                <option value="5">Comparative</option>
                <option value="6">Ranking</option>
                </select>
                
                <%= Html.ValidationMessage("questiontype", "*")%>
                
            </p>
            
            <p>
                <label for="num">Question in sequence:</label>
                <%= Html.TextBox("num")%>
                <%= Html.ValidationMessage("num", "*")%>
            <p>
            
            <p>
                <label for="question">Question Text:</label>
                <%= Html.TextBox("question")%>
                <%= Html.ValidationMessage("question", "*")%>
            </p>
            <p>
                <label for="chartstyle">Response Chart</label>
                <select id="chartstyle" name="chartstyle">
                <option value="">(Select one)</option>
                <option value="1">Horizontal Bar Graph</option>
                <option value="2">Vertical Bar Graph</option>
                <option value="3">Line Graph</option>
                <option value="4">Pie Chart</option>
                </select>
  
                <%= Html.ValidationMessage("chartstyle", "*")%>
            </p>
            
                <input type="submit" value="Create Question" />
            </p>
        </fieldset>

    <% } %>

    <div>
         <%=Html.ActionLink("Back to Question List", "Index", new { id = ViewData["id"] }) %>
    </div>

</asp:Content>

