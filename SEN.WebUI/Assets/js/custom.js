$('.open-left').click(function() {
	$('.list-keyword').toggleClass('display-ili');
	$('.list-keyword').toggleClass('display-noi');
	$('#icon-list-keyword').toggle();
	$('.side-menu .slimScrollDiv').toggleClass('sidebar-small-fix');
});

$('.btn-comment-modal').on('click', function() {
	$('#post-comment-modal').toggle(200);
	$('#post-info-modal').toggleClass('col-md-2');
	$('#post-gallery-modal').toggleClass('col-md-6');
	$('.post-image-modal').toggleClass('post-image-full-modal');
});


// Xem hình ảnh trước khi upload
function previewImg(event) {
    // Gán giá trị các file vào biến files
    var files = document.getElementById('img_file').files; 
 
    // Show khung chứa ảnh xem trước
    $('.box-preview-img').show();
  
    // Dùng vòng lặp for để thêm các thẻ img vào khung chứa ảnh xem trước
    for (i = 0; i < files.length; i++)
    {
        // Thêm thẻ img theo i
        $('.box-preview-img').append('<img src="" class="img-thumbnail abc" id="' + i +'">');
        $('.box-preview-img').append('<span id="abd" class="badge badge-danger">×</span>')
 
        // Thêm src vào mỗi thẻ img theo id = i
        $('.box-preview-img img:eq('+i+')').attr('src', URL.createObjectURL(event.target.files[i]));

        $('.box-preview-img img').hover(function(){
		    $('.box-preview-img span').css('display', 'inline');
		},
		function(){
		    $('.box-preview-img span').css('display', 'none');
		});

    	$('.box-preview-img span').click(function() {
    		$('.box-preview-img img').remove();
    		$(this).remove();
    	});

    }   
}

$('.blog-widget-action .btn-like').click(function() {
	$(this).toggleClass('blog-widget-action-active');

});


$('.blog-widget-action .btn-helpful').click(function() {
	$(this).toggleClass('blog-widget-action-active');

});