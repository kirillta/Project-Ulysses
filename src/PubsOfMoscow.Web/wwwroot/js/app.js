"use strict";

var getPubDescription = function getPubDescription(pub) {
    var status = "deactivated";
    var title = pub.title;
    if (pub.isChosen) {
        status = "activated";
        title += " ⭐";
    }

    return "<div class=\"pub-desc " + status + " text-centered\">\n        <span class=\"title\">" + title + "</span>\n        <br />\n        <span class=\"address\">" + pub.address + "</span>\n        <div class =\"time-frame\">" + pub.estimateStartTime + "</div>\n    </div>";
};

var getPubGroup = function getPubGroup(round) {
    var str = "<hr /><div id=\"Group" + round.number + "\" class=\"pub-chart\">";

    var _iteratorNormalCompletion = true;
    var _didIteratorError = false;
    var _iteratorError = undefined;

    try {
        for (var _iterator = round.pubs[Symbol.iterator](), _step; !(_iteratorNormalCompletion = (_step = _iterator.next()).done); _iteratorNormalCompletion = true) {
            var pub = _step.value;

            str += getPubDescription(pub);
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

    str += "</div>";
    return str;
};

function getRounds() {
    sendRequest("GET", "rounds", null, renderPubChart);
}

var renderPubChart = function renderPubChart(rounds) {
    var chart = document.getElementById("PubChart");

    var content = "";
    var _iteratorNormalCompletion2 = true;
    var _didIteratorError2 = false;
    var _iteratorError2 = undefined;

    try {
        for (var _iterator2 = rounds[Symbol.iterator](), _step2; !(_iteratorNormalCompletion2 = (_step2 = _iterator2.next()).done); _iteratorNormalCompletion2 = true) {
            var round = _step2.value;

            content += getPubGroup(round);
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

    chart.innerHTML = content.substring(6);
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
    getRounds();
    assignCongratulationEvent();
}

