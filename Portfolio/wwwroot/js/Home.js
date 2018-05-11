$(document).ready(function () {

    $('.item').first().addClass("active");
    $('.indicator').first().addClass("active");

    $(".info").hide();

    $(".info-btn-about").click(function () {
        $(".info-contact").hide();
        $(".info-about").fadeToggle();

        $('html, body').animate({
            scrollTop: $(".info-btn-about").offset().top
        }, 2000);
    });

    $(".info-btn-contact").click(function () {
        $(".info-about").hide();
        $(".info-contact").fadeToggle();

        $('html, body').animate({
            scrollTop: $(".info-btn-contact").offset().top
        }, 2000);
    });
});