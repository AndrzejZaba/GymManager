"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub?userId=" + userId).build();

connection.on("sendToUser", (message) => {
    toastr.success(message);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
}).then(function () {
    connection.invoke("GetConnectionId").then(function (connectionId) {
        document.getElementById("signalRConnectionId").innerHTML = connectionId;
    })
})

