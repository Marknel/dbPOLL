<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Controllers.TwoQuestionModels>" %>

<script runat="server">
 
    protected void Page_Load(object sender, EventArgs e)
    {

    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    StatisticalReport
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        List<string> pollnamecheck = new List<string>();
        List<string> qcheck = new List<string>();
        List<string> scheck = new List<string>();
        List<string> acheck = new List<string>();
        List<int> list = new List<int>();


        int[] sValues = new int[100];
        String[] sLists = new String[100];
        String[] aLists = new String[100];
        int sListsCounter = 0;
        int sValuesCounter = 0;
        int aListsCounter = 0; 
        
    %>
   
     <% foreach (var item in Model.data1)
       { %>
        
        <% if (!qcheck.Contains(item.question) && pollnamecheck == null || !qcheck.Contains(item.question) && !pollnamecheck.Contains(item.pollname))
        { %>
                        
            <br />
            </fieldset>
            <%
                scheck = new List<string>();
                sLists = new String[100];
                aLists = new String[100];
                sValues = new int[100];  
            %>

            <fieldset>
            <legend><%= Html.Encode(item.pollname)%></legend>
            <br />
            <%= Html.Encode("Question: " + item.question)%> 

            <% qcheck.Add(item.question); %>

            <br />
            <br />

            <table>
            
                <tr>
                    <th nowrap="nowrap" class="style5">
                        Answer Choices
                    </th>
                   
                    <% foreach (var item2 in Model.data2){ %>  
                        <% if (!scheck.Contains(item2.sessionname) && item2.pollname == item.pollname){ %>
                            <th nowrap="nowrap" class="style100">
                                <%= Html.Encode(item2.sessionname)%>
                                <% sLists[sListsCounter++] = item2.sessionname; %>
                                <% scheck.Add(item2.sessionname); %>
                            </th>
                        <%} %>
                    <% } %> 
                </tr>
                <% foreach (var item2 in Model.data1)
                {
                    if (item2.question == item.question && item2.pollid == item.pollid && !acheck.Contains(item2.answer))
                    { %>
                        <tr>
                            <td nowrap="nowrap" class="style5">
                            <%= Html.Encode(item2.answer)%>
                            <% 
                                acheck.Add(item2.answer);
                                aLists[aListsCounter++] = item2.answer;
                            %>
                            </td>         
                    
                            <%foreach (var item3 in Model.data2)
                             {
                                    foreach (String s in scheck)
                                    {
                                            if (item3.sessionname == s && item3.question == item2.question && item3.answer == item2.answer)
                                            { %>
                                                <td nowrap="nowrap" class="style100">
                                                    <%= Html.Encode(item3.totalparticipants)%>
                                                    <% sValues[sValuesCounter++] = item3.totalparticipants; %>
                                                </td>
                                            <%}%>
                                    <%} %>
                             <%} %>     

                        </tr>
                    <%} %>       
            <%} %>
            
            <%  
                pollnamecheck.Add(item.pollname); 
                qcheck.Add(item.question);
                list.Add(item.totalparticipants);


                String finalResult = "";
                int i = 0;
                while (i < sValuesCounter)
                {
                    finalResult += Convert.ToString(sValues[i]);
                    if (i + 1 < sValuesCounter)
                    {
                        finalResult += "/";
                    }

                    i++;
                }

                finalResult += ",";
                i = 0;

                while (sLists[i] != null)
                {
                    finalResult += sLists[i];
                    if (sLists[i + 1] != null)
                    {
                        finalResult += "/";
                    }
                    i++;
                }

                i = 0;

                finalResult += ",";

                while (aLists[i] != null)
                {
                    finalResult += aLists[i];
                    if (aLists[i + 1] != null)
                    {
                        finalResult += "/";
                    }
                    i++;
                }
            %>
            </table>
            <br />
            <br />
            <img src="<%= Url.Action("Chart", new {chartParameter = finalResult}) %>" alt="image" /> 
            <br />
            <br />
            <br />
            <br />
            <%--<img src="<%= Url.Action("Chart", new {sessionValues = sValues, sessionLists = sLists, answerLists = aLists}) %>" alt="image" /> --%>

            <%
                sListsCounter = 0;
                aListsCounter = 0;
                sValuesCounter = 0;
            %>

            
                
                
        <% }
           else if (!qcheck.Contains(item.question) && pollnamecheck != null || !qcheck.Contains(item.question) && pollnamecheck.Contains(item.pollname))
        { %>
 
            <%
                scheck = new List<string>();
                sLists = new String[100];
                aLists = new String[100];
                sValues = new int[100];   
            %>  

            <br />
<%--            <fieldset>
            <legend><%= Html.Encode(item.pollname)%></legend>--%>
            <%= Html.Encode("Question: " + item.question)%> 
            <% qcheck.Add(item.question); %>

            <br />
            <br />

            <table>
                <tr>
                    <th nowrap="nowrap" class="style5">
                        Answer Choices
                    </th>
                   
                    <% foreach (var item2 in Model.data2){ %>  
                        <% if (!scheck.Contains(item2.sessionname) && item2.pollname == item.pollname){ %>
                            <th nowrap="nowrap" class="style100">
                                <%= Html.Encode(item2.sessionname)%>
                                <% sLists[sListsCounter++] = item2.sessionname; %>
                                <% scheck.Add(item2.sessionname); %>
                            </th>
                        <%} %>
                    <% } %> 
                </tr> 

                <% foreach (var item2 in Model.data1)
                {
                    if (item2.question == item.question && item2.pollid == item.pollid && !acheck.Contains(item2.answer))
                    { %>
                        <tr>
                            <td nowrap="nowrap" class="style5">
                            <%= Html.Encode(item2.answer)%>
                            <% 
                                acheck.Add(item2.answer);
                                aLists[aListsCounter++] = item2.answer;
                            %>
                            </td>         
                    
                            <%foreach (var item3 in Model.data2)
                             {
                                    foreach (String s in scheck)
                                    {
                                            if (item3.sessionname == s && item3.question == item2.question && item3.answer == item2.answer)
                                            { %>
                                                <td nowrap="nowrap" class="style100">
                                                    <%= Html.Encode(item3.totalparticipants)%>
                                                    <% sValues[sValuesCounter++] = item3.totalparticipants; %>
                                                </td>
                                            <%}%>
                                    <%} %>
                             <%} %>     
                        </tr>
                    <%} %>       
            <%} %>
            <%  
                pollnamecheck.Add(item.pollname); 
                qcheck.Add(item.question);
                list.Add(item.totalparticipants);

                String finalResult = "";
                int i = 0;
                while (i < sValuesCounter)
                {
                    finalResult += Convert.ToString(sValues[i]);
                    if (i + 1 < sValuesCounter)
                    {
                        finalResult += "/";
                    }

                    i++;
                }

                finalResult += ",";
                i = 0;

                while (sLists[i] != null)
                {
                    finalResult += sLists[i];
                    if (sLists[i + 1] != null)
                    {
                        finalResult += "/";
                    }
                    i++;
                }

                i = 0;

                finalResult += ",";

                while (aLists[i] != null)
                {
                    finalResult += aLists[i];
                    if (aLists[i + 1] != null)
                    {
                        finalResult += "/";
                    }
                    i++;
                }
            %>
            </table>
            <br />
            <br />
            <img src="<%= Url.Action("Chart", new {chartParameter = finalResult}) %>" alt="image" /> 
            <br />
            <br />
            <br />
            <br />
           <%-- <img src="<%= Url.Action("Chart", new {sessionValues = sValues, sessionLists = sLists, answerLists = aLists}) %>" alt="image" /> --%>

            <%
                sListsCounter = 0;
                aListsCounter = 0;
                sValuesCounter = 0;
            %>
            
        <% }%>
       
            
            
    <%} %>

   <%-- DO NOT DELETE THIS <p>--%>
           <%--  <input name="submit" type="submit" id="generate" value="Generate excel file" />--%>
          <%--   <input name="submit" type="submit" id="submit" value="Save" />--%>
                
   <%-- </p>--%>

    </table>

    </fieldset>
    
    
    <p> &nbsp;</p>

    </asp:Content>



<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .special
        {
            
            font-weight: bolder;    
            text-decoration: underline;
        }
    </style>
</asp:Content>

