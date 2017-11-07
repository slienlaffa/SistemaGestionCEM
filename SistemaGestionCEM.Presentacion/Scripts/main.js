$(document).ready(function () {
    var altura = $('.menu').offset().top;

    $(window).on('scroll', function () {
        if ($(window).scrollTop() > altura) {
            $('.menu').addClass('fixed');
        } else {

            $('.menu').removeClass('fixed');
        }
    });
});