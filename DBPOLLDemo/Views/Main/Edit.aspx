<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLContext.POLL>" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm("Edit","Edit", FormMethod.Post)) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="POLLNAME">POLLNAME:</label>
                <%= Html.Hidden("TEST", 1)%>
                <%= Html.Hidden("POLLID", ViewData["id"])%>
                
                <%= Html.Hidden("LONGITUDE", ViewData["longitude"])%>
                
                <%= Html.Hidden("LATITUDE", ViewData["latitude"])%>
                
                <%= Html.Hidden("CREATEDBY", ViewData["createdby"])%>
                
                <%= Html.Hidden("CREATEDAT", ViewData["createdat"])%>
                
                <%= Html.TextBox("pollname") %>
                <%= Html.ValidationMessage("POLLNAME", "*") %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

