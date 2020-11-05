$(document).ready(function () {
    // Hàm check email
    function validateEmail(email) {
        const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }
    // Hàm check nhập email
    $("#choose-box-first #email").blur(function () {
        var inputEmail = $("#choose-box-first #email");
        var email = $(inputEmail).val();
        if (validateEmail(email)) {
            $(inputEmail).removeClass("errorEmail");
            $(inputEmail).addClass("correctInput");
        }
        else {
            $(inputEmail).removeClass("correctInput");
            $(inputEmail).addClass("errorEmail");
        }
    })
    // Hàm check nhập mật khẩu
    $("#choose-box-first #password").blur(function () {
        var inputPassword = $("#choose-box-first #password");
        var pass = $(inputPassword).val();
        if (pass.length >= 8) {
            $(inputPassword).removeClass("errorPass");
            $(inputPassword).addClass("correctInput");
        }
        else {
            $(inputPassword).removeClass("correctInput");
            $(inputPassword).addClass("errorPass");
        }
    });
    // Hàm check nhập lại mật khẩu
    $("#choose-box-first #retype").blur(function () {
        var inputRetype = $("#choose-box-first #retype");
        var retype = $(inputRetype).val();
        var pass = $("#choose-box-first #password").val();
        if (pass == retype && pass != null && pass != "") {
            $(inputRetype).removeClass("errorRetype");
            $(inputRetype).addClass("correctInput");
        }
        else {
            $(inputRetype).removeClass("correctInput");
            $(inputRetype).addClass("errorRetype");
        }
    });
    // Hàm check all thông tin
    // Part 1
    $("#choose-box-first #submit-first").click(function () {
        var email = $("#choose-box-first #email").val();
        var pass = $("#choose-box-first #password").val();
        var retype = $("#choose-box-first #retype").val();
        var coverLoad = $("#choose-box-first .cover-load");
        if (validateEmail(email) && pass.length >= 8 && retype == pass) {
            $.ajax({
                data: { email: email, pass: pass },
                url: '/Account/SendVertify',
                dataType: 'json',
                method: 'POST',
                beforeSend: function () {
                    $(coverLoad).css("display", "block");
                },
                success: function (res) {
                    $(coverLoad).css("display", "none")
                    if (res.status) {
                        $("#choose-box-first #title-vertify").css("display", "block");
                        $("#choose-box-first #codeVertify").css("display", "block");
                        $("#choose-box-first #submit-second").css("display", "block");
                        $("#choose-box-first #submit-first").css("display", "none");
                        $("#choose-box-first #email").attr("disabled", "disabled");
                        $("#choose-box-first #password").attr("disabled", "disabled");
                        $("#choose-box-first #retype").attr("disabled", "disabled");
                        emailReg = email;
                        passReg = pass;
                        code = res.code;
                    }
                    else {
                        if (res.checkEmail) {
                            alert("Đăng ký gặp trục trặc");
                        }
                        else {
                            alert("Gmail này đã tồn tại");
                        }
                    }
                }
            })
        }
        else {
            alert("Bạn cần nhập đầy đủ và chính xác thông tin!")
        }
    })
    $("#choose-box-first #submit-second").click(function () {
        var codeInput = $("#choose-box-first #codeVertify").val();
        var coverLoad = $("#choose-box-first .cover-load");
        var hiddenBox = $("#choose-box-first");
        var appearBox = $("#choose-box-second");
        if (codeInput == code) {
            code = "";
            $.ajax({
                data: { accountName: emailReg, accountPass: passReg, typeOfAccount: 'Employee' },
                url: '/Account/CreateAccount',
                dataType: 'json',
                method: 'POST',
                beforeSend: function () {
                    $(coverLoad).css("display", "block");
                },
                success: function (res) {
                    $(coverLoad).css("display", "none");
                    $(hiddenBox).css("display", "none");
                    $(appearBox).css("display", "block");
                    userID = res.userId;
                    passReg = "";

                }
            })
        }
        else {
            alert("Mã code sai");
        }
    })
    $("#choose-box-second button").click(function () {
        var val = $('#NameEnteprise').val();
        var id = $('#ListEnterprise option').filter(function () {
            return $(this).val() == val;
        }).data('id');
        var idEnterprise = id;
        var EmployeeName = $('#fullName').val();
        var bornDay = $('#born-day').val();
        var bornMonth = $('#born-month').val();
        var bornYear = $('#born-year').val();
        var dateBirth = bornYear + "-" + bornMonth + "-" + bornDay;
        var code = $('#CodeEnterprise').val();
        var mobile = $('#mobile').val();
        var position = Number($('#position').val());
        var coverLoad = ("#choose-box-second .cover-load");
        var sex = $("input[name='gender']:checked").val();
        console.log(id + " " + EmployeeName + " " + bornDay + " " + bornMonth + " " + bornYear +
            " " + dateBirth + " " + code + " " + mobile + " " + position + " " + sex);
        if (!idEnterprise || !EmployeeName || !dateBirth || !code || !mobile || !position || !sex ||
            idEnterprise=="" || EmployeeName=="" || dateBirth=="" || code=="" || mobile=="" || position=="" || sex=="" ) {
            alert("Bạn phải nhập đủ thông tin");
            return;
        }
        $.ajax({
            data: { UserID: userID, EnterpriseID: idEnterprise, EmployeeName: EmployeeName, Position: position, Sex: sex, BirthDay: dateBirth, Email: emailReg, Mobile: mobile, Code: code },
            url: '/Employee/CreateAccountInfor',
            dataType: 'json',
            method: 'POST',
            beforeSend: function () {
                $(coverLoad).css("display", "block");
            },
            success: function (res) {
                $(coverLoad).css("display", "none");
                if (!res.codeInput) {
                    alert("Mã code công ty sai");
                }
                else {
                    if (!res.status) {
                        alert("Hệ thống gặp trục trặc");
                    }
                    else {
                        alert("Đăng ký thành công");
                    }
                }
            }
        })
    })
    var emailReg = "";
    var passReg = "";
    var code = "";
    var userID;
})