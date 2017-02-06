"use strict";

function sendRequest(method, controller, data) {
    var url = "/api/" + controller;

    var request = new XMLHttpRequest();
    request.open(method, url, true);

    request.contentType = "application/json";
    request.setRequestHeader("content-type", "application/json");
    request.setRequestHeader("cache-control", "no-cache");

    request.send(JSON.stringify(data));
}

function assignCongratulationEvent() {
    var button = document.getElementById("CongratulationSubmit");
    button.addEventListener("click", sendCongratulation);
}

function sendCongratulation() {
    document.body.style.cursor = "wait";

    var textarea = document.getElementById("CongratulationText");
    var content = { content: textarea.value };
    sendRequest("POST", "congratulations", content);
    textarea.value = "";

    document.body.style.cursor = "default";
}

document.addEventListener("DOMContentLoaded", ready);
function ready() {
    assignCongratulationEvent();
}

