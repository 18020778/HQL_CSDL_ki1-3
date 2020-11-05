$(document).ready(function () {
    $("#reschedule").click(function () {
        $("#changeDate").show();
        $("#verticalLine").css("height", "670px");
    })
    $(".stage:lt(1)").mouseenter(function () {
        var ind = $(this).index();
        $(".stage").eq(ind / 3).css({ "background-color": "#f07e1d", "cursor": "pointer", "color": "white" });
        $(".triangleStageGray").eq(ind / 3).css({ "border-left-color": "#f07e1d" });
    })
    $(".stage:lt(1)").mouseleave(function () {
        var ind = $(this).index();
        $(".stage").eq(ind / 3).css({ "background-color": "rgb(236, 236, 236)", "color": "black" });
        $(".triangleStageGray").eq(ind / 3).css({ "border-left-color": "rgb(236, 236, 236)" });
    })
    checkStatusInterView();
    acceptInterview();
    denyInterview();
})


function acceptInterview() {
    $("#accept").click(function () {
        var check = confirm("Bạn chắc chắn đồng ý lời mời phỏng vấn này ?!");
        if (check == true) {
            var offerId = $("#offer").attr("stt");
            $.ajax({
                data: { offerId: offerId, accept: true },
                url: '/Manage/InterViewInvitationRep',
                dataType: 'json',
                method: 'POST',
                beforeSend: function () {

                },
                success: function (res) {
                    if (res.status == true) {
                        $(".button").css("display", "none");
                        $("#result").css("display", "block");
                        $("#status").css("display", "none");
                    }
                }
            })
        }
    })
}

function denyInterview() {
    $("#ignore").click(function () {
        var check = confirm("Bạn chắc chắn từ chối lời mời phỏng vấn này ?!");
        if (check == true) {
            var offerId = $("#offer").attr("stt");
            $.ajax({
                data: { offerId: offerId, accept: false },
                url: '/Manage/InterViewInvitationRep',
                dataType: 'json',
                method: 'POST',
                beforeSend: function () {

                },
                success: function (res) {
                    if (res.status == true) {
                        $(".button").css("display", "none");
                        $("#result2").css("display", "block");
                        $("#status").css("display", "none");
                    }
                }
            })
        }
    })
}

function checkStatusInterView() {
    var offerId = $("#offer").attr("stt");
    $.ajax({
        data: { offerId: offerId },
        url: '/Manage/checkStatusInterview',
        dataType: 'json',
        method: 'POST',
        beforeSend: function () {

        },
        success: function (res) {
            if (res.interviewStatus != "waiting") {
                $(".button").css("display", "none");
            }
            if (res.interviewStatus == "accept") {
                $("#result").css("display", "block");
                $("#status").css("display", "none");
            }
            if (res.interviewStatus == "deny") {
                
                $("#result2").css("display", "block");
                $("#status").css("display", "none");
            }
            if (res.workStatus == "Trượt pv") {
                $("#result").css("display", "none");
                $("#result2").css("display", "none");
                $("#mainContent").css("display", "none");
                $("#result3").css("display", "block");
                $("#horizontalLine").css("display", "none");
                $("#verticalLine").css("display", "none");
            }
            
        }
    })
}