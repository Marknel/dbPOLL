<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DBPOLLDemo.Models.SESSION>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Session
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
<style type="text/css">
  html { height: 100% }
  body { height: 100%; margin: 0; padding: 0 }
  #map_canvas { height: 100% }
    .style1
    {
        font-size: x-small;
    }
</style>
<script type="text/javascript"
    src="http://maps.googleapis.com/maps/api/js?sensor=false">
</script>
<script type="text/javascript">
    var map;
    var marker = null;
    function initialize() {
        // Map is centered over brisbane
        var latlng = new google.maps.LatLng(-27.28, 153.01);
        var myOptions = {
            zoom: 9,
            center: latlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        map = new google.maps.Map(document.getElementById("map_canvas"),
        myOptions);

        google.maps.event.addListener(map, 'click', function (event) {
            placeMarker(event.latLng);
        });

    }

    //As there is only one point. Returns the Geogrpahical Location of the poll to a createpoll / update poll 
    function getPollLocation() {
        if (marker == null) {
            return null;
        } else { return marker.getPosition(); }
    }

    function getPollLat() {
        if (marker == null) {
            return 0;
        } else {
            return marker.getPosition().lat();
        }
    }

    function getPollLong() {
        if (marker == null) {
            return 0;
        } else {
            return marker.getPosition().lng();
        }
    }

    function setPollLocation() {
        document.getElementById('longitude').value = getPollLong();
        document.getElementById('latitude').value = getPollLat();
    }

    // Adds a marker or moves the markers position on the map.
    function placeMarker(location) {
        if (marker == null) {
            marker = new google.maps.Marker({
                position: location,
                map: map
            });
            document.getElementById('longitudeBox').value = getPollLong();
            document.getElementById('latitudeBox').value = getPollLat();
        } else {
            marker.setPosition(location);
            document.getElementById('longitudeBox').value = getPollLong();
            document.getElementById('latitudeBox').value = getPollLat();
        }
    }

</script>

<script runat="server" Language="C#" Culture="en-AU">
void Page_Load(object source, EventArgs e){
        HtmlGenericControl body = (HtmlGenericControl)
        Page.Master.FindControl("MyBody");
        body.Attributes.Add("onload", "initialize()");

    } 
    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create New Session for <%=ViewData["pollName"] %></h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm("CreateSession","Poll", FormMethod.Post)) {%>

        <fieldset>
            <legend>Session Data</legend>
            <p>
                <label for="SESSIONNAME">Session Name:</label>

                <%= Html.TextBox("name") %>

            </p>
            <p>
                <label for="SESSIONTIME">Session Time:</label>

                <%= Html.TextBox("time") %>
                <%--<%= ViewData["date1"] %>--%>
                <%= Html.ValidationMessage("date1")%>

            </p>

            <p style="width: 608px" class="style1"> Note: Valid longitude is between -180" and 180" </p>
            <p>
                <label for="Longitude">Session Longitude :</label>

                <%= Html.TextBox("longitudeBox") %>
                <%--<%= ViewData["longBox"] %>--%>
                <%= Html.ValidationMessage("longBox")%>

            </p>
            <p style="width: 608px" class="style1"> Note: Valid latitude is between -90" and 90" </p>
            <p>
                <label for="Latitude">Session Latitiude:</label>

                <%= Html.TextBox("latitudeBox") %>
               <%-- <%= ViewData["latBox"] %>--%>
               <%= Html.ValidationMessage("latBox")%>

            </p>
            <p>

                <%= Html.Hidden("pollID", ViewData["pollID"])%>
                     <label for="map_canvas">Select Session Location:</label>
                     <div id="map_canvas" style="width:520px; height:360px"></div>
                 <%=Html.Hidden("pollName", ViewData["pollName"] )%>
                 </p>
                 <input type="hidden" id="longitude" name="longitude" value="null" />
                 <input type="hidden" id="latitude" name="latitude" value="null" />
            <p>
                <input type="submit" value="Create Session" onclick="setPollLocation();"/>
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "../Poll/Index") %>
    </div>

</asp:Content>


