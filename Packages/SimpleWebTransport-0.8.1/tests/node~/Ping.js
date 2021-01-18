const WebSocket = require("websocket").w3cwebsocket;

const RunMany = require("./RunMany").RunMany;

RunMany((onExit, log, error) => {
    // Create webSocket connection.
    const webSocket = new WebSocket("ws://localhost:7776/");
    webSocket.binaryType = 'arraybuffer';

    webSocket.addEventListener('error', function (event) {
        error(`Socket Error ${event}`);
    });

    const pingInterval = 1000;
    let intervalHandle;
    // Connection opened
    webSocket.addEventListener('open', function (event) {
        intervalHandle = setInterval(() => {
            var buffer = new ArrayBuffer(4);
            var view = new Uint8Array(buffer);
            for (let i = 0; i < view.length; i++) {
                view[i] = i + 10;
            }
            webSocket.send(buffer);
        }, pingInterval);
    });

    webSocket.addEventListener('close', function (event) {
        onExit();
        clearInterval(intervalHandle);
    });
});