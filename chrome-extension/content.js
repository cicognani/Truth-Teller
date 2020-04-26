//alert('Grrr.')
// chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
//   const re = new RegExp('bear', 'gi')
//   const matches = document.documentElement.innerHTML.match(re)
//   sendResponse({count: matches.length})
// })


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
  var rr = Math.floor(Math.random() * 3);
  if (rr == 0 ) {
    $(this).append('<div style="display:contents;"><a class="tt-toggle"><img  src="'+chrome.runtime.getURL("images/yellow.png")+'"></a><div class="tt-link-box"><ul><li><a class="tt-up" href="#">up</a></li><li><a class="tt-down" href="#">down</a></li></div></div>');
  }
  else if (rr==1) {
    $(this).append('<div style="display:contents;"><a class="tt-toggle"><img  src="'+chrome.runtime.getURL("images/green.png")+'"></a><div class="tt-link-box"><ul><li><a class="tt-up" href="#">up</a></li><li><a class="tt-down" href="#">down</a></li></div></div>');
  }
  else {
    $(this).append('<div style="display:contents;"><a class="tt-toggle"><img  src="'+chrome.runtime.getURL("images/red.png")+'"></a><div class="tt-link-box"><ul><li><a class="tt-up" href="#">up</a></li><li><a class="tt-down" href="#">down</a></li></div></div>')
  }
});
