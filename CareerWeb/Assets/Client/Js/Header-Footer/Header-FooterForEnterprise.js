$(document).ready(function () {
    var opening = 0;
    $('.responsive_menu_icon').click(function () {
        if (opening == 0)
            opening++;
        else
            opening--;
        $('.responsive_menu').toggleClass("open");
    });
    $(window).resize(function () {
        if ($(window).width() > 1050) {
            $('.responsive_menu').removeClass("open");
        }
        else {
            if (opening == 1)
                $('.responsive_menu').addClass("open");
        }
    });
    $('.searching, .res_searching').click(function () {
        window.location.href = "/User/UserHome";
    });
    $("header .login_form .btn_box button").click(function () {
        var accName = $("header .login_form #acc").val();
        var pass = $("header .login_form #pass").val();
        if (!accName || !pass || accName == "" || pass == "") {
            alert("Bạn cần nhập đủ");
            return;
        }
        $.ajax({
            data: { accName: accName, passWord: pass },
            url: "/Account/Login",
            dataType: "Json",
            method: "Post",
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    if (res.type == 1) {
                        window.location.href = "/User/Index";
                    }
                    else if (res.type == 2 || res.type == 3) {
                        window.location.href = "/Enterprise/Index";
                    }
                }
                else {
                    if (res.error == -1) {
                        alert("Tài khoản không tồn tại");
                    }
                    else {
                        alert("Mật khẩu không chính xác");
                    }
                }
            }
        })

    })
    $('header .log_in').click(function () {
        $('header .login_form').css({
            "opacity": "1",
            "visibility": "visible"
        })
        $('header .fog_background').css({
            "opacity": "1",
            "visibility": "visible"
        })
    })
    $('header .fog_background').click(function () {
        $('header .login_form').css({
            "opacity": "0",
            "visibility": "hidden"
        })
        $('header .fog_background').css({
            "opacity": "0",
            "visibility": "hidden"
        })
    })
    $("header #control-account").click(function () {
        $("header #container-control").fadeTo();
    })
    $(document).on("click", function (event) {
        if ($(event.target).attr("id") != "control-account" && $(event.target).attr("id") != "container-control") {
            $('header #container-control').fadeOut();
        }
    });

})