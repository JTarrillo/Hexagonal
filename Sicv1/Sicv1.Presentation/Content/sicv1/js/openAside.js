
function openAside() {
    document.getElementById("slide-out").classList.toggle("aside-open");
    document.getElementById("status-header").classList.toggle("header-display");
    document.getElementById("section-content").classList.toggle("status-body");
    document.getElementById("bg-toggle-movile").classList.toggle("bg-toogle-on");
    document.getElementById('icon-arrow-bar').classList.toggle('hidden');
    document.getElementById('icon-line-bar').classList.toggle('flex');
    document.getElementById('toggleButton').classList.toggle('press-aside-bar');
    //document.getElementsByClassName('top-fixed-action')[0].classList.toggle('side');
}

$(function () {
    $(window).scroll(function () {
        if ($(window).scrollTop() > 90) {
            $(".top-fixed-action").addClass('active');//.fadeIn();
            $(".space-top-fixed-action").addClass('active');//.fadeIn();
        } else {
            $(".top-fixed-action").removeClass('active');
            $(".space-top-fixed-action").removeClass('active');
        }
    });
});