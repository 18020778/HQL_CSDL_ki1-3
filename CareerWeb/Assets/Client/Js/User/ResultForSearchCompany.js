$(document).ready(function () {
    function calcRate(r) {
        const f = ~~r,//Tương tự Math.floor(r)
            id = 'star' + f + (r % f ? 'half' : '')
        id && (document.getElementById(id).checked = !0)
    }
    function initMap() {
        var url = { lat: parseFloat($(".main-content").attr("xPoint")), lng: parseFloat($(".main-content").attr("yPoint"))};
        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 18,
            center: url
        });
        var contentString = $(".main-content").attr("addressName");
        var infowindow = new google.maps.InfoWindow({
            content: contentString
        });
        var marker = new google.maps.Marker({
            position: url,
            map: map,
            title: "Địa chỉ"
        })
        marker.addListener('click', function () {
            infowindow.open(map, marker);
        });
    }
    google.maps.event.addDomListener(window, 'load', initMap);
})
