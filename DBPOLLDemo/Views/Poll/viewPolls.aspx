<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.pollModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
 
    <h2>Polls</h2>
    <% using (Html.BeginForm("Edit","Edit", FormMethod.Post)) {%>
        <fieldset>
            <legend>Search by Date and Time (mm/dd/yyyy [hh:mm])</legend>
                <div>
                <div style="float:left; padding-right:25px;"> 
                    <label for="date1">Start Date and Time</label> 
                    <%= Html.TextBox("date1")%><br /> 
                    <%= ViewData["date1"]%>
                </div>
                
                <div style="float:left; padding-left:10px;"> 
                    <label for="date2">End Date and Time</label> 
                    <%= Html.TextBox("date2")%><br /> 
                    <%= ViewData["date2"]%>
                </div>
                </div>
                 <br />
                  <br />
                   <br />
                    <br />
                <br />
               
                <div>
               
                    <input type="submit" value="Search" />
                    
                </div>
        </fieldset>
     
    <table>
        <tr>
            <th>Actions</th>
             <th>Poll Name</th>
             <th>Date Poll Created</th>
        </tr>
        <tr>
            <td><%= Html.ActionLink(" Search All Questions", "../Question/viewQuestions", new {pollid=0 })%></td>
            <td></td>
            <td></td>
        </tr>

    <%
        foreach (var item in Model) {
           %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Search Questions", "../Question/viewQuestions", new {pollid=item.pollid})%>
            </td>
            <td>
                <%= Html.Encode(item.pollname) %>
            </td>
              <td>
                <%= Html.Encode(String.Format("{0:g}", item.createdAt))%>
            </td>
        </tr>
    
    <% } %>

    </table>
<% } %>
    <p>
        <%= Html.ActionLink("Back to Menu", "../Home/Home") %>
    </p>
    

</asp:Content>

