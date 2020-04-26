$(document).ready(function() {$('head').append('<style> .tt-link-box { '+
      '  background: #ffffff;'+
      '  width: 100px;'+
      '  height: 50px;'+
        '  padding: 5px;'+
      '  display: none;'+
      '  float: right;position: absolute;z-index: 100;'+
      "}</style>");
      $('.tt-toggle').click(function() {$(this).parent().children('.tt-link-box').toggle('slow');console.log('qui') });
});

$( ".r" ).each(function( index ) {
  console.log($(this).children("a").attr("href"));
  $.getJSON("https://t2spekno-62a66c37.northeurope.cloudapp.azure.com/api/Links/1",
            {url:$(this).children("a").attr("href")},
            function(data) {
              console.log(data);
            });
  var imgstr = "";
  var imgup =  chrome.runtime.getURL("images/tup.png");
  var imgdown =  chrome.runtime.getURL("images/tdown.png");
  var rr = Math.floor(Math.random() * 3);
  if (rr == 0 ) {
    imgstr = chrome.runtime.getURL("images/yellow.png");
  }
  else if (rr==1) {
    imgstr = chrome.runtime.getURL("images/green.png");
  }
  else {
    imgstr = chrome.runtime.getURL("images/red.png");
  }
  $(this).append('<div style="display:contents;"><a class="tt-toggle"><img  src="'+imgstr+'"></a><div class="tt-link-box"><ul style="list-style-type: none;"><li><a class="tt-up" href="#"><img style="width:25px" src="'+imgup+'"></a></li><li><a class="tt-down" href="#"><img style="width:25px" src="'+imgdown+'"></a></li></div></div>')
});
