<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.userModel>>" %>

<%@ Import Namespace="DBPOLLDemo.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SystemUtilisationReport
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        System Utilisation Report</h2>
    <table class="table">
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
            <%--<th nowrap="nowrap" class="style10">
                Expiry Date
            </th>--%>
            <th nowrap="nowrap" class="style10">
                Status
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <%
            String utype;
            String expire;
        %>
        <tr>
            <td class="style2">
                <%
                      
                    switch (item.usertype)
                    {
                        case User_Type.POLL_ADMINISTRATOR:
                            utype = "Poll Administrator";
                            break;
                        case User_Type.POLL_CREATOR:
                            utype = "Poll Creator";
                            break;
                        case User_Type.POLL_MASTER:
                            utype = "Poll Master";
                            break;
                        case User_Type.POLL_USER:
                            utype = "Poll User (Web)";
                            break;
                        case User_Type.KEYPAD_USER:
                            utype = "Poll User (Keepad)";
                            break;
                        default:
                            utype = "undefined";
                            break;
                    }
                           
                %>
                <%= Html.Encode(utype)%>
            </td>
            <td nowrap="nowrap" class="style7">
                <%= Html.Encode(item.name) %>
            </td>
            <% if (item.createdby == null) { item.createdby = item.sysAdmin; } %>
            <td nowrap="nowrap" class="style9">
                <%= Html.Encode(item.createdby) %>
            </td>
            <td nowrap="nowrap" class="style5">
                <%= Html.Encode(String.Format("{0:g}", item.createdat))%>
            </td>
           <%--  <td nowrap="nowrap" class="style5">
                <%= Html.Encode(String.Format("{0:g}", item.Expires_At))%>
            </td>--%>
            <td nowrap="nowrap" class="style10">
                <%    
                    expire = "";   
                    if (utype == "Poll Administrator")
                    {
                        int total = (item.Expires_At - DateTime.Now).Days;



                        if ((total >= 0) && (total <= 30) &&
                                    (utype == "Poll Administrator"))
                        { expire = "This Account is about to expire in " + total + " day(s)"; }
                        else if ((total < 0) && (utype == "Poll Administrator")) { expire = "Expired"; }
                        else { expire = ""; }
                    }
                    //Graces broken code
                    //int total = (item.createdat - DateTime.Now).Days + 365;
                    //if ((total >= 0) && (total <= 30) &&
                    //        (utype == "Poll Administrator"))
                    //{ expire = "This Account is about to expire in " + total + " days"; }
                    //else if ((total < 0) && (utype == "Poll Administrator")) { expire = "Expired"; }
                    //else { expire = ""; }
                %>
                <%= Html.Encode(expire)%>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style2
        {
            width: 180px;
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
        td.style10
        {
            width: 180px;
        }
        .table
        {
            width: 100px;
        }
        td.style10
        {
            width: 180px;
            color: Red;
        }
    </style>
</asp:Content>
