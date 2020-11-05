$(document).ready(function () {
    //common
    $("#choose-box-second input[type='text']").blur(function () {
        var str = $(this).val().trim();
        var ret = "";
        for (var i = 0; i < str.length; i += 1) {
            if (str[i] != ' ') {
                if (i == 0 || str[i - 1] == ' ' || str[i - 1] == '/') ret += str[i].toUpperCase();
                else ret += str[i].toLowerCase();
            }
            else {
                if (str[i - 1] != ' ') ret += ' ';
            }
        }
        $(this).val(ret);
        return;
    })
    // Part 1
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
                data: { accountName: emailReg, accountPass: passReg, typeOfAccount: 'Enterprise' },
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
                    listArea = res.listArea;
                }
            })
        }
        else {
            alert("Mã code sai");
        }
    })
    //Part 2
    $("#search-area").keyup(function () {
        var sg = $("#suggest-area");
        $(sg).empty();
        var str = $(this).val();
        if (str == "" || str == null) return;
        str = str.toLowerCase();
        $.each(listArea, function (index, item) {
            if (item.NameArea.toLowerCase().indexOf(str) != -1) {
                var eachHTML = $(`<div area-id = "` + item.AreaId + `" class = "list-sg"> ` + item.NameArea + `</div>`);
                $(sg).append(eachHTML);
            }
        });
    })
    $("#suggest-area").on("click", ".list-sg", function () {
        var ind = Number($(this).attr("area-id"));
        var text = $(this).text();
        if (addIdListArea.indexOf(ind) == -1) {
            addIdListArea.push(ind);
            var eachHTML = $(`<div class = "option-choosed" area-id = "` + ind + `"><div>` + text + `</div> <span>x</span></div>`);
            $("#option-area").append(eachHTML);
        }
        //console.log(addIdListJob)
    })
    $("#option-area").on("click", ".option-choosed span", function () {
        var ind = $("#option-area .option-choosed span").index(this);
        var opt = $("#option-area .option-choosed");
        var removeID = addIdListArea.indexOf(Number($(opt[ind]).attr("area-id")));
        $(opt[ind]).remove();
        var swap = 0;
        swap = addIdListArea[addIdListArea.length - 1];
        addIdListArea[addIdListArea.length - 1] = addIdListArea[removeID];
        addIdListArea[removeID] = swap;
        addIdListArea.pop();
        //console.log(addIdListArea);
    })
    $("#search-important").keyup(function () {
        var sg = $("#suggest-important");
        $(sg).empty();
        var str = $(this).val();
        if (str == "" || str == null) return;
        str = str.toLowerCase();
        $.each(listJob, function (index, item) {
            if (item.JobName.toLowerCase().indexOf(str) != -1) {
                var eachHTML = $(`<div job-id = "` + item.JobID + `" class = "list-sg"> ` + item.JobName + `</div>`);
                $(sg).append(eachHTML);
            }
        });
    })
    $("#suggest-important").on("click", ".list-sg", function () {
        var ind = Number($(this).attr("job-id"));
        var text = $(this).text();
        if (addIdListJobSub.indexOf(ind) != -1) {
            alert("Bạn đã chọn ngành nghề này dưới phần phụ!");
            return;
        }
        if (addIdListJobImp.indexOf(ind) == -1) {
            addIdListJobImp.push(ind);
            var eachHTML = $(`<div class = "option-choosed" job-id = "` + ind + `"><div>` + text + `</div> <span>x</span></div>`);
            $("#option-important").append(eachHTML);
        }
        //console.log(addIdListJob)
    })
    $("#option-important").on("click", ".option-choosed span", function () {
        var ind = $("#option-important .option-choosed span").index(this);
        var opt = $("#option-important .option-choosed");
        var removeID = addIdListJobImp.indexOf(Number($(opt[ind]).attr("job-id")));
        $(opt[ind]).remove();
        var swap = 0;
        swap = addIdListJobImp[addIdListJobImp.length - 1];
        addIdListJobImp[addIdListJobImp.length - 1] = addIdListJobImp[removeID];
        addIdListJobImp[removeID] = swap;
        addIdListJobImp.pop();
        //console.log(addIdListJobImp);
    })
    $("#search-sub").keyup(function () {
        var sg = $("#suggest-sub");
        $(sg).empty();
        var str = $(this).val();
        if (str == "" || str == null) return;
        str = str.toLowerCase();
        $.each(listJob, function (index, item) {
            if (item.JobName.toLowerCase().indexOf(str) != -1) {
                var eachHTML = $(`<div job-id = "` + item.JobID + `" class = "list-sg"> ` + item.JobName + `</div>`);
                $(sg).append(eachHTML);
            }
        });
    })
    $("#suggest-sub").on("click", ".list-sg", function () {
        var ind = Number($(this).attr("job-id"));
        var text = $(this).text();
        if (addIdListJobImp.indexOf(ind) != -1) {
            alert("Bạn đã chọn ngành nghề này trên phần chính!");
            return;
        }
        if (addIdListJobSub.indexOf(ind) == -1) {
            addIdListJobSub.push(ind);
            var eachHTML = $(`<div class = "option-choosed" job-id = "` + ind + `"><div>` + text + `</div> <span>x</span></div>`);
            $("#option-sub").append(eachHTML);
        }
        //console.log(addIdListJob)
    })
    $("#option-sub").on("click", ".option-choosed span", function () {
        var ind = $("#option-sub .option-choosed span").index(this);
        var opt = $("#option-sub .option-choosed");
        var removeID = addIdListJobSub.indexOf(Number($(opt[ind]).attr("job-id")));
        $(opt[ind]).remove();
        var swap = 0;
        swap = addIdListJobSub[addIdListJobSub.length - 1];
        addIdListJobSub[addIdListJobSub.length - 1] = addIdListJobSub[removeID];
        addIdListJobSub[removeID] = swap;
        addIdListJobSub.pop();
        //console.log(addIdListJobSub);
    })
    $(".add-more").click(function () {
        $("#choose-box-second #cover").css("display", "block");
        $("#container-add #add").attr("job-id", $(this).attr("atr"));
    })
    $("#container-add #add").click(function () {
        var jobAdd = $("#container-add input[type='text']").val();
        if (jobAdd == "" || jobAdd == null) {
            alert("Bạn cần nhập tên công việc!");
            return;
        }
        if ($(this).attr("job-id") == "job-imp") {
            if (newListJobImp.indexOf(jobAdd) == -1) {
                newListJobImp.push(jobAdd);
                var eachHTML = $(`<div class = "option-add" atr = 'job-imp'><div>` + jobAdd + `</div> <span>x</span></div>`);
                $("#container-add input[type='text']").val("");
                $("#choose-box-second #cover").css("display", "none");
                $("#option-important").append(eachHTML);
            }
            else {
                alert("Ngành nghề đã được lựa chọn");
                return;
            }
        }
        else {
            if (newListJobSub.indexOf(jobAdd) == -1) {
                newListJobSub.push(jobAdd);
                var eachHTML = $(`<div class = "option-add" atr = 'job-sub'><div>` + jobAdd + `</div> <span>x</span></div>`);
                $("#container-add input[type='text']").val("");
                $("#choose-box-second #cover").css("display", "none");
                $("#option-sub").append(eachHTML);
            }
            else {
                alert("Ngành nghề đã được lựa chọn");
                return;
            }
        }
    })
    $("#option-important").on("click", ".option-add span", function () {
        var ind = $("#option-important .option-add span").index(this);
        var opt = $("#option-important .option-add");
        var removeID = newListJobImp.indexOf($(opt).text());
        $(opt[ind]).remove();
        var swap = 0;
        swap = newListJobImp[newListJobImp.length - 1];
        newListJobImp[newListJobImp.length - 1] = newListJobImp[removeID];
        newListJobImp[removeID] = swap;
        newListJobImp.pop();
    })
    $("#option-sub").on("click", ".option-add span", function () {
        var ind = $("#option-sub .option-add span").index(this);
        var opt = $("#option-sub .option-add");
        var removeID = newListJobSub.indexOf($(opt).text());
        $(opt[ind]).remove();
        var swap = 0;
        swap = newListJobSub[newListJobSub.length - 1];
        newListJobSub[newListJobSub.length - 1] = newListJobSub[removeID];
        newListJobSub[removeID] = swap;
        newListJobSub.pop();
    })
    $("#container-add #cancel").click(function () {
        $("#container-add input[type='text']").val("");
        $("#choose-box-second #cover").css("display", "none");
    })
    $("#choose-box-second button").click(function () {
        var coverLoad = $("#choose-box-second .cover-load");
        var name = $("#enterpriseName").val();
        var mobile = $("#mobile").val();
        var establishYear = $("#establishYear").val();
        var enterpriseSize = Number($("#enterprise-size").val());
        var typeEnterprise = Number($("#type-company").val());
        if (name == "" || !name || mobile == "" || !mobile || establishYear == "" || !establishYear ||
            srcImage == "" || addIdListArea.length < 1 || (addIdListJobImp.length < 1 && newListJobImp.length < 1)) {
            alert("Bạn cần nhập đầy đủ thông tin(trừ các chuyên ngành phụ)!")
            return;
        }
        console.log(addIdListJobImp + " " + addIdListJobSub + " " + newListJobImp + " " + newListJobSub);
        var data = new FormData;
        data.append("Id", userID);
        data.append("Name", name);
        data.append("Logo", srcImage);
        data.append("EstablishYear", Number(establishYear));
        data.append("Email", emailReg);
        data.append("Size", enterpriseSize);
        data.append("Type", typeEnterprise);
        data.append("Mobile", mobile);
        for (var i = 0; i < addIdListArea.length; i++) {
            data.append("ListArea", addIdListArea[i]);
        }
        for (var i = 0; i < addIdListJobImp.length; i++) {
            data.append("ListJobImp", addIdListJobImp[i]);
        }
        for (var i = 0; i < addIdListJobSub.length; i++) {
            data.append("ListJobSub", addIdListJobSub[i]);
        }
        for (var i = 0; i < newListJobImp.length; i++) {
            data.append("NewJobImp", newListJobImp[i]);
        }
        for (var i = 0; i < newListJobSub.length; i++) {
            data.append("NewJobSub", newListJobSub[i]);
        }
        $.ajax({
            data: data,
            url: '/Enterprise/CreateAccountInfor',
            dataType: 'json',
            method: 'POST',
            contentType: false,
            processData: false,
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
                    alert("Đăng ký gặp trục trặc");
                }
            }
        })
       
    })
    ///Upload Image;
    $("#uploadImage").click(function () {
        var file = $("#imageBrowse").get(0).files;
        var data = new FormData;
        if (!file || !file[0]) {
            alert("Bạn cần chọn hình ảnh logo!");
        }
        data.append("ImageFile", file[0]);
        $.ajax({
            data: data,
            url: '/Enterprise/ImageUpload',
            dataType: 'json',
            method: 'POST',
            contentType: false,
            processData: false,
            beforeSend: function () {
              
            },
            success: function (res) {
                srcImage = res.srcImage;
                alert("Upload ảnh thành công");
            }
        })
    })
    $("#chooseImage").click(function () {
        $("#imageBrowse").click();
    })
    $("#imageBrowse").change(function () {
        var File = this.files;
        if (File && File[0]) {
            ReadImage(File[0]);
        }
    })
    // Biến lưu dữ liệu
    var srcImage = "";
    var emailReg = "";
    var passReg = "";
    var code = "";
    var userID;
    var listJob;
    var listArea;
    var addIdListArea = [];
    var addIdListJobImp = [];
    var addIdListJobSub = [];
    var newListJobImp = [];
    var newListJobSub = [];
})
var ReadImage = function (file) {
    var reader = new FileReader;
    var image = new Image;
    reader.readAsDataURL(file);
    reader.onload = function (_file) {
        image.src = _file.target.result;
        image.onload = function () {
            $("#targetImg").attr('src', image.src);
        }
    }
}