<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLL.Models.userModel>>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SystemUtilisationReport
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>System Utilisation Report</h2>
    <table>
        <tr>
            <th nowrap="nowrap" class="style2">
                User Type
            </th>
           
            <th nowrap="nowrap" class="style7">
                Name
            </th>
            <th nowrap="nowrap" class="style9">
                Created By
            </th>
            <th nowrap="nowrap" class="style5">
                Created at
            </th>
           <th nowrap="nowrap" class="style10">
                Status
            </th>
            
        </tr>

   <% foreach (var item in Model) { %>
             <%
                  String type;
             %>
        <tr>
            
            <td class="style2"><%
                  //String type;
                  switch(item.usertype)
                  {
                      case 1:
                          type = "Poll Administrator";
                          break;
                      case 2:
                          type = "Poll Creator";
                          break;
                      case 3:
                          type = "Poll master";
                          break;
                      case 4:
                          type = "Poll user (web)";
                          break;
                      case 5:
                          type = "Poll User(keypad)";
                          break;
                      default:
                          type = "undefined";
                          break;
                   }
                           
             %>
                <%= Html.Encode(type)%>
            </td>
            
            <td nowrap="nowrap" class="style7">
                <%= Html.Encode(item.name) %>
            </td>
            <td nowrap="nowrap" class="style9">
                <%= Html.Encode(item.createdby) %>
            </td>
            <td nowrap="nowrap" class="style5">
                <%= Html.Encode(String.Format("{0:g}", item.createdat))%>
            </td>
            <td class="style10"><%
                  String expire;
                  if (((item.createdat - DateTime.Now).Days >= 0) && ((item.createdat - DateTime.Now).Days < 30) && (type == "Poll Administrator")) { expire = "This Account is about to expire in " + (item.createdat - DateTime.Now).Days +" days"; }
                  else { expire = ""; }
             %>
                <%= Html.Encode(expire)%>
            </td>
        </tr>
    
    <% } %>

    </table>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">

        .style2
        {
            width: 140px;
        }
        .style5
        {
            width: 164px;
        }
        .style7
        {
            width: 82px;
        }
        .style9
        {
            width: 87px;
        }
        .style10
        {
            width: 120px;
        }
    </style>
</asp:Content>

