<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Models.QUESTION>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Short Answer Question
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    <h2>Create Short Answer Question <%=ViewData["error1"]%></h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <%
        
        %>
    <%using (Html.BeginForm())
      {%>
    
        <fieldset>
            <legend>New Question</legend>
            <p>
                <label for="shortanswertype">Short Answer Type:</label>
                <select id="shortanswertype" name="shortanswertype">
                <option value="1">Numeric Responses Only</option>
                <option value="2">Alphanumeric Responses Only</option>
                </select>
                
                <%= Html.ValidationMessage("shortanswertype", "*")%>
                <%=Html.Hidden("pollid", ViewData["id"])%>
                
            </p>
            <p>
                <label for="num">Number of question in sequence:</label>
                <%= Html.TextBox("num") %>
            </p>
            <p style ="color: Red;"><%=ViewData["numerror"]%></p>
            
            <p>
                <label for="question">Question Text:</label>
                <%= Html.TextBox("question") %>
                <%= Html.ValidationMessage("question", "*") %>
            </p>
            <p style ="color: Red;"><%=ViewData["questionerror"]%></p>
            <p>
                <label for="chartstyle">Response Chart</label>
                <select id="chartstyle" name="chartstyle">
                <option value="1">Horizontal Bar Graph</option>
                <option value="2">Vertical Bar Graph</option>
                <option value="3">Line Graph</option>
                <option value="4">Pie Chart</option>
                </select>
                
                <%= Html.ValidationMessage("chartstyle", "*")%>
            </p>
            <p><%=ViewData["created"]%></p>

            <p style ="color: Red;"><%=ViewData["mastererror"]%></p>
            <p>
                <input type="submit" value="Create Question" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to Question List", "Index", new { id = ViewData["id"] }) %>
    </div>

</asp:Content>

