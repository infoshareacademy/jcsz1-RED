

    window.onload = function () {
        GetMap();
    };
    function GetMap() {
        var apiKey = "Aq1d61Jw4x4MYNoxuz2RwCqOWoBhSwj24oLJkqVBCdMrYVB7-21yCo9R9FtGWAmk";
        var map = new Microsoft.Maps.Map(document.getElementById("simpleMap"), {credentials: apiKey });
        map.setView({center: new Microsoft.Maps.Location(54.365406, 18.615999), zoom: 12 });
        var pinInfobox = new Microsoft.Maps.Infobox(new Microsoft.Maps.Location(0, 0), {visible: false });
    var infoboxLayer = new Microsoft.Maps.EntityCollection();
    infoboxLayer.push(pinInfobox);
        Microsoft.Maps.Events.addHandler(map, 'click', function (e) {
            if (e.targetType == "map") {
        map.entities.clear();
    var point = new Microsoft.Maps.Point(e.getX(), e.getY());
    latlng = e.target.tryPixelToLocation(point);
                var marker = new Microsoft.Maps.Pushpin(latlng, {visible: true });
    map.entities.push(marker);
    getLocationDetails(latlng.latitude, latlng.longitude)
}
});
}
    function getLocationDetails(lat, lng) {
        $.ajax({
            url: "http://dev.virtualearth.net/REST/v1/Locations/" + lat + "," + lng,
            dataType: "jsonp",
            data:
            {
                key: "Aq1d61Jw4x4MYNoxuz2RwCqOWoBhSwj24oLJkqVBCdMrYVB7-21yCo9R9FtGWAmk",
                includeEntityTypes: "Address",
                includeNeighborhood: "1",
                include: "ciso2"
            },
            jsonp: "jsonp",
            success: function (data) {
                var result = data.resourceSets[0];
                if (result) {
                    if (result.estimatedTotal > 0) {
                        document.getElementById('xlatitude').value = lat;
                        document.getElementById('ylongitude').value = lng;
                        document.getElementById('xlocation').value = result.resources[0].address.addressLine;
                    }
                    else {
                        alert("Lokalizacja niedostępna")
                    }
                }
            }
        });
    }