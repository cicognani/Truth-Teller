window.user= ""
chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
  if (request.context == "token" ){
    window.token = request.token;
    window.email = request.email;
  }
  else if (request.context == "url") {
    var form = new FormData();
    form.append( 'url', request.url );

    var settings = {
      "url": "https://t2spekno-62a66c37.northeurope.cloudapp.azure.com/api/Links/1",
      "method": "POST",
      "timeout": 0,
      "processData": false,
      "mimeType": "multipart/form-data",
      "contentType": false,
      "data": form,
      "dataType": "json"
    };
    $.ajax(settings).done(function (response) {
      console.log(response["state"]);
      sendResponse(response["state"]);
    });
    /*$.getJSON("https://t2spekno-62a66c37.northeurope.cloudapp.azure.com/api/Links/1",
                {url:request.url},
                function(data) {
                  console.log(data);
                  sendResponse(data["state"]);
                });*/
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
