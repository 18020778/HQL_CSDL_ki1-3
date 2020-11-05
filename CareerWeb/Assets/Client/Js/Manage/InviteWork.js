$(document).ready(function () {
    $("#reschedule").click(function () {
        $("#changeDate").show();
        $("#verticalLine").css("height", "500px");
    })

    $(".stage:lt(2)").mouseenter(function () {
        var ind = $(this).index();
        $(".stage").eq(ind / 3).css({ "background-color": "#f07e1d", "cursor": "pointer", "color": "white" });
        $(".triangleStageGray").eq(ind / 3).css({ "border-left-color": "#f07e1d" });
    })
    $(".stage:lt(2)").mouseleave(function () {
        var ind = $(this).index();
        $(".stage").eq(ind / 3).css({ "background-color": "rgb(236, 236, 236)", "color": "black" });
        $(".triangleStageGray").eq(ind / 3).css({ "border-left-color": "rgb(236, 236, 236)" });
    })
    checkStatusWorkInvitation();
    acceptWorkInvitation();
    denyWorkInvitation();
})

function acceptWorkInvitation() {
    $("#accept").click(function () {
        var check = confirm("Bạn chắc chắn đồng ý lời mời việc làm này ?!");
        if (check == true) {
            var offerId = $("#offer").attr("stt");
            $.ajax({
                data: { offerId: offerId, accept: true },
                url: '/Manage/WorkInvitationRep',
                dataType: 'json',
                method: 'POST',
                beforeSend: function () {

                },
                success: function (res) {
                    if (res.status == true) {
                        var offerId = $("#offer").attr("stt");
                        var dbParam = "offerId=" + offerId;
                        window.location.href = '/Manage/Result?' + dbParam;
                    }
                }
            })
        }
    })
}

function denyWorkInvitation() {
    $("#ignore").click(function () {
        var check = confirm("Bạn chắc chắn từ chối lời mời làm việc này ?!");
        if (check == true) {
            var offerId = $("#offer").attr("stt");
            $.ajax({
                data: { offerId: offerId, accept: false },
                url: '/Manage/WorkInvitationRep',
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

function checkStatusWorkInvitation() {
    var offerId = $("#offer").attr("stt");
    $.ajax({
        data: { offerId: offerId },
        url: '/Manage/checkStatusWorkInvitation',
        dataType: 'json',
        method: 'POST',
        beforeSend: function () {

        },
        success: function (res) {
            if (res.workInvitationStatus == "accept") {
                window.location.href = "/Manage/Result";
            }
            if (res.workInvitationStatus == "deny") {
                $(".button").css("display", "none");
                $("#result2").css("display", "block");
                $("#status").css("display", "none");
            }

        }
    })
}