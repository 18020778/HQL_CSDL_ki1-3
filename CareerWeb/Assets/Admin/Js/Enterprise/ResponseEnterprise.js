$(document).ready(function () {
    $(".container-enterprise").click(function () {
        var id = $(this).attr("ent-id");
        var index = $(".container-enterprise").index(this);
        $.ajax({
            data: { id: id },
            url: "/Admin/Enterprise/GetInfor",
            method: "POST",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                var listArea = res.listArea;
                var strArea = listArea[0].AreaName;
                for (var i = 1; i < listArea.length; i++) {
                    strArea += ", " + listArea[i].AreaName;
                }
                $("#container-full-infor").attr("ent-id", id);
                $("#container-full-infor").attr("index-class", index);
                $("#container-full-infor #ent-logo").attr("src", res.ent.ImageLogo);
                $("#container-full-infor #ent-name span").text(res.ent.EnterpriseName);
                $("#container-full-infor #ent-email span").text(res.ent.Email);
                $("#container-full-infor #ent-mobile span").text(res.ent.Mobile);
                $("#container-full-infor #ent-establish-year span").text(res.ent.EstablishYear);
                $("#container-full-infor #ent-create-date span").text(res.createDate);
                $("#container-full-infor #ent-area span").text(strArea);
                $("#container-job-main").empty();
                $("#container-job-sub").empty();
                $.each(res.importantCom, function (index, item) {
                    var eachHTML = $(`<div job-id = "` + item.JobID + `" class = "option-complete"> ` + item.JobName + `</div>`);
                    $("#container-job-main").append(eachHTML);
                });
                $.each(res.importantNew, function (index, item) {
                    var eachHTML = $(`<div job-id = "` + item.JobID + `" class = "option-new"> ` + item.JobName + `</div>`);
                    $("#container-job-main").append(eachHTML);
                    listNewImp.push(item.JobID);
                });
                $.each(res.normalCom, function (index, item) {
                    var eachHTML = $(`<div job-id = "` + item.JobID + `" class = "option-complete"> ` + item.JobName + `</div>`);
                    $("#container-job-sub").append(eachHTML);
                });
                $.each(res.normalNew, function (index, item) {
                    var eachHTML = $(`<div job-id = "` + item.JobID + `" class = "option-new"> ` + item.JobName + `</div>`);
                    $("#container-job-sub").append(eachHTML);
                    listNewSub.push(item.JobID);
                });
                $("#container-full-infor").css("display", "block");
              
            }
        });
    })
    $(".container-job").on("click", ".option-new", function () {
        var id = $(this).attr("job-id");
        var name = $(this).text();
        var indexOfClass = $(".option-new").index(this);
        var newJob = $("#check-new-job #job-name");
        $(newJob).attr("job-id", id);
        $(newJob).text(name);
        $("#cover-check-new-job").css("display", "block");
        $("#check-new-job").attr("ind-class", indexOfClass);
    })
    function removeList(id , index, add) {
        var optionNew = $(".option-new");
        var pr = $(optionNew[index]).parent();
        if ($(pr).attr("id") == "container-job-main") {
            var ind = listNewImp.indexOf(id);
            var sw = listNewImp[ind];
            listNewImp[ind] = listNewImp[listNewImp.length - 1];
            listNewImp[listNewImp.length - 1] = sw;
            listNewImp.pop();
        }
        else {
            var ind = listNewSub.indexOf(id);
            var sw = listNewSub[ind];
            listNewSub[ind] = listNewSub[listNewSub.length - 1];
            listNewSub[listNewSub.length - 1] = sw;
            listNewSub.pop();
        }
        //console.log(listNewImp + " " + listNewSub);
        if (add != "" && add != null) {
            var eachHTML = $(`<div job-id = "` + id + `" class = "option-complete"> ` + add + `</div>`);
            $(pr).append(eachHTML);
        }
        $(optionNew[index]).remove();
        
    }
    $("#check-new-job #accept").click(function () {
        var x = confirm("Thêm ngành nghề mới này vào trang web ?");
        if (x) {
            var id = Number($("#check-new-job #job-name").attr("job-id")); 
            var index = Number($("#check-new-job").attr("ind-class"));
            var jobName = $("#check-new-job #job-name");
            $.ajax({
                data: { jobID: id },
                url: "/Admin/JobMajor/ConfirmNewJob",
                method: "Post",
                dataType: "Json",
                beforeSend: function () {
                    //$("#cover-check-new-job").css("display", "block");
                },
                success: function () {
                    $("#cover-check-new-job").css("display", "none");
                    removeList(id, index, $(jobName).text());
                    $(jobName).text("");
                    $(jobName).attr("job-id", "");
                }
            })
        }
    })
    $("#check-new-job #cancel").click(function () {
        var x = confirm("Xóa ngành nghề này ?");
        if (x) {
            var id = Number($("#check-new-job #job-name").attr("job-id"));
            var index = Number($("#check-new-job").attr("ind-class"));
            var jobName = $("#check-new-job #job-name");
            $.ajax({
                data: { jobID: id },
                url: "/Admin/JobMajor/DeleteNewJob",
                method: "Post",
                dataType: "Json",
                beforeSend: function () {
                   // $("#cover-check-new-job").css("display", "block");
                },
                success: function () {
                    $("#cover-check-new-job").css("display", "none");
                    removeList(id, index, "");
                    $(jobName).text("");
                    $(jobName).attr("job-id", "");
                }
            })
        }
    })
    function turnOffContainerFullInfor() {
        listNewImp = [];
        listNewSub = [];
        $("#container-full-infor").css("display", "none");
        $("#container-full-infor").attr("ent-id", "");
        $("#container-full-infor").attr("index-class", "");
        $("#container-full-infor #ent-logo").attr("src", "");
        $("#container-full-infor #ent-name span").text("");
        $("#container-full-infor #ent-email span").text("");
        $("#container-full-infor #ent-mobile span").text("");
        $("#container-full-infor #ent-establish-year span").text("");
        $("#container-full-infor #ent-create-date span").text("");
        $("#container-full-infor #ent-area span").text("");
        $("#container-job-main").empty();
        $("#container-job-sub").empty();
    }
    $("#container-full-infor #submit").click(function () {
        if (listNewImp.length != 0 || listNewSub.length != 0) {
            alert("Bạn phải xử lý toàn bộ những công việc mới được thêm vào");
            return;
        }
        var x = confirm("Xác nhận yêu cầu tạo lập doanh nghiệp ?");
        if (!x) return;
        var idEnterprise = $("#container-full-infor").attr("ent-id"); 
        var indexClass = $("#container-full-infor").attr("index-class");
        $.ajax({
            data: { id: idEnterprise },
            url: "/Admin/Enterprise/AccpetRequest",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    if (res.checkEmail) {
                        alert("Xác nhận tạo doanh nghiệp thành công");
                        turnOffContainerFullInfor();
                        var rm = $(".container-enterprise");
                        $(rm[indexClass]).remove();
                    }
                    else {
                        alert("Hệ thống gặp trục trặc");
                    }
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })

    })
    $("#container-full-infor #remove").click(function () {
        var x = confirm("Hủy yêu cầu tạo lập doanh nghiệp ?(Xóa toàn bộ ngành nghề mới được yêu cầu)");
        if (!x) return;
        var idEnterprise = $("#container-full-infor").attr("ent-id"); 
        var indexClass = $("#container-full-infor").attr("index-class");
        $.ajax({
            data: { id: idEnterprise },
            url: "/Admin/Enterprise/RemoveRequest",
            method: "Post",
            dataType: "Json",
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status) {
                    alert("Hủy yêu cầu thành công");
                    turnOffContainerFullInfor();
                    var rm = $(".container-enterprise");
                    $(rm[indexClass]).remove();
                }
                else {
                    alert("Hệ thống gặp trục trặc");
                }
            }
        })

    })
    $("#container-full-infor #turn-off").click(function () {
        turnOffContainerFullInfor();
    })
    var listNewImp = [];
    var listNewSub = [];
})