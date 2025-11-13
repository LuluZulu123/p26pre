// http://www.websocket.org/echo.html
const button = document.querySelector("button");
const output = document.querySelector("#output");
const textarea = document.querySelector("textarea");
const wsUri = "ws://localhost:5000/o/chat";
const websocket = new WebSocket(wsUri);

button.addEventListener("click", onClickButton);

websocket.onopen = (e) => {
  writeToScreen("CONNECTED");
  doSend("auto c");
};

websocket.onclose = (e) => {
  writeToScreen("DISCONNECTED");
};

websocket.onmessage = (e) => {
  writeToScreen(`<span>RESPONSE: ${e.data}</span>`);
};

websocket.onerror = (e) => {
  writeToScreen(`<span class="error">ERROR:</span> ${e.data}`);
};

function doSend(message) {
  writeToScreen(`SENT: ${message}`);
  websocket.send(message);
}

function writeToScreen(message) {
  output.insertAdjacentHTML("afterbegin", `<p>${message}</p>`);
}

function onClickButton() {
  const text = textarea.value;

  text && doSend(text);
  textarea.value = "";
  textarea.focus();
}