$(document).ready(function () {
    $("#i1").click(function () {
        var targetOffset = $("#contactInfo").offset().top;
        $('html,body').animate({ scrollTop: targetOffset }, 1000);
        return false;
    });
    $("#i2").click(function () {
        var targetOffset = $("#generalInfo").offset().top;
        $('html,body').animate({ scrollTop: targetOffset }, 1000);
        return false;
    });
    $("#i3").click(function () {
        var targetOffset = $("#fileCV").offset().top;
        $('html,body').animate({ scrollTop: targetOffset }, 1000);
        return false;
    });
    $("#i4").click(function () {
        var targetOffset = $("#academicLevel").offset().top;
        $('html,body').animate({ scrollTop: targetOffset }, 1000);
        return false;
    });
    $("#i5").click(function () {
        var targetOffset = $("#experience").offset().top;
        $('html,body').animate({ scrollTop: targetOffset }, 1000);
        return false;
    });
    $("#i6").click(function () {
        var targetOffset = $("#englishLevel").offset().top;
        $('html,body').animate({ scrollTop: targetOffset }, 1000);
        return false;
    });
    $("#i7").click(function () {
        var targetOffset = $("#skill").offset().top;
        $('html,body').animate({ scrollTop: targetOffset }, 1000);
        return false;
    });
    $("#i8").click(function () {
        var targetOffset = $("#video").offset().top;
        $('html,body').animate({ scrollTop: targetOffset }, 1000);
        return false;
    });
})