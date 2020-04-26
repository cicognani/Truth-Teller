window.user= ""
chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
  if (request.context == "token" ){
    window.token = request.token;
    window.email = request.email;
  }
  else if (request.context == "url") {
    $.getJSON("https://t2spekno-62a66c37.northeurope.cloudapp.azure.com/api/Links/1",
                {url:request.url},
                function(data) {
                  console.log(data);
                  sendResponse(data["state"]);
                });
  }
  else if (request.context == "user") {
    window.user = request.state;
  }
  else if (request.context == "userstate") {
    sendResponse(window.user); 
  }
  return true;
})

chrome.browserAction.onClicked.addListener(function (tab) {
  chrome.tabs.create({url: 'popup.html'})
})

/*chrome.tabs.sendMessage(tab.id, {content: "message"}, function(response) {
 	if(response) {
 		//We do something
 	}
});
$.getJSON("https://t2spekno-62a66c37.northeurope.cloudapp.azure.com/api/Links/1",
          {},
          function(data) {
            console.log(data);
          }
        );*/
