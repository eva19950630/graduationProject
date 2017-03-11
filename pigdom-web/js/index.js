/*sidebar*/
$(document).ready(function() {
	function setHeight() {
		windowHeight = $(window).innerHeight();
		$('.sidebar').css('min-height', windowHeight);
	};
	setHeight();
  
	$(window).resize(function() {
		setHeight();
	});
});

$(document).ready(function(){
    $('#history-link').click(function(){
        $('.rightside-page').fadeOut('slow',function(){
            $('.rightside-page').html($('.history').html());
               $('.rightside-page').fadeIn('slow');
        })            
    })
    $('#analysis-link').click(function(){
        $('.rightside-page').fadeOut('slow',function(){
            $('.rightside-page').html($('.analysis').html());
               $('.rightside-page').fadeIn('slow');
        })            
    })
    $('#account-link').click(function(){
        $('.rightside-page').fadeOut('slow',function(){
            $('.rightside-page').html($('.account').html());
               $('.rightside-page').fadeIn('slow');
        })            
    })
});