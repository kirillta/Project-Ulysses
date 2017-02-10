"use strict";

var isMarkerRendered = false;
var platform;
var maptypes;
var map;
var ui;


function initMap() {
    sendRequest("GET", "hereapps", null, renderMap);
}


function addMapEvents() {
    map.addEventListener("tap", function (evt) {
        console.log(evt.type, evt.currentPointer.type);
    });

    new H.mapevents.Behavior(new H.mapevents.MapEvents(map));
}


function renderMap(credentials) {
    platform = new H.service.Platform({
        'app_id': credentials[0],
        'app_code': credentials[1]
    });

    maptypes = platform.createDefaultLayers();

    map = new H.Map(
    document.getElementById("MapContainer"),
    maptypes.normal.map,
    {
        zoom: 12,
        center: { lng: 37.625, lat: 55.75 }
    });

    ui = H.ui.UI.createDefault(map, maptypes, "ru-RU");

    addMapEvents();
}


function renderMapMarkers(rounds) {
    if (isMarkerRendered)
        return;

    let group = new H.map.Group();
    map.addObject(group);
    group.addEventListener('tap', function (evt) {
        let bubble = new H.ui.InfoBubble(evt.target.getPosition(), {
            content: evt.target.getData()
        });
        ui.addBubble(bubble);
    }, false);

    for (let round of rounds) {
        for (let pub of round.pubs) {
            let html = `<div><p>${pub.title}</p><p>${round.number}-й раунд</p></div>`;
            let marker = new H.map.Marker({ lat: pub.latitude, lng: pub.longitude }/*, { icon: icon }*/);
            marker.setData(html);
            group.addObject(marker);
        }
    }

    isMarkerRendered = true;
}


var renderCongratulations = (congratulations) => {
    let content = "";
    for (let congratulation of congratulations)
        content += `<p>«${congratulation}»</p>`;

    document.getElementById("CongratulationList").innerHTML = content;
};


function getCongratulations() {
    sendRequest("GET", "congratulations", null, renderCongratulations);
}


var getTimeFrame = (fullTimeFrame, isChosen) => {
    if (fullTimeFrame === "0001-01-01T00:00:00")
        return "TBD";
    if (fullTimeFrame === "1980-01-01T00:00:00")
        return "—";

    let start = fullTimeFrame.indexOf("T") + 1;
    let end = fullTimeFrame.lastIndexOf(":");
    let time = fullTimeFrame.substring(start, end);

    if (!isChosen)
        time = "~" + time;

    return time;
};


var getPubDescription = (pub) => {
    let status = "deactivated";
    let timeFrame = getTimeFrame(pub.estimateStartTime, pub.isChosen);
    let title = pub.title;
    if (pub.isChosen) {
        status = "activated";
        title += " ⭐";
    }

    return `<div class="pub-desc ${status} text-centered">
        <img src="images/logos/${pub.logoUrl}" alt="${title}"/>
        <div>
            <span class="title">${title}</span>
            <br />
            <span class="address">${pub.address}</span>
            <div class ="time-frame">${timeFrame}</div>
        </div>
    </div>`;
};


var getPubGroup = (round) => {
    let str = `<hr /><div id="Group${round.number}" class="pub-chart">`;

    for (let pub of round.pubs)
        str += getPubDescription(pub);

    str += "</div>";
    return str;
};


function getRounds() {
    sendRequest("GET", "rounds", null, renderPubChart);
}


var renderPubChart = (rounds) => {
    let content = "";
    for (let round of rounds)
        content += getPubGroup(round);

    document.getElementById("PubChart").innerHTML = content.substring(6);

    renderMapMarkers(rounds);
};


function sendCongratulation() {
    const textarea = document.getElementById("CongratulationText");
    const content = { content: textarea.value };
    sendRequest("POST", "congratulations", content);
    textarea.value = "";

    document.getElementById("CongratulationSubmit").value = "Написать ещё";
}



function sendRequest(method, controller, data, callback) {
    const url = `/api/${controller}`;

    const request = new XMLHttpRequest();
    request.open(method, url, true);

    request.contentType = "application/json";
    request.setRequestHeader("content-type", "application/json");
    request.setRequestHeader("cache-control", "no-cache");

    if (data !== null)
        request.send(JSON.stringify(data));
    else
        request.send();

    request.onreadystatechange = () => {
        if (request.readyState !== 4)
            return;

        document.body.style.cursor = "default";

        if (request.status >= 200 && request.status < 300) {
            if (callback !== undefined && callback !== null) {
                const result = JSON.parse(request.responseText);
                callback(result);
            }
        } else {
            console.log(`${request.status}: ${request.statusText}`);
        }
    };

    document.body.style.cursor = "wait";
}


function assignCongratulationEvent() {
    const button = document.getElementById("CongratulationSubmit");
    button.addEventListener("click", sendCongratulation);
}


document.addEventListener("DOMContentLoaded", ready);
function ready() {
    initMap();
    getRounds();
    setInterval(getRounds, 60 * 1000);
    getCongratulations();
    assignCongratulationEvent();
}