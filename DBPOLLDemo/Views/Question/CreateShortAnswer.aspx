<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Models.QUESTION>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Short Answer Question
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
<style type="text/css">
  html { height: 100% }
  body { height: 100%; margin: 0; padding: 0 }
  #map_canvas { height: 100% }
</style>
<script type="text/javascript"
    src="http://maps.googleapis.com/maps/api/js?sensor=false">
</script>
<script type="text/javascript">
    function initialize() {
        var latlng = new google.maps.LatLng(-34.397, 150.644);
        var myOptions = {
            zoom: 8,
            center: latlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("map_canvas"),
        myOptions);
    }

</script>

<script runat="server" language="C#">
void Page_Load(object source, EventArgs e){
        HtmlGenericControl body = (HtmlGenericControl)
        Page.Master.FindControl("MyBody");
        body.Attributes.Add("onload", "initialize()");

    } 
    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    <h2>Create Short Answer Question <%=ViewData["error1"]%></h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <%
        
        %>
    <%using (Html.BeginForm())
      {%>

      
    
        <fieldset>
            <legend>New Question</legend>
            <p>
                <label for="shortanswertype">Short Answer Type:</label>
                <select id="shortanswertype" name="shortanswertype">
                <option value="1">Numeric Responses Only</option>
                <option value="2">Alphanumeric Responses Only</option>
                </select>
                
                <%= Html.ValidationMessage("shortanswertype", "*")%>
                <%=Html.Hidden("pollid", ViewData["id"])%>
                
            </p>
            <p>
                <label for="num">Number of question in sequence:</label>
                <%= Html.TextBox("num") %>
            </p>
            <p style ="color: Red;"><%=ViewData["numerror"]%></p>
            
            <p>
                <label for="question">Question Text:</label>
                <%= Html.TextBox("question") %>
                <%= Html.ValidationMessage("question", "*") %>
            </p>
            <p style ="color: Red;"><%=ViewData["questionerror"]%></p>
            <p>
                <label for="chartstyle">Response Chart</label>
                <select id="chartstyle" name="chartstyle">
                <option value="1">Horizontal Bar Graph</option>
                <option value="2">Vertical Bar Graph</option>
                <option value="3">Line Graph</option>
                <option value="4">Pie Chart</option>
                </select>
                
                <%= Html.ValidationMessage("chartstyle", "*")%>
            </p>
            <p><%=ViewData["created"]%></p>

    <!--start of map modifications
                This is here just for testing. move to Poll views once created.   -->
    <div id="map_canvas" style="width:360px; height:200px"></div>
     <!--end of map modifications  -->

            <p style ="color: Red;"><%=ViewData["mastererror"]%></p>
            <p>
                <input type="submit" value="Create Question" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to Question List", "Index", new { id = ViewData["id"] }) %>
    </div>

</asp:Content>

