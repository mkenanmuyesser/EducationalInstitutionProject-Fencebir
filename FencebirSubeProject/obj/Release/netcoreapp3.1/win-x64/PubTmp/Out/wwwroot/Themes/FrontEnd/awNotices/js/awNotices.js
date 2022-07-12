$('.awNotices').append('<span class="controller fa fa-pause"></span>');
$('.awNotices a:nth-of-type(1)').addClass('active');

function awNotice() {
  if(!$('.awNotices').hasClass('stopped')){
  var $active = $('.awNotices a.active');
  var $next = $active.next('a');    
  
  if ($next.length){
  $next.addClass('active');
  $active.removeClass('active');
  }else{
    $active.removeClass('active');
		$('.awNotices a:first-of-type').addClass('active');
  }
  }
}

$('.awNotices .controller').click(function(){
  $(this).toggleClass('fa-pause fa-play');
  $('.awNotices').toggleClass('stopped');
})

function awNotices(timer){
    setInterval( "awNotice()", timer);
}