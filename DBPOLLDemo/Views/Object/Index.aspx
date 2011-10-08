<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.questionObjectModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index <%= ViewData["error1"]%></h2>
    <% if (Model.Count() == 0)
       {%>
        <p style ="color: Red;"><%=ViewData["message"]%></p>
    <%} %>
    <%else
        { %>
    <table>
        <tr>
            <th></th>
            <th>
                Object Type
            </th>
            <th>
                Object Attribute
            </th>
        </tr>

    <% foreach (var item in Model)
       { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Delete", "Delete", new { objectid = item.obid, questionid = ViewData["questionid"] })%>
            </td>
            <td>
            <%
        string test;
        switch (item.obid)
        {
            case 1:
                test = "Countdown Timer";
                break;
            case 2:
                test = "Response Counter";
                break;
            case 3:
                test = "Correct Answer Indicator";
                break;
            default:
                test = "";
                break;
        }
           %>
                 <%=Html.Encode(test)%>

            </td>
            <td>
                <%= Html.Encode(item.attribute)%>
            </td>
        </tr>
    
    <% } %>
    <%} %>
    </table>

    <p>
        <%= Html.ActionLink("Create New Object", "Create", new { questionid = ViewData["questionid"] })%>
    </p>

</asp:Content>

