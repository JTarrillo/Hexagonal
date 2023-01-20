$(function () {
    $("#li_home").removeClass("active-route");
    var url = window.location.pathname;
    //console.log(url);
    var urlRegExp = new RegExp(url.replace(/\/$/, '') + "$");
    $('.menu a').each(function () {
        //console.log(this.href)
        //if (urlRegExp.test(this.href.replace(/\/$/, ''))) {
        //    //alert($(this).attr("id"));
        //    $(this).addClass('active-route');
        //}
    });   
});


$(document).ready(function () {
    var readURL = function (input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('.profile-pic').attr('src', e.target.result);
            };
            reader.readAsDataURL(input.files[0]);
        }
    };

    $(".file-upload").on('change', function () {
        readURL(this);
    });
    $(".image-upload ").on('click', function () {
        $(".file-upload").click();
    });
    
});

