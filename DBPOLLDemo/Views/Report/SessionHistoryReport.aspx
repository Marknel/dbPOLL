<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.pollModel>>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SessionHistoryReport</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

<meta name="viewport" content="initial-scale=1.0, user-scalable=yes" />
<style type="text/css">
  html { height: 100% }
  body { height: 100%; margin: 0; padding: 0 }
  #map_canvas { height: 100% }
</style>

<script type="text/javascript"
    src="http://maps.googleapis.com/maps/api/js?sensor=false">
</script>
<script type="text/javascript">
    var map;
    var marker = null;
    function initialize() {
        var latlng = new google.maps.LatLng(39, 35);
        var myOptions = {
            zoom: 1,
            center: latlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        map = new google.maps.Map(document.getElementById("google_map"),
        myOptions);

//        google.maps.event.addListener(map, 'click', function (event) {
//            placeMarker(event.latLng);
//        });

    }

    //As there is only one point. Returns the Geogrpahical Location of the poll to a createpoll / update poll 
//    function getPollLocation() {
//        if (marker = null) {
//            return null;
//        } else { return marker.getPosition; }
//    }

    // Adds a marker or moves the markers position on the map.
//    function placeMarker(location) {
//        if (marker == null) {
//            marker = new google.maps.Marker({
//                position: location,
//                map: map
//            });
//        } else {
//            marker.setPosition(location);
//        }
//    }

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

    <h2>Session History Report</h2>

    <table>
        <tr>
            <th nowrap="nowrap">
                Poll Name
            </th>
           
            <th nowrap="nowrap">
                Date Poll Created
            </th>
            <th nowrap="nowrap">
                Poll Master Assigned
            </th>
            <th nowrap="nowrap">
                Poll Creator
            </th>
            <th nowrap="nowrap">
                Total Number of Participants
            </th>
            
            
        </tr>

   <% foreach (var item in Model) { %>
    
        <tr>
            
            <td nowrap="nowrap">
                <%= Html.Encode(item.pollname)%>
            </td>
         
            <td nowrap="nowrap">
                <%= Html.Encode(String.Format("{0:g}", item.createdAt))%>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.createdmaster) %>
            </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.createdcreator1) %>
            </td>
            
            <td nowrap="nowrap">
                <%= Html.Encode(item.total) %>
            </td>
            
            
        </tr>
    
    <% } %>

    </table>
    <br />
    <br />
    <br />
    <br />
    <div id="google_map" style="width:360px; height:200px"></div>
     </asp:Content>
