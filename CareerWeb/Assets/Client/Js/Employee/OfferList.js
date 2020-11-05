$(document).ready(function () {
    // Check định dạng email
    function validateEmail(email) {
        const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }
    $("#input-email-offer").blur(function () {
        var inp = $(this).val();
        if (inp == "" || !inp) return;
        if (!validateEmail(inp)) {
            $(this).css("background-color", "rgba(255, 117, 102, 0.4)");
        }
        else {
            $(this).css("background-color", "white");
        }
    })
    // Check định dạng ngày
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
    $("#input-limit-date").blur(function () {
        var inp = $(this).val();
        if (inp == "" || !inp) return;
        if (!checkFormatFullDate(inp)) {
            $(this).css("background-color", "rgba(255, 117, 102, 0.4)");
        }
        else {
            $(this).css("background-color", "white");
        }
    })
    //Xóa skill
    $("#box-main #container-skill").on("click", ".each-skill .delete-skill", function () {
        $(this).parent().remove();
    })
    /// Thêm skill
    $("#box-main .search-list").on("input", function () {
        var opt = $(this).next().children("option");
        for (var i = 0; i < opt.length; i++) {
            if ($(opt[i]).val() == $(this).val()) {
                if ($(this).attr("id") == "input-major-offer") {
                    $(this).attr("data-id", $(opt[i]).attr("data-id"));
                    $("input[name='offerMajor']").val($(opt[i]).attr("data-id"));
                }
                else {
                    $(this).val("");
                    var eachHTML = $(`<div class="each-skill">
                                        <div class="name-skill"> ` + $(opt[i]).val() + `</div>
                                        <div class="delete-skill">x</div>
                                        <input type="hidden" value = "`+ $(opt[i]).attr('data-id') +`" name = "offerListSkillId[]"/>
                                        <input type="hidden" value = "`+ $(opt[i]).attr('data-parent') +`" name = "offerListSkillParent[]"/>
                                     </div>`);
                    $("#box-main #container-skill").append(eachHTML);
                    break;
                }
            }
        }
    })
    /// Xóa offer
    $(".down-right .delete-offer").click(function () {
        var cf = confirm("Gỡ đơn tuyển dụng này ?");
        if (!cf) return;
        var offerID = $(this).attr("data-id");
        $.ajax({
            data: { offerID: offerID },
            url: "/OfferJob/DeleteOffer",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (!res.status) {
                    alert("Hệ thống gặp trục trặc!");
                }
                else {
                    window.location.href = "/Employee/ListAndCreateOffer";
                }
            }
        })
    })
    /// Xóa skill offer
    $("#box-main #container-skill").on("click", ".each-skill-save .delete-skill", function () {
        var cf = confirm("Xóa kỹ năng này khỏi yêu cầu của đơn tuyển dụng ?");
        if (!cf) return;
        var skillID = Number($(this).attr("data-id"));
        var offerID = $("#box-footer #both-input").attr("data-id");
        var remove = $(this).parent();
        $.ajax({
            data: { offerID: offerID, skillID: skillID },
            url: "/OfferJob/DeleteSkillOffer",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {
                
            },
            success: function (res) {
                if (!res.status) {
                    alert("Hệ thống gặp trục trặc");
                }
                else {
                    $(remove).remove();
                }
            }
        })
    })
    /// Bật edit-tab
    $(".each-offer .edit-offer").click(function () {
        $("#box-footer #both-input").text("EDIT");
        $("input[name='typeAction']").val("edit");
        var offerID = $(this).attr("data-id");
        $.ajax({
            data: { offerID: offerID },
            url: "/OfferJob/ReturnOffer",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (!res.status) {
                    alert("Hệ thống gặp trục trặc");
                    return;
                }
                var offerReturn = res.offer;
                $("#cover-screen").css("display", "block");
                $("#box-header").text("CHỈNH SỬA ĐƠN TUYỂN DỤNG");
                $("#box-main #input-name-offer").val(offerReturn.OfferName);
                $("#box-main #input-major-offer").attr("data-id", offerReturn.OfferMajor);
                $("#box-main #input-major-offer").val(res.majorName);
                $("input[name='offerMajor']").val(offerReturn.OfferMajor);
                $("#box-main #input-position-offer").val(offerReturn.OfferPosition);
                $("#box-main #input-salary-offer").val(offerReturn.OfferSalary);
                $("#box-main #input-area-offer").val(offerReturn.Area);
                $("#box-main #input-email-offer").val(offerReturn.ContactEmail);
                $("#box-main #input-experience-offer").val(offerReturn.ExperienceRequest);
                $("#box-main #input-leanring-offer").val(offerReturn.LearningLevelRequest);
                $("#box-main #input-amount-offer").val(offerReturn.Amount);
                $("#box-main #input-address").val(offerReturn.JobAddress);
                $("#box-main #input-gender").val(offerReturn.Sex);
                $("#box-main #input-limit-date").val(res.limitDate);
                //$("#box-main #offer-description").val(offerReturn.OfferDescription);
                CKEDITOR.instances["offer-description"].setData(offerReturn.OfferDescription);
                $(".part-right #display-image img").attr("src", offerReturn.OfferImage);
                for (var i = 0; i < res.listSkill.length; i++) {
                    var eachHTML = $(`<div class="each-skill-save">
                                        <div class="name-skill"> ` + res.listNameSkill[i] + `</div>
                                        <div class="delete-skill" data-id="`+ res.listSkill[i].ChildMajor + `" parent-id="` + res.listSkill[i].ParentMajor + `">x</div>
                                     </div>`);
                    $("#box-main #container-skill").append(eachHTML);
                }
                $("#box-footer #both-input").attr("data-id", offerID);
                $("input[name='saveID']").val(offerID);
            }
        })
    })
    /// Mở tab
    $("#container-title #button-new").click(function () {
        $("#cover-screen").css("display", "block");
        $("#box-footer #both-input").text("SUBMIT");
        $("#box-header").text("TẠO ĐƠN TUYỂN DỤNG");
        $("#box-footer input[name='typeAction']").val("add");
    })
    /// Tắt Tab
    $("#box-footer #cancel").click(function () {
        $("#cover-screen").css("display", "none");
        $("#box-main input").val("");
        $("#box-main textarea").val("");
        $("#box-main img").attr("src", "");
        $("#box-main #container-skill").empty();
    })
    //Thêm, chỉnh offer
    $("#box-footer #both-input").click(function () {
        var offerName = $("#box-main #input-name-offer").val();
        var offerMajor = $("#box-main #input-major-offer").attr("data-id");
        var offerPosition = $("#box-main #input-position-offer").val();
        var offerSalary = $("#box-main #input-salary-offer").val();
        var offerArea = $("#box-main #input-area-offer").val();
        var offerEmail = $("#box-main #input-email-offer").val();
        var offerExperience = $("#box-main #input-experience-offer").val();
        var offerLearning = $("#box-main #input-learning-offer").val();
        var offerAmount = $("#box-main #input-amount-offer").val();
        var offerLimitDate = $("#box-main #input-limit-date").val();
        var offerDescription = CKEDITOR.instances["offer-description"].getData();
        var listSkill = $("#box-main #container-skill .each-skill");
        if (!Boolean(offerName) || !Boolean(offerMajor) || !Boolean(offerEmail) || !Boolean(offerAmount) ||
            !Boolean(offerLimitDate) || !checkFormatFullDate(offerLimitDate) || !validateEmail(offerEmail)) {
            alert("Bạn cần nhập đủ và chính xác thông tin yêu cầu!");
            return;
        }
        if ($("input[name='typeAction']").val() == "add" && (!Boolean(offerDescription) || listSkill.length == 0)) {
            alert("Bạn cần nhập đủ và chính xác thông tin yêu cầu!");
            return;
        }
        if ($("input[name='typeAction']").val() == "add") {
            var cf = confirm("Xác nhận thêm đơn tuyển dụng mới ?");
        }
        else {
            var cf = confirm("Xác nhận chỉnh sửa đơn tuyển dụng ?");
        }
        if (!cf) return;
        $("#box-create form").submit();
    })
    /// Upload Image
    var ReadImage = function (file) {
        var reader = new FileReader;
        var image = new Image;
        reader.readAsDataURL(file);
        reader.onload = function (_file) {
            image.src = _file.target.result;
            image.onload = function () {
                $(".part-right #display-image img").attr('src', image.src);
            }
        }
    }
    $(".part-right #img-upload").click(function () {
        $(".part-right #imgBrowser").click();
    })
    $(".part-right #imgBrowser").change(function () {
        var File = this.files;
        if (File && File[0]) {
            ReadImage(File[0]);
        }
    })
})