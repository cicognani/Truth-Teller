$(document).ready(function() {$('head').append('<style> .tt-link-box { background: transparent; width: 55px; height: 27px; padding: 5px;  float: right; position: absolute; z-index: 100;'+
      "}</style>");
      //$('.tt-toggle').click(function() {$(this).parent().children('.tt-link-box').toggle('slow');console.log('qui') });
});
//const bg = chrome.extension.getBackgroundPage();
var state ;
var imgstr = "";
var imgup =  chrome.runtime.getURL("images/tup.png");
var imgdown =  chrome.runtime.getURL("images/tdown.png");
var updown = "";
chrome.runtime.sendMessage({context:"userstate"}, function(response) {
    if (response == 'logged')
    {
      updown = '<!--div class="tt-link-box"--><a class="tt-up"><img style="width:20px" src="'+imgup+'"></a><a class="tt-down" ><img style="width:20px" src="'+imgdown+'"></a></div>';
    }
    else {
      updown = '</div>';
    }
});
$( ".r" ).each(function( index ) {
  console.log($(this).children("a").attr("href"));
  var result = this;

  chrome.runtime.sendMessage({context:"url", url: $(this).children("a").attr("href")}, function(response) {
      console.log("Response: ", response);
      state = response;
      console.log(state);
      if (state == 'not_found' ) {
        imgstr = chrome.runtime.getURL("images/grey.png");
      }
      else if (state =='certified_ok' || state =='ok') {
        imgstr = chrome.runtime.getURL("images/green.png");
      }
      else {
        imgstr = chrome.runtime.getURL("images/red.png");
      }
      $(result).append('<div style="display:contents;"><a class="tt-toggle"><img style="width:20px" src="'+imgstr+'"></a>'+updown)

  });
$("tt-down").click(function(){console.log($(this).parent().parent().children(a).attr("href") )});
/*  $.getJSON("https://t2spekno-62a66c37.northeurope.cloudapp.azure.com/api/Links/1",
            {url:$(this).children("a").attr("href")},
            function(data) {
              console.log(data);
              state = data["state"];
              console.log(state);
              if (state == 'not_found' ) {
                imgstr = chrome.runtime.getURL("images/grey.png");
              }
              else if (state ==1) {
                imgstr = chrome.runtime.getURL("images/green.png");
              }
              else {
                imgstr = chrome.runtime.getURL("images/red.png");
              }
              $(result).append('<div style="display:contents;"><a class="tt-toggle"><img  src="'+imgstr+'"></a><div class="tt-link-box"><a class="tt-up" href="#"><img style="width:25px" src="'+imgup+'"></a><a class="tt-down" href="#"><img style="width:25px" src="'+imgdown+'"></a></div></div>')
            });*/
});
//$('.tt-toggle').click(function() {$(this).parent().children('.tt-link-box').toggle('slow');console.log('qui') });
