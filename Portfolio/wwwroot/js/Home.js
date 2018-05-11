$(document).ready(function () {

    $('.item').first().addClass("active");
    $('.indicator').first().addClass("active");

    $(".info").hide();

    $(".info-btn-about").click(function () {
        $(".info-contact").hide();
        $(".info-about").fadeToggle();
    });

    $(".info-btn-contact").click(function () {
        $(".info-about").hide();
        $(".info-contact").fadeToggle();
    });
});