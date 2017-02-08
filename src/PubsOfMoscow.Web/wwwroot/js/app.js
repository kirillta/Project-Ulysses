"use strict";

function sendRequest(method, controller, data) {
    document.body.style.cursor = "wait";
    var url = "/api/" + controller;

    var request = new XMLHttpRequest();
    request.open(method, url, true);

    request.contentType = "application/json";
    request.setRequestHeader("content-type", "application/json");
    request.setRequestHeader("cache-control", "no-cache");

    request.send(JSON.stringify(data));
    document.body.style.cursor = "default";
}

function assignCongratulationEvent() {
    var button = document.getElementById("CongratulationSubmit");
    button.addEventListener("click", sendCongratulation);
}

function sendCongratulation() {
    var textarea = document.getElementById("CongratulationText");
    var content = { content: textarea.value };
    sendRequest("POST", "congratulations", content);
    textarea.value = "";

    document.getElementById("CongratulationSubmit").value = "Написать ещё";
}

document.addEventListener("DOMContentLoaded", ready);
function ready() {
    assignCongratulationEvent();
}

