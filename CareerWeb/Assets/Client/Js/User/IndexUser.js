$(document).ready(function () {
    //Check định dạng tháng năm
    function checkFormatDate(inp) {
        if (inp.length == 0) return true;
        var ind = -1;
        for (var i = 0; i < inp.length; i++) {
            if (inp[i] == '/') {
                if (ind != - 1) {
                    return false;
                }
                ind = i;
            }
        }
        if (ind == -1) return false;
        var twoElements = inp.split("/");
        if (isNaN(twoElements[0]) || Number(twoElements[0]) < 1 || Number(twoElements[0]) > 12) {
            return false;
        }
        if (isNaN(twoElements[1]) || Number(twoElements[1]) < 1 || Number(twoElements[1]) > Number(new Date().getFullYear())) {
            return false;
        }
        return true;
    }
    function validateEmail(email) {
        const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }
    function checkFormatFullDate(inp) {
        var indFirst = -1;
        var indSecond = -1;
        for (var i = 0; i < inp.length; i++) {
            if (inp[i] == '/') {
                if (indFirst == -1) {
                    indFirst = i;
                    continue;
                }
                else if (indSecond == -1) {
                    indSecond = i;
                    continue;
                }
                else {
                    return false;
                }
            }
        }
        if (indFirst == -1 || indSecond == -1) return false;
        var threeElements = inp.split("/");
        if (isNaN(threeElements[0]) || Number(threeElements[0]) < 1 || Number(threeElements[0]) > 31) {
            return false;
        }
        if (isNaN(threeElements[1]) || Number(threeElements[1]) < 1 || Number(threeElements[1]) > 12) {
            return false;
        }
        if (isNaN(threeElements[2]) || Number(threeElements[2]) < 1 || Number(threeElements[2]) > Number(new Date().getFullYear())) {
            return false;
        }
        return true;

    }
    // Common
    $("#chart-infor").easyPieChart({
        size: 210,
        barColor: "blue",
        scaleColor: false,
        lineWidth: 12,
        trackColor: "#d1dced",
        lineCap: "circle",
        animate: 1000
    })
    $(".list-part").click(function (e) {
        e.preventDefault();
        var id = $(this).attr("data-link");
        var top = document.getElementById(id);
        var ps = Number($(top).offset().top) - 100;
        $("html, body").animate({ scrollTop: ps }, 650);
    })
    $(".add-more").click(function (e) {
        e.preventDefault();
        var ind = $(".add-more").index(this);
        var scroll = document.getElementById($(this).attr("add-for"));
        var topScroll = Number($(scroll).offset().top) - 100;
        var listInputInfor = $(".input-infor");
        $(listInputInfor[ind]).slideDown();
        $(listInputInfor[ind]).children().children(".submit").attr("use-for", "add");
        if ($(this).attr("add-for") != "finding-job-infor") {
            $(listInputInfor[ind]).children().children(".submit").text("Thêm");
        }
        else {
            $(listInputInfor[ind]).prev().slideUp();
        }
        $("html, body").animate({ scrollTop: topScroll }, 650);
    })
    $(".input-infor .infor-details .cancel").click(function () {
        var ind = $(".input-infor .infor-details .cancel").index(this);
        var listInputInfor = $(".input-infor");
        var containerInfor = $(".container-infor");
        $(containerInfor[ind]).slideDown();
        $(listInputInfor[ind]).slideUp();
        $(listInputInfor[ind]).children().children("input[type = 'text']").val("");
        $(listInputInfor[ind]).children().children("textarea").val("");
    })
    $(".input-time").blur(function () {
        var inp = $(this).val();
        if (inp == "" || !inp) return;
        if (!checkFormatDate(inp)) {
            $(this).css("background-color", "rgba(255, 117, 102, 0.4)");
            $(this).next().css("display", "block");
        }
        else {
            $(this).css("background-color", "white");
            $(this).next().css("display", "none");
        }
    })
    $("#user-dob").blur(function () {
        var inp = $(this).val();
        if (inp == "" || !inp) return;
        if (!checkFormatFullDate(inp)) {
            $(this).css("background-color", "rgba(255, 117, 102, 0.4)");
            $(this).next().css("display", "block");
        }
        else {
            $(this).css("background-color", "white");
            $(this).next().css("display", "none");
        }
    })
    $("#user-email").blur(function () {
        var inp = $(this).val();
        if (inp == "" || !inp) return;
        if (!validateEmail(inp)) {
            $(this).css("background-color", "rgba(255, 117, 102, 0.4)");
            $(this).next().css("display", "block");
        }
        else {
            $(this).css("background-color", "white");
            $(this).next().css("display", "none");
        }
    })
    $("#contact-id .first-title #edit-profile").click(function () {
        $("#contact-id .input-infor-user").slideDown();
    })
    $("#contact-id .infor-details .cancel").click(function () {
        $("#contact-id .input-infor-user").slideUp();
    })
    //Chỉnh sửa thông tin
    $(".input-infor-user .infor-details .submit").click(function () {
        var userName = $(".infor-details #user-name").val();
        var userDob = $(".infor-details #user-dob").val();
        var userGender = $(".infor-details #user-gender").val();
        var userEmail = $(".infor-details #user-email").val();
        var userMobile = $(".infor-details #user-mobile").val();
        var userArea = $(".infor-details #user-area").val();
        var userAddress = $(".infor-details #user-address").val();
        if (userName == "" || userDob == "" || userEmail == "" || userMobile == "" || userArea == ""
            || !userName || !userDob || !userEmail || !userMobile || !userArea || !checkFormatFullDate(userDob)
            || !validateEmail(userEmail)) {
            alert("Bạn phải nhập đủ và chính xác thông tin!");
            return;
        }
        var data = new FormData;
        data.append("userName", userName);
        data.append("userDob", userDob);
        data.append("userGender", userGender);
        data.append("userEmail", userEmail);
        data.append("userMobile", userMobile);
        data.append("userArea", Number(userArea));
        data.append("userAddress", userAddress);
        $.ajax({
            data: data,
            url: "/User/ModifyUser",
            method: "Post",
            dataType: "json",
            contentType: false,
            processData: false,
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    window.location.href = "/User/Index";
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    })
    //Thêm kinh nghiệm làm việc
    $("#work-experience .infor-details .submit").click(function () {
        var position = $("#work-experience .infor-details #position").val();
        var nameEnterprise = $("#work-experience .infor-details #name-enterprise").val();
        var timeStart = $("#time-start-ex").val();
        var timeEnd = $("#time-end-ex").val();
        var description = $("#work-experience .infor-details .description").val();
        if (!position || !nameEnterprise || !timeStart || position == "" || timeStart == "" || nameEnterprise == ""
            || !checkFormatDate(timeStart) || !checkFormatDate(timeEnd)) {
            alert("Bạn phải nhập đủ thông tin cần thiết và chính xác");
        }
        var useFor = $(this).attr("use-for");
        if (useFor == "add") {
            $.ajax({
                data: { position: Number(position), nameEnterprise: nameEnterprise, timeStart: timeStart, timeEnd: timeEnd, description: description },
                url: '/UserExperience/Insert',
                method: "Post",
                dataType: "Json",
                beforeSend: function () {

                },
                success: function (res) {
                    if (res.status != -1) {
                        window.location.href = "/User/Index";
                    }
                    else {
                        alert("Hệ thống gặp trục trặc");
                    }
                }
            });
        }
        else {
            var id = $(this).attr("id-save");
            $.ajax({
                data: { id : id, position: Number(position), nameEnterprise: nameEnterprise, timeStart: timeStart, timeEnd: timeEnd, description: description },
                url: '/UserExperience/Modify',
                method: "Post",
                dataType: "Json",
                beforeSend: function () {

                },
                success: function (res) {
                    if (res.status) {
                        window.location.href = "/User/Index";
                    }
                    else {
                        alert("Hệ thống gặp trục trặc");
                    }
                }
            });
        }
    })
    //Sửa kinh nghiệm việc làm:
    $("#work-experience .cover-button .edit-part").click(function () {
        var ind = $(this).attr("id-save");
        var index = $("#work-experience .cover-button .edit-part").index(this);
        $("#work-experience .container-infor").slideUp();
        $("#work-experience .input-infor").slideDown();
        var posList = $(".each-part #position-ex");
        var nameEntList = $(".each-part #nameEnterprise-ex");
        var timeList = $(".each-part #time-ex"); 
        var desList = $(".each-part #description-ex");
        $("#work-experience .input-infor #position").val($(posList[index]).attr("position-id"));
        $("#work-experience .input-infor #name-enterprise").val($(nameEntList[index]).text());
        var split = $(timeList[index]).text().split("-");
        $("#work-experience .input-infor #time-start-ex").val(split[0].trim());
        $("#work-experience .input-infor #time-end-ex").val((split[1].trim() == "now") ? "" : split[1].trim());
        $("#work-experience .input-infor .description").val($(desList[index]).text());
        $("#work-experience .infor-details .submit").text("Chỉnh sửa");
        $("#work-experience .infor-details .submit").attr("id-save", ind);
        $("#work-experience .infor-details .submit").attr("use-for", "edit");
    })
    //Xóa kinh nghiệm việc làm:
    $("#work-experience .cover-button .delete-part").click(function () {
        var cf = confirm("Bạn chắc chắn muốn xóa mục kinh nghiệm này chứ?");
        if (!cf) return;
        var ind = $(this).attr("id-save");
        var removeHTML = $(this).parent().parent();
        $.ajax({
            data: { id: ind },
            url: "/UserExperience/Remove",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    $(removeHTML).remove();
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    })
    // Thêm ngoại ngữ
    $("#foreign-language .infor-details .submit").click(function () {
        var languegeId = $("#foreign-language .infor-details #language").val();
        var level = $("#foreign-language .infor-details #level").val();
        var description = $("#foreign-language .infor-details .description").val();
        var useFor = $(this).attr("use-for");
        if (useFor == "add") {
            $.ajax({
                data: { languegeId: languegeId, level: level, description: description },
                url: '/UserForeignLanguage/Insert',
                method: "Post",
                dataType: "Json",
                beforeSend: function () {

                },
                success: function (res) {
                    if (res.status != -1) {
                        window.location.href = "/User/Index";
                    }
                    else {
                        alert("Hệ thống gặp trục trặc");
                    }
                }
            });
        }
        else {
            var id = $(this).attr("id-save");
            $.ajax({
                data: { id: id, languegeId: languegeId, level: level, description: description },
                url: '/UserForeignLanguage/Modify',
                method: "Post",
                dataType: "Json",
                beforeSend: function () {

                },
                success: function (res) {
                    if (res.status) {
                        window.location.href = "/User/Index";
                    }
                    else {
                        alert("Hệ thống gặp trục trặc");
                    }
                }
            });
        }
    });
    //Sửa ngoại ngữ:
    $("#foreign-language .cover-button .edit-part").click(function () {
        var ind = $(this).attr("id-save");
        var index = $("#foreign-language .cover-button .edit-part").index(this);
        $("#foreign-language .container-infor").slideUp();
        $("#foreign-language .input-infor").slideDown();
        var nameLan = $(".each-part #name-language");
        var nameLevel = $(".each-part #name-level");
        var des = $(".each-part #description-fr");
        $("#foreign-language .input-infor #language").val($(nameLan[index]).attr("language-id"));
        $("#foreign-language .input-infor #level").val($(nameLevel[index]).text().toLowerCase());
        $("#foreign-language .infor-details .description").val($(des[index]).text());
        $("#foreign-language .infor-details .submit").text("Chỉnh sửa");
        $("#foreign-language .infor-details .submit").attr("id-save", ind);
        $("#foreign-language .infor-details .submit").attr("use-for", "edit");
    })
    //Xóa ngoại ngữ:
    $("#foreign-language .cover-button .delete-part").click(function () {
        var cf = confirm("Bạn chắc chắn muốn xóa mục kinh nghiệm này chứ?");
        if (!cf) return;
        var ind = $(this).attr("id-save");
        var removeHTML = $(this).parent().parent();
        $.ajax({
            data: { id: ind },
            url: "/UserForeignLanguage/Remove",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    $(removeHTML).remove();
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    })
    // Thêm chứng chỉ, thành tích
    $("#award .infor-details .submit").click(function () {
        var file = $("#award .infor-details #image-award").get(0).files;
        var date = $("#award .infor-details #time-gain").val();
        var description = $("#award .infor-details .description").val();
        var useFor = $(this).attr("use-for");
        var data = new FormData;
        data.append("Name", description);
        data.append("Date", date);
        if (file && file[0]) {
            data.append("ImageFile", file[0]);
        }
        if (useFor == "add") {
            $.ajax({
                data: data,
                url: '/UserCertificate/Insert',
                method: "Post",
                dataType: "Json",
                contentType: false,
                processData: false,
                beforeSend: function () {

                },
                success: function (res) {
                    if (res.status != -1) {
                        window.location.href = "/User/Index";
                    }
                    else {
                        alert("Hệ thống gặp trục trặc");
                    }
                }
            });
        }
        else {
            var id = $(this).attr("id-save");
            data.append("ID", id);
            $.ajax({
                data: data,
                url: '/UserCertificate/Modify',
                method: "Post",
                dataType: "Json",
                contentType: false,
                processData: false,
                beforeSend: function () {

                },
                success: function (res) {
                    if (res.status) {
                        window.location.href = "/User/Index";
                    }
                    else {
                        alert("Hệ thống gặp trục trặc");
                    }
                }
            });
        }
    });
    //Hiện ảnh chứng chỉ
    $("#award .each-part button").click(function () {
        var link = $(this).attr("img-src");
        $("#cover-screen").css("display", "block");
        $("#cover-screen img").attr("src", link);
    })
    //Ẩn hiện ảnh chứng chỉ
    $("#cover-screen div").click(function () {
        $("#cover-screen").css("display", "none");
        $("#cover-screen img").attr("src", "");
    })
    //Sửa chứng chỉ
    $("#award .cover-button .edit-part").click(function () {
        var ind = $(this).attr("id-save");
        var index = $("#award .cover-button .edit-part").index(this); 
        $("#award .container-infor").slideUp();
        $("#award .input-infor").slideDown();
        var timeList = $(".each-part #time-cer");
        var nameCerList = $(".each-part #name-cer");
        $("#award .input-infor #time-gain").val($(timeList[index]).text());
        $("#award .infor-details .description").val($(nameCerList[index]).text());
        $("#award .infor-details .submit").text("Chỉnh sửa");
        $("#award .infor-details .submit").attr("id-save", ind);
        $("#award .infor-details .submit").attr("use-for", "edit");
    })
    //Xóa chứng chỉ:
    $("#award .cover-button .delete-part").click(function () {
        var cf = confirm("Bạn chắc chắn muốn xóa mục chứng chỉ, thành tích này chứ?");
        if (!cf) return;
        var ind = $(this).attr("id-save");
        var removeHTML = $(this).parent().parent();
        $.ajax({
            data: { id: ind },
            url: "/UserCertificate/Remove",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    $(removeHTML).remove();
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    })
    //Click add kĩ năng, chuyên ngành
    $(".type-skill").on("input", function () {
        var opt = $(this).next().children("option");
        var type = $(this).attr("type-for");
        for (var i = 0; i < opt.length; i++) {
            if ($(opt[i]).val() == $(this).val()) {
                $(this).val("");
                if (type == "major") {
                    var eachHTML = $(`<div class="each-skill">
                                      <div class="name-skill" job-id="`+ $(opt[i]).attr('job-id') + `"> ` + $(opt[i]).val() + `</div>
                                      <div class="delete-skill">x</div>
                                  </div>`);
                    $("#finding-job-infor .container-skill").append(eachHTML);
                }
                else {
                    var eachHTML = $(`<div class="each-skill">
                                      <div class="name-skill" parent-id="`+ $(opt[i]).attr('parent-id') + `"> ` + $(opt[i]).val() + `</div>
                                      <div class="delete-skill" job-id="` + $(opt[i]).attr('job-id') + `">x</div>
                                  </div>`);
                    $("#skill-job .container-skill").append(eachHTML);
                }
                break;
            }
        }
    })
    // Xóa add kĩ năng
    $(".container-skill").on("click", ".each-skill .delete-skill", function () {
        $(this).parent().remove();
    })
    // Thêm kĩ năng
    $("#skill-job .infor-details .submit").click(function () {
        var listAdd = $("#skill-job .container-skill .each-skill");
        if (listAdd == null || listAdd.length == 0) {
            alert("Bạn phải thêm ít nhất 1 kỹ năng!");
            return;
        }
        var listJobIdAdd = [];
        var listJobIdParent = [];
        for (var i = 0; i < listAdd.length; i++) {
            var firstEle = $(listAdd[i]).children(".name-skill");
            var secondEle = $(listAdd[i]).children(".delete-skill");
            listJobIdParent.push(Number($(firstEle).attr("parent-id")));
            listJobIdAdd.push(Number($(secondEle).attr("job-id")));
        }
        $.ajax({
            data: { listAdd: listJobIdAdd, listParent: listJobIdParent },
            url: "/UserMajor/AddListUserSkill",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    window.location.href = "/User/Index";
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    })
    //Xóa kỹ năng
    $("#skill-job .container-infor .delete-skill").click(function () {
        var cf = confirm("Xác nhận xóa ngành nghề này ?");
        if (!cf) return;
        var removeHTML = $(this).parent();
        var ind = Number($(this).attr("job-id"));
        $.ajax({
            data: { jobID: ind },
            url: "/UserMajor/DeleteUserSkill",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    $(removeHTML).remove();
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    })
    // Sửa thông tin xin việc
    $("#finding-job-infor .infor-details .submit").click(function () {
        var cf = confirm("Chỉnh sửa thông tin ?");
        if (!cf) return;
        var nameDesiredJob = $(".infor-details #desired-job").val();
        var salaryId = Number($(".infor-details #salary").val());
        var positionId = Number($(".infor-details #position-apply").val());
        var listAddUserJob = $("#finding-job-infor .container-skill .each-skill");
        var idList = [];
        for (var i = 0; i < listAddUserJob.length; i++) {
            var elm = $(listAddUserJob[i]).children(".name-skill");
            idList.push(Number($(elm).attr("job-id")));
        }
        $.ajax({
            data: { nameJob: nameDesiredJob, salaryId: salaryId, positionId: positionId, idList: idList },
            url: "/User/ModifyInforJob",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    window.location.href = "/User/Index";
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    })
    //Xóa chuyên ngành
    $(".present-skill .delete-skill").click(function () {
        var cf = confirm("Xác nhận , tất cả cá kỹ năng liên quan đến chuyên ngành này sẽ bị xóa theo?");
        if (!cf) return;
        var ind = $(this).attr("job-id");
        $.ajax({
            data: { jobID: ind },
            url: "/UserMajor/DeleteUserMajor",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (!res.status) {
                    alert("Hệ thống gặp trục trặc!");
                }
                window.location.href = "/User/Index";
            }
        })
    })
    ///Upload Image;
    $("#container-img #save-image").click(function () {
        var file = $("#container-img #image-browser").get(0).files;
        var data = new FormData;
        if (!file || !file[0]) {
            alert("Bạn cần chọn hình ảnh!");
        }
        data.append("ImageFile", file[0]);
        $.ajax({
            data: data,
            url: '/User/ImageUpload',
            dataType: 'json',
            method: 'POST',
            contentType: false,
            processData: false,
            beforeSend: function () {
            },
            success: function (res) {
                if (res.status) {
                    alert("Upload ảnh thành công!");
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })
    })
    var ReadImage = function (file) {
        var reader = new FileReader;
        var image = new Image;
        reader.readAsDataURL(file);
        reader.onload = function (_file) {
            image.src = _file.target.result;
            image.onload = function () {
                $("#container-img #img-profile").attr('src', image.src);
            }
        }
    }
    $("#container-img #choose-image").click(function () {
        $("#container-img #image-browser").click();
    })
    $("#container-img #image-browser").change(function () {
        var File = this.files;
        if (File && File[0]) {
            ReadImage(File[0]);
        }
    })
    //Limit length Select
    $("select").focus(function () {
        if ($(this).children().length > 6) {
            this.size = 6;
        }
    })
    $("select").mousedown(function () {
        if ($(this).children().length > 6) {
            this.size = 6;
        }
    })
    $("select").change(function () {
        this.size = 0;
    })
    $("select").blur(function () {
        this.size = 0;
    })
})