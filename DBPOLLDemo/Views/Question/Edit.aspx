<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Models.questionModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Question: <%=Model.question + " " + ViewData["quest"]%></h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <script type="text/javascript">
      function PreselectMyItem(itemToSelect)
      {

        // Get a reference to the drop-down
        var myDropdownList = document.getElementById(questiontype);

        // Loop through all the items
        for (iLoop = 0; iLoop< myDropdownList.options.length; iLoop++)
        {    
          if (myDropdownList.options[iLoop].value == itemToSelect)
          {
            // Item is found. Set its selected property, and exit the loop
            myDropdownList.options[iLoop].selected = true;
            break;
          }
        }
      }
      function questiontype_onclick() {

      }

    </script>
    <% using (Html.BeginForm()){%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <%= Html.Hidden("questionid", Model.questionid)%>
            </p>
            <p>
                <label for="questiontype">Question Type:</label>
                
                <select onload ="PreselectMyItem(<%= Model.questiontype %>)" id="questiontype" name="questiontype" onclick="return questiontype_onclick()">
                <option value="1">Short Answer: Numeric Responses Only</option>
                <option value="2">Short Answer: Alphanumeric Responses Only</option>
                <option value="3">Multiple Choice: Standard</option>
                <option value="4">Multiple Choice: Demographic</option>
                <option value="5">Multiple Choice: Comparative</option>
                <option value="6">Multiple Choice: Ranking</option>
                </select>
                <%= Html.ValidationMessage("questiontype", "*")%>
                
                
                <!-- <%= Html.TextBox("questiontype", Model.questiontype)%> -->
                
            </p>
            <p>
                <label for="question">Question Text:</label>
                <%= Html.TextBox("question", Model.question)%>
                <%= Html.ValidationMessage("question", "*")%>
            </p>
            <p>
                <label for="chartstyle">Response Chart:</label>
                <select id="chartstyle" name="chartstyle">
                <option value="1">Horizontal Bar Graph</option>
                <option value="2">Vertical Bar Graph</option>
                <option value="3">Line Graph</option>
                <option value="4">Pie Chart</option>
                </select>
                
                <%= Html.ValidationMessage("CHARTSTYLE", "*") %>
            </p>
            <p>
                <label for="num">Question in sequence:</label>
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
                <input type="submit" value="Save Changes" />
            </p>
        </fieldset>

    <% } %>

    <div>
       <%=Html.ActionLink("Back to Question List", "Index", new { id = Model.pollid })%>
    </div>

</asp:Content>

