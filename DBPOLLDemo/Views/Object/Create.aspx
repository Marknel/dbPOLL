<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.questionObjectModel>>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create Object</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>



        <fieldset>
            <legend>New Object</legend>
            <p>
                <label for="obtype">Object Type:</label>
                <select id="obtype" name="obtype">
                <option value="1">Countdown Timer</option>
                <option value="2">Response Counter</option>
                <option value="3">Correct Answer Indicator</option>
                </select>
                <%= Html.ValidationMessage("obtype", "*")%>
                <%=Html.Hidden("questionid", ViewData["questionid"])%>
            </p>
            <p>
                <label for="attribute">Object Attributes:</label>
                <%= Html.TextBox("attribute")%>
                <%= Html.ValidationMessage("attribute", "*")%>
            </p>
            <p><%= ViewData["created"] %></p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to Object List", "Index", new { questionid = ViewData["questionid"] }) %>
    </div>

</asp:Content>

