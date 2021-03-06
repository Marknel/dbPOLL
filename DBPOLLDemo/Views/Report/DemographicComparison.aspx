﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Controllers.TwoQuestionModels>" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	DemographicComparison
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <br />
    <h3>&nbsp;&nbsp;&nbsp;&nbsp; Demographic Comparison Report</h3>

    <% using (Html.BeginForm()) {%>
        <fieldset>
            <legend>Search:</legend>
                <p style="width: 467px" class="style2"> Enter demographic group below to filter your report. E.g: Female </p>
                <p style="width: 467px" class="style2"> Note: Search is case sensitive </p>
                <p>
                    <label for="demographic">Enter demographic group</label> 
                    <%= Html.TextBox("demographic")%><br /> 
                </p>

                
                <p>
                    <label for="graphType">Graph Type:</label>
                    <select id="graphType" name="graphType">
                    <option value="Bar">Bar</option>
                    <option value="Column">Column</option>
                    </select>
                </p>

                <p>
                    <label for="includeOrExclude">Options:</label>
                    <select id="includeOrExclude" name="includeOrExclude">
                    <option value="Include">Display data include</option>
                    <option value="Exclude">Display data exclude</option>
                    </select>
                </p>
               
                <div>
               
                    <input type="submit" value="Search" />
                    
                </div>
        </fieldset>

        <% 
            List<string> pollnamecheck = new List<string>();
            List<string> qcheck = new List<string>();
            List<string> scheck = new List<string>();
            List<string> acheck = new List<string>();
            List<int> list = new List<int>();
            Session["test"] = "";

            int[] sValues = new int[100];
            String[] sLists = new String[100];
            String[] aLists = new String[100];
            int sListsCounter = 0;
            int sValuesCounter = 0;
            int aListsCounter = 0; 
        %>
   
        <%=ViewData["error"] %>
        <% 
           if (Model != null){%>
            <% ViewData["error"] = ""; %>
            
              
            <%foreach (var item in Model.data1)
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
                                <%List<String> lastCheck = new List<string>(); %>
                                <%foreach (var item3 in Model.data2)
                                 {
                                        foreach (String s in scheck)
                                        {
                                                if (item3.sessionname == s && item3.question == item2.question && item3.answer == item2.answer
                                                    && !lastCheck.Contains(item3.answer + item3.sessionname + item3.question
                                                    ))
                                                { %>
                                                    <td nowrap="nowrap" class="style100">
                                                        <%= Html.Encode(item3.totalparticipants)%>
                                                        <% lastCheck.Add(item3.answer + item3.sessionname + item3.question); %>
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
                    <br />
                    <br />
                    <br />
                    <hr style="width: 765px; text-align: left; margin-left: 0px;" />
                <br />
                <br />
                <br />

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
                                <%List<String> lastCheck = new List<string>(); %>
                                <%foreach (var item3 in Model.data2)
                                 {
                                        foreach (String s in scheck)
                                        {
                                                if (item3.sessionname == s && item3.question == item2.question && item3.answer == item2.answer
                                                     && !lastCheck.Contains(item3.answer + item3.sessionname + item3.question
                                                    ))
                                                
                                                { %>
                                                    <td nowrap="nowrap" class="style100">
                                                        <%= Html.Encode(item3.totalparticipants)%>
                                                        <% lastCheck.Add(item3.answer + item3.sessionname + item3.question); %>
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
                        while (i<sValuesCounter)
                        {
                            finalResult+=Convert.ToString(sValues[i]);
                            if (i+1<sValuesCounter){
                                finalResult += "/";
                            }
                        
                            i++;
                        }

                        finalResult += ",";
                        i = 0;

                        while (sLists[i] != null)
                        {
                            finalResult += sLists[i];
                            if (sLists[i+1] != null)
                            {
                                finalResult += "/";
                            }
                            i++;
                        }

                        i = 0;

                        finalResult += ",";

                        while (aLists[i] != null)
                        {
                            finalResult +=aLists[i];
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
                    <br />
                    <br />
                    <br />
                    <hr style="width: 765px; text-align: left; margin-left: 0px;" />
                    <br />
                   <%-- <img src="<%= Url.Action("Chart", new {sessionValues = sValues, sessionLists = sLists, answerLists = aLists}) %>" alt="image" /> --%>

                    <%
                        sListsCounter = 0;
                        aListsCounter = 0;
                        sValuesCounter = 0;
                    %>
            
                    <% }%>
       
                <% } %>               
            <% } %>


    <% } %>
    </fieldset></asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: small;
        }
        .style2
        {
            font-size: x-small;
        }
    </style>
</asp:Content>
