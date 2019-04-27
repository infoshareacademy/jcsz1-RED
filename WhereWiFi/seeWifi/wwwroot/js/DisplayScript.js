window.onload = function () {
    GetMap();
};
function GetMap() {
    var apiKey = "Aq1d61Jw4x4MYNoxuz2RwCqOWoBhSwj24oLJkqVBCdMrYVB7-21yCo9R9FtGWAmk";
    var map = new Microsoft.Maps.Map(document.getElementById("simpleMap"), { credentials: apiKey });
    map.setView({ center: new Microsoft.Maps.Location(54.365406, 18.615999), zoom: 12 });
}