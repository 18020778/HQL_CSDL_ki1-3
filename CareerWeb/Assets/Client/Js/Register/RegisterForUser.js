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
                data: { accountName: emailReg, accountPass: passReg, typeOfAccount :  'User'},
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
                    listJob = res.listJob;
                    passReg = "";
                    $.each(res.listArea, function (index, item) {
                        var eachHTML = $(`<option value = "`+ item.AreaId +`"> `+ item.NameArea +`</option>`);
                        $("#choose-box-second .container-input #area").append(eachHTML);
                    });
                }
            })
        }
        else {
            alert("Mã code sai");
        }
    })
     // Part 2
    $("#choose-box-second #searchMajor").keyup(function () {
        var sg = $("#choose-box-second #container-suggest");
        $(sg).empty();
        var str = $("#choose-box-second #searchMajor").val();
        if (str == "" || str == null) return;
        str = str.toLowerCase();
        $.each(listJob, function (index, item) {
            if (item.JobName.toLowerCase().indexOf(str) != -1) {
                var eachHTML = $(`<div job-id = "` + item.JobID + `" class = "list-job"> ` + item.JobName + `</div>`);
                $(sg).append(eachHTML);
            }
        });
    })
    $("#choose-box-second #container-suggest").on("click", ".list-job", function () {
        var ind = Number($(this).attr("job-id"));
        var text = $(this).text(); 
        if (addIdListJob.indexOf(ind) == -1) {
            addIdListJob.push(ind);
            var eachHTML = $(`<div class = "option-choosed" job-id = "` + ind + `"><div>` + text + `</div> <span>x</span></div>`);
            $("#choose-box-second #container-option").append(eachHTML);
        }
        //console.log(addIdListJob)
    })
    $("#choose-box-second #container-option").on("click", ".option-choosed span", function () {
        var ind = $(".option-choosed span").index(this);
        var opt = $(".option-choosed");
        var removeJobIDInd = addIdListJob.indexOf(Number($(opt[ind]).attr("job-id")));
        //console.log("Remove id :" + removeJobIDInd + ",ID :" + $(opt[ind]).attr("job-id"));
        $(opt[ind]).remove();
        var swap = 0;
        swap = addIdListJob[addIdListJob.length - 1];
        addIdListJob[addIdListJob.length - 1] = addIdListJob[removeJobIDInd];
        addIdListJob[removeJobIDInd] = swap;
        addIdListJob.pop();
        //console.log(addIdListJob);
    })
    $("#choose-box-second button").click(function () {
        var name = $(".container-input #fullName").val();
        var mobile = $(".container-input #mobile").val();
        var dayBirth = $(".container-input #born-day").val();
        var monthBirth = $(".container-input #born-month").val();
        var yearBirth = $(".container-input #born-year").val();
        var dateBirth = yearBirth + "-" + monthBirth + "-" + dayBirth;
        var area = Number($(".container-input #area").val());
        var sex = $("input[name='gender']:checked").val();
        var atSchool = $("input[name='atSchool']:checked").val();
        var coverLoad = $("#choose-box-second .cover-load");
        if (name != "" && name != null && mobile != "" && mobile != null && addIdListJob.length > 0) {
            $.ajax({
                data: { userID: userID, email: emailReg, name: name, mobile: mobile, dateBirth: dateBirth, sex: sex, atSchool: atSchool, area: area, listJob: addIdListJob },
                url: '/User/CreateAccountInfor',
                dataType: 'json',
                method: 'POST',
                beforeSend: function () {
                    $(coverLoad).css("display", "block");
                },
                success: function (res) {
                    $(coverLoad).css("display", "none");
                    if (res.status) {
                        $("#choose-box-second").css("display", "none");
                        $("#finish-box").css("display", "block");
                    }
                    else {
                        alert("Đăng ký thông tin gặp trục trặc");
                    }
                }
            })
        }
        else {
            alert("Bạn cần nhập và lựa chọn đầy đủ thông tin cần thiết");
        }

    })
    $("select").focus(function () {
        this.size = 6;
    })
    $("select").mousedown(function () {
        this.size = 6;
    })
    $("select").change(function () {
        this.size = 0;
    })
    $("select").blur(function () {
        this.size = 0;
    })
    /// Biến lưu dữ liệu
    var emailReg = "";
    var passReg = "";
    var code = "";
    var userID;
    var listJob;
    var addIdListJob = [];
})