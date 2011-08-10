﻿<%@ Page Title="" Language="C#"%>

<!--basic map. need to determine how to load in a partial view. -->

<!DOCTYPE html>
<html>
<head>

<title>hello</title>
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
</head>
<body onload="initialize()">

hello world
  <div id="map_canvas" style="width:100%; height:100%"></div>
</body>
</html>