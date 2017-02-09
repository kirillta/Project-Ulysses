"use strict";


var getPubDescription = (pub) => {
    let status = "deactivated";
    let title = pub.title;
    if (pub.isChosen) {
        status = "activated";
        title += " ⭐";
    }

    return `<div class="pub-desc ${status} text-centered">
        <span class="title">${title}</span>
        <br />
        <span class="address">${pub.address}</span>
        <div class ="time-frame">${pub.estimateStartTime}</div>
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
    const chart = document.getElementById("PubChart");

    let content = "";
    for (let round of rounds)
        content += getPubGroup(round);

    chart.innerHTML = content.substring(6);
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
    getRounds();
    assignCongratulationEvent();
}