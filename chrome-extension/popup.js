document.addEventListener('DOMContentLoaded', function () {

  const bg = chrome.extension.getBackgroundPage()

  if !(bg.auth ){
    $( "#auth" ).click(function() {
      console.log( "qui");
      chrome.identity.getAuthToken({ 'interactive': true },
          function(token) {
            console.log( token);
            chrome.runtime.sendMessage({
              context: "auth",
              token: token
            })
          }
        );
    });
  }
  else {
    $( "#auth" ).click(function() {console.log(bg.auth);});
  }
}, false)
