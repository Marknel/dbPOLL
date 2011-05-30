<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.questionModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	questionDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Questions for Poll: <%= Html.Encode(ViewData["name"]) %></h2>
        <% using (Html.BeginForm("Edit","Edit", FormMethod.Post)) {%>
        <fieldset>
            <legend>Search by Date and Time (mm/dd/yyyy (mm/dd/yyyy [hh:mm]))</legend>
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
                </div
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
            <th nowrap="nowrap">
                Question Number
            </th>
            <th nowrap="nowrap">
                Question
            </th>
            <th nowrap="nowrap">
                Question Type
            </th>
            <th nowrap="nowrap">
                Date Question Created
            </th>
            
            
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td nowrap="nowrap">
                <%= Html.Encode(item.QuestionNumber) %>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.Question) %>
            </td>
            <td nowrap="nowrap">
           <%  
                string test;
                switch(item.QuestionType)       
                  {         
                     case 1:   
                        test = "Short Answer: Numeric Responses Only";
                        break;                  
                     case 2:            
                        test = "Short Answer: Alphanumeric Responses Only";
                        break;           
                     case 3:            
                        test = "Multiple Choice: Standard";
                        break; 
                     case 4:            
                        test = "Multiple Choice: Demographic";
                        break;   
                     case 5:            
                        test = "Multiple Choice: Comparative";
                        break;  
                     case 6:            
                        test = "Multiple Choice: Ranking";
                        break;         
                     default:            
                        test ="";         
                        break;      
                   }
           %>
                 <%=Html.Encode(test)%>
           
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(String.Format("{0:g}", item.QuestionCreated)) %>
            </td>
            
            
        </tr>
    
    <% } %>

    </table>
<% } %>
   <p>
        <%= Html.ActionLink("Back to Menu", "../Home/Home") %>
    </p>
    

</asp:Content>

