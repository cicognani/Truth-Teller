chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
  if (request.context == "token" ){
    window.token = request.token;
  }
})

chrome.browserAction.onClicked.addListener(function (tab) {
  chrome.tabs.create({url: 'popup.html'})
})
