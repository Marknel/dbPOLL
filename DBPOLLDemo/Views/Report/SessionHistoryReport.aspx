<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.pollModel>>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SessionHistoryReport
 
</asp:Content>


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


<script runat="server" Language="C#" Culture="en-AU">
void Page_Load(object source, EventArgs e){
        HtmlGenericControl body = (HtmlGenericControl)
        Page.Master.FindControl("MyBody");
        
        body.Attributes.Add("onload", "initialize()");
    } 
    </script>

  <script type="text/javascript">
      var map;
      var marker = null;

      function createMarkerClickHandler(marker, text, link) {
          return function () {
              marker.openInfoWindowHtml(
      '<h3>' + text + '</h3>' +
      '<p><a href="' + link + '">Wikipedia &raquo;</a></p>'
    );
              return false;
          };
      }


      function createMarker(pointData) {
          var latlng = new GLatLng(pointData.latitude, pointData.longitude);

          var icon = new GIcon();
          icon.image = 'red_marker.png';
          icon.iconSize = new GSize(32, 32);
          icon.iconAnchor = new GPoint(16, 16);
          icon.infoWindowAnchor = new GPoint(25, 7);

          opts = {
              "icon": icon,
              "clickable": true,
              "labelText": pointData.abbr,
              "labelOffset": new GSize(-16, -16)
          };

          var marker = new LabeledMarker(latlng, opts);
          var handler = createMarkerClickHandler(marker, pointData.name, pointData.wp);

          GEvent.addListener(marker, "click", handler);

          var listItem = document.createElement('li');
          listItem.innerHTML = '<div class="label">' + pointData.abbr + '</div><a href="' + pointData.wp + '">' + pointData.name + '</a>';
          listItem.getElementsByTagName('a')[0].onclick = handler;

          document.getElementById('sidebar-list').appendChild(listItem);

          return marker;
      }

      function initialize() {

          var latlng = new google.maps.LatLng(39, 35);
          var myOptions = {
              zoom: 2,
              center: latlng,
              mapTypeId: google.maps.MapTypeId.ROADMAP
          };

          var infowindow = new google.maps.InfoWindow();

          map = new google.maps.Map(document.getElementById("google_map"),
                myOptions);

          var i = 0;
          for (i = 0; i < array.length; i++) {
              marker = new google.maps.Marker({
                  position: new google.maps.LatLng(array2[i], array[i]),
                  map: map
              });

              google.maps.event.addListener(marker, 'click', (function (marker, i) {
                  return function () {
                      var result = "<p>" + pollinfo[i] + "<br/> " + sessionInfo[i] + "</p>";
                      infowindow.setContent(result);
                      infowindow.open(map, marker);
                  }
              })(marker, i));
          }
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
                Session Name
            </th>
            <th nowrap="nowrap">
                Date Poll Created
            </th>
            <th nowrap="nowrap">
                Poll Master Assigned
            </th>
            <th nowrap="nowrap">
                Poll Creator
                Assigned</th>
            <th nowrap="nowrap">
                Total Number of Participants
            </th>

            
            
        </tr>
     <script type="text/javascript">
         var array = new Array();
         var array2 = new Array();
         var sessionInfo = new Array();
         var pollinfo = new Array();
         var i = 0;
         var s = "";
     </script>
   <% String pnamecheck = "" ;
      int counter = 0;
   %>
   <% foreach (var item in Model) { %>
  
        <script type="text/javascript">
        
            longitude = <%=item.longitude %>;
            lat = <%=item.latitude %>;

            p = 'Poll: ' + '<%=item.pollname %>';
            s = 'Session: ' + '<%=item.sessionname %>';
            
            array[i] = longitude;
            array2[i] = lat;
            sessionInfo[i] = s;
            pollinfo[i] = p;

            i++;


        </script>   
        <tr>
            <td nowrap="nowrap">
            <% if (pnamecheck != item.pollname){ %>
                    <%= Html.Encode(item.pollname)%>
                    <%  %>
                <% pnamecheck = item.pollname; %>
            <%} %>
             </td>
            <td nowrap="nowrap">
                <%= Html.Encode(item.sessionname)%>
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
            
<%--        <% Session["a1"] = item.latitude;
           Session["a2"] = item.longitude;
           counter++;
        %>--%>
        

        
        </tr>
    
    <% } %>

    </table>
    <br />
    <br />
    <br />
    <br />

    
    <div id="google_map" style="width:1000px; height:560px"></div>
     </asp:Content>
