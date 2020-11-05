$(document).ready(function () {
    var $table = $('#tableOfContents');
    $(window).scroll(function () {
        if ($(window).scrollTop() > 490) {
            $table.css({ "position": "fixed", "top": "30px", "left": "0px", "width": "250px", "margin-left": "2.5%", "z-index": "3", "margin-top": "75px" });
        }
        else if ($(window).scrollTop() < 490) {
            $table.css({ "position": "relative", "top": "0px", "width": "96%", "z-index": "3","margin-top": "30px" });
        }
    });
    $("#i1").click(function() {
        var targetOffset = $("#employmentInfo").offset().top - 80;
        $("html, body").animate({ scrollTop: targetOffset }, 1000);
        return false;
    });
    $("#i2").click(function() {
        var targetOffset = $("#workingConditions").offset().top - 80;
        $("html, body").animate({ scrollTop: targetOffset }, 1000);
        return false;
    });
    $("#i3").click(function() {
        var targetOffset = $("#jobDescription").offset().top - 80;
        $("html, body").animate({ scrollTop: targetOffset }, 1000);
        return false;
    });
    $("#i4").click(function() {
        var targetOffset = $("#entitlements").offset().top - 80;
        $("html, body").animate({ scrollTop: targetOffset }, 1000);
        return false;
    });
    $("#i5").click(function() {
        var targetOffset = $("#profileRequest").offset().top - 80;
        $("html, body").animate({ scrollTop: targetOffset }, 1000);
        return false;
    });
    $("#i6").click(function() {
        var targetOffset = $("#skillsNeeded").offset().top - 80;
        $("html, body").animate({ scrollTop: targetOffset }, 1000);
        return false;
    });
});