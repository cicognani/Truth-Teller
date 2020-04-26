document.addEventListener('DOMContentLoaded', function () {
  var manifest = chrome.runtime.getManifest();

  var clientId = encodeURIComponent(manifest.oauth2.client_id);
  var scopes = encodeURIComponent(manifest.oauth2.scopes.join(' '));
  var redirectUri = encodeURIComponent(chrome.identity.getRedirectURL("oauth2"));

  var url = 'https://accounts.google.com/o/oauth2/auth' +
            '?client_id=' + clientId +
            '&response_type=id_token' +
            '&access_type=offline' +
            '&redirect_uri=' + redirectUri +
            '&scope=' + scopes;
  console.log(url)

  const bg = chrome.extension.getBackgroundPage()

  if (bg.token ){
    $( "#auth" ).click(function() {console.log(bg.token);});
  }
  else {

    $( "#auth" ).click(function() {
      console.log( "qui");
      chrome.identity.launchWebAuthFlow(
          {
              'url': url,
              'interactive':true
          },
          function(redirectedTo) {
              if (chrome.runtime.lastError) {
                  // Example: Authorization page could not be loaded.
                  console.log(chrome.runtime.lastError.message);
              }
              else {
                  var response = redirectedTo.split('#', 2)[1];
                  chrome.identity.getProfileUserInfo(function(userinfo){
                    console.log("userinfo",userinfo);
                    var email=userinfo.email;
                    uniqueId=userinfo.id;
                  });
                  chrome.runtime.sendMessage({
                    context: "auth",
                    token: response,
                    email: email
                  })
              }
          }
      );
      chrome.identity.getAuthToken({ 'interactive': true },
          function(token) {
            console.log( token);
            chrome.identity.getProfileUserInfo()
            chrome.runtime.sendMessage({
              context: "auth",
              token: token,

            })
          }
        );
    });
  };
}, false)
