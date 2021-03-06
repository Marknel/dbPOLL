<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Models.POLL>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm("Create","Create", FormMethod.Post)) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="POLLNAME">Poll Name:</label>

                <%= Html.TextBox("name") %>
                <%= Html.ValidationMessage("POLLNAME", "*") %>
            </p>
                <%= Html.Hidden("createdby", Session["uid"])%>
            <p>
                <input type="submit" value="Create Poll"/>
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>
