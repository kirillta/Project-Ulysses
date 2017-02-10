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

    map = new H.Map(document.getElementById("MapContainer"), maptypes.normal.map, {
        zoom: 12,
        center: { lng: 37.625, lat: 55.75 }
    });

    ui = H.ui.UI.createDefault(map, maptypes, "ru-RU");

    addMapEvents();
}

function renderMapMarkers(rounds) {
    if (isMarkerRendered) return;

    var group = new H.map.Group();
    map.addObject(group);
    group.addEventListener('tap', function (evt) {
        var bubble = new H.ui.InfoBubble(evt.target.getPosition(), {
            content: evt.target.getData()
        });
        ui.addBubble(bubble);
    }, false);

    var _iteratorNormalCompletion = true;
    var _didIteratorError = false;
    var _iteratorError = undefined;

    try {
        for (var _iterator = rounds[Symbol.iterator](), _step; !(_iteratorNormalCompletion = (_step = _iterator.next()).done); _iteratorNormalCompletion = true) {
            var round = _step.value;
            var _iteratorNormalCompletion2 = true;
            var _didIteratorError2 = false;
            var _iteratorError2 = undefined;

            try {
                for (var _iterator2 = round.pubs[Symbol.iterator](), _step2; !(_iteratorNormalCompletion2 = (_step2 = _iterator2.next()).done); _iteratorNormalCompletion2 = true) {
                    var pub = _step2.value;

                    var html = "<div><p>" + pub.title + "</p><p>" + round.number + "-й раунд</p></div>";
                    var marker = new H.map.Marker({ lat: pub.latitude, lng: pub.longitude } /*, { icon: icon }*/);
                    marker.setData(html);
                    group.addObject(marker);
                }
            } catch (err) {
                _didIteratorError2 = true;
                _iteratorError2 = err;
            } finally {
                try {
                    if (!_iteratorNormalCompletion2 && _iterator2["return"]) {
                        _iterator2["return"]();
                    }
                } finally {
                    if (_didIteratorError2) {
                        throw _iteratorError2;
                    }
                }
            }
        }
    } catch (err) {
        _didIteratorError = true;
        _iteratorError = err;
    } finally {
        try {
            if (!_iteratorNormalCompletion && _iterator["return"]) {
                _iterator["return"]();
            }
        } finally {
            if (_didIteratorError) {
                throw _iteratorError;
            }
        }
    }

    isMarkerRendered = true;
}

var renderCongratulations = function renderCongratulations(congratulations) {
    var content = "";
    var _iteratorNormalCompletion3 = true;
    var _didIteratorError3 = false;
    var _iteratorError3 = undefined;

    try {
        for (var _iterator3 = congratulations[Symbol.iterator](), _step3; !(_iteratorNormalCompletion3 = (_step3 = _iterator3.next()).done); _iteratorNormalCompletion3 = true) {
            var congratulation = _step3.value;

            content += "<p>«" + congratulation + "»</p>";
        }
    } catch (err) {
        _didIteratorError3 = true;
        _iteratorError3 = err;
    } finally {
        try {
            if (!_iteratorNormalCompletion3 && _iterator3["return"]) {
                _iterator3["return"]();
            }
        } finally {
            if (_didIteratorError3) {
                throw _iteratorError3;
            }
        }
    }

    document.getElementById("CongratulationList").innerHTML = content;
};

function getCongratulations() {
    sendRequest("GET", "congratulations", null, renderCongratulations);
}

var getTimeFrame = function getTimeFrame(fullTimeFrame, isChosen) {
    if (fullTimeFrame === "0001-01-01T00:00:00") return "TBD";
    if (fullTimeFrame === "1980-01-01T00:00:00") return "—";

    var start = fullTimeFrame.indexOf("T") + 1;
    var end = fullTimeFrame.lastIndexOf(":");
    var time = fullTimeFrame.substring(start, end);

    if (!isChosen) time = "~" + time;

    return time;
};

var getPubDescription = function getPubDescription(pub) {
    var status = "deactivated";
    var timeFrame = getTimeFrame(pub.estimateStartTime, pub.isChosen);
    var title = pub.title;
    if (pub.isChosen) {
        status = "activated";
        title += " ⭐";
    }

    return "<div class=\"pub-desc " + status + " text-centered\">\n        <img src=\"images/logos/" + pub.logoUrl + "\" alt=\"" + title + "\"/>\n        <div>\n            <span class=\"title\">" + title + "</span>\n            <br />\n            <span class=\"address\">" + pub.address + "</span>\n            <div class =\"time-frame\">" + timeFrame + "</div>\n        </div>\n    </div>";
};

var getPubGroup = function getPubGroup(round) {
    var str = "<hr /><div id=\"Group" + round.number + "\" class=\"pub-chart\">";

    var _iteratorNormalCompletion4 = true;
    var _didIteratorError4 = false;
    var _iteratorError4 = undefined;

    try {
        for (var _iterator4 = round.pubs[Symbol.iterator](), _step4; !(_iteratorNormalCompletion4 = (_step4 = _iterator4.next()).done); _iteratorNormalCompletion4 = true) {
            var pub = _step4.value;

            str += getPubDescription(pub);
        }
    } catch (err) {
        _didIteratorError4 = true;
        _iteratorError4 = err;
    } finally {
        try {
            if (!_iteratorNormalCompletion4 && _iterator4["return"]) {
                _iterator4["return"]();
            }
        } finally {
            if (_didIteratorError4) {
                throw _iteratorError4;
            }
        }
    }

    str += "</div>";
    return str;
};

function getRounds() {
    sendRequest("GET", "rounds", null, renderPubChart);
}

var renderPubChart = function renderPubChart(rounds) {
    var content = "";
    var _iteratorNormalCompletion5 = true;
    var _didIteratorError5 = false;
    var _iteratorError5 = undefined;

    try {
        for (var _iterator5 = rounds[Symbol.iterator](), _step5; !(_iteratorNormalCompletion5 = (_step5 = _iterator5.next()).done); _iteratorNormalCompletion5 = true) {
            var round = _step5.value;

            content += getPubGroup(round);
        }
    } catch (err) {
        _didIteratorError5 = true;
        _iteratorError5 = err;
    } finally {
        try {
            if (!_iteratorNormalCompletion5 && _iterator5["return"]) {
                _iterator5["return"]();
            }
        } finally {
            if (_didIteratorError5) {
                throw _iteratorError5;
            }
        }
    }

    document.getElementById("PubChart").innerHTML = content.substring(6);

    renderMapMarkers(rounds);
};

function sendCongratulation() {
    var textarea = document.getElementById("CongratulationText");
    var content = { content: textarea.value };
    sendRequest("POST", "congratulations", content);
    textarea.value = "";

    document.getElementById("CongratulationSubmit").value = "Написать ещё";
}

function sendRequest(method, controller, data, callback) {
    var url = "/api/" + controller;

    var request = new XMLHttpRequest();
    request.open(method, url, true);

    request.contentType = "application/json";
    request.setRequestHeader("content-type", "application/json");
    request.setRequestHeader("cache-control", "no-cache");

    if (data !== null) request.send(JSON.stringify(data));else request.send();

    request.onreadystatechange = function () {
        if (request.readyState !== 4) return;

        document.body.style.cursor = "default";

        if (request.status >= 200 && request.status < 300) {
            if (callback !== undefined && callback !== null) {
                var result = JSON.parse(request.responseText);
                callback(result);
            }
        } else {
            console.log(request.status + ": " + request.statusText);
        }
    };

    document.body.style.cursor = "wait";
}

function assignCongratulationEvent() {
    var button = document.getElementById("CongratulationSubmit");
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

