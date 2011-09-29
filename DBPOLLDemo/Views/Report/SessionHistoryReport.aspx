<%@ Page Title="" Language="C#" Culture="en-AU" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DBPOLLDemo.Models.pollModel>>" %>


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
              function initialize() {
                  var latlng = new google.maps.LatLng(39, 35);
                  var myOptions = {
                      zoom: 1,
                      center: latlng,
                      mapTypeId: google.maps.MapTypeId.ROADMAP
                  };
                  
     
                map = new google.maps.Map(document.getElementById("google_map"),
                myOptions);

                marker = new google.maps.Marker({
                    position: new google.maps.LatLng(-34.933333, 138.6),
                    map: map
                });
                marker = new google.maps.Marker({
                    position: new google.maps.LatLng(-12.466667, 130.833333),
                    map: map
                });
                marker = new google.maps.Marker({
                    position: new google.maps.LatLng(153.016667, -27.5),
                    map: map
                });
                marker = new google.maps.Marker({
                    position: new google.maps.LatLng(-31.933333, 115.833333),
                    map: map
                }); 
                marker = new google.maps.Marker({
                    position: new google.maps.LatLng( -33.8830555556,151.216666667),
                    map: map
                });
//                marker = new google.maps.Marker({
//                    position: new google.maps.LatLng( -27.5, 153.016667),
//                    map: map
//                }); marker = new google.maps.Marker({
//                    position: new google.maps.LatLng(-34.933333, 138.6),
//                    map: map
//                });
//                marker = new google.maps.Marker({
//                    position: new google.maps.LatLng(-12.466667, 130.833333 ),
//                    map: map
//                });
//                marker = new google.maps.Marker({
//                    position: new google.maps.LatLng(-31.933333, 115.833333),
//                    map: map
//                });
//                marker = new google.maps.Marker({
//                    position: new google.maps.LatLng(151.216666667, -33.8830555556),
//                    map: map
//                });
//                marker = new google.maps.Marker({
//                    position: new google.maps.LatLng(-33.895820604476604, 151.14644409179687),
//                    map: map
//                });
//                marker = new google.maps.Marker({
//                    position: new google.maps.LatLng(152.70592064619063, -27.627904686799557),
//                    map: map
//                });
                 ;
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

   <% String pnamecheck = "" ;%>
   <% foreach (var item in Model) { %>
    
        <tr>
            <td nowrap="nowrap">
            <% if (pnamecheck != item.pollname){ %>
                    <%= Html.Encode(item.pollname)%>
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
            
        <% Session["a1"] = item.latitude;
           Session["a2"] = item.longitude;
        %>
        

        
        </tr>
    
    <% } %>

    </table>
    <br />
    <br />
    <br />
    <br />

    
    <div id="google_map" style="width:360px; height:200px"></div>
     </asp:Content>
