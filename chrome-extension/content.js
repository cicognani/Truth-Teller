$(document).ready(function() {$('head').append('<style> .tt-link-box { background: transparent; width: 55px; height: 27px; padding: 5px; display: none; float: right; position: absolute; z-index: 100;'+
      "}</style>");
    //  $('.tt-toggle').click(function() {$(this).parent().children('.tt-link-box').toggle('slow');console.log('qui') });
});
//const bg = chrome.extension.getBackgroundPage();
var state ;
var imgstr = "";
var imgup =  chrome.runtime.getURL("images/tup.png");
var imgdown =  chrome.runtime.getURL("images/tdown.png");
$( ".r" ).each(function( index ) {
  console.log($(this).children("a").attr("href"));
  var result = this;
  $.getJSON("https://t2spekno-62a66c37.northeurope.cloudapp.azure.com/api/Links/1",
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
            });
});
$('.tt-toggle').click(function() {$(this).parent().children('.tt-link-box').toggle('slow');console.log('qui') });
