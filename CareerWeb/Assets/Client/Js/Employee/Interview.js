var userId;
var offerId;
var employeeId;

$(document).ready(function () {
    $(window).scroll(function () {
        contactFixed();
    });

    $("#fail").click(function () {
        $("#message").slideDown(500);
    });

    $("#cancel").click(function () {
        $("#message").slideUp(500);
    });

    $("#deny").click(function () {
        $("#message2").slideDown(500);
    });

    $("#cancel2").click(function () {
        $("#message2").slideUp(500);
    });


    userId = $("#user").attr("stt");
    offerId = $("#offer").attr("stt");
    employeeId = $("#employee").attr("stt");

    wait_for_response();
    wait_for_response2();
    console.log("here");
});


function contactFixed() {
    var pos_body = $("html,body").scrollTop();
    if (pos_body >= 282) {
        $("#contact").addClass("fixed-contact");
    }
    if (pos_body < 282) {
        $("#contact").removeClass("fixed-contact");
    }

}


function deleteCVConfirm() {
    var check = confirm("Bạn có chắc sẽ loại ứng viên này ?!");
    if (check == true) {
        $.ajax({
            data: { userId: userId, offerId: offerId },
            url: '/Employee/FailedCV',
            dataType: 'json',
            method: 'POST',
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status == true) {
                    $("#interview1").css("display", "none");
                    $("#fail").css("display", "none");
                    $("#message").css("display", "none");
                    $("#failmessage").css("display", "block");
                }
            }
        })
    }
}

function FailedConfirm() {
    var check = confirm("Bạn có chắc sẽ loại ứng viên này ?!");
    if (check == true) {
        $.ajax({
            data: { userId: userId, offerId: offerId },
            url: '/Employee/FailedInterview',
            dataType: 'json',
            method: 'POST',
            beforeSend: function () {

            },
            success: function (res) {
                if (res.status == true) {
                    $("#accept").css("display", "none");
                    $("#deny").css("display", "none");
                    $("#message2").css("display", "none");
                    $("#failmessage2").css("display", "block");
                }
            }
        })
    }
}

function setUpInterview() {
    $("#overview").fadeOut(100);
    $("#interview").fadeIn(100);
    $("#thu-moi").fadeIn(100);
    $(".steps li:first-child").removeClass("active");
    $(".steps li:nth-child(2)").addClass("active");
    $("#process").text(" Tiến trình: Phỏng vấn");
    $("#main h1").text("Tiến trình phỏng vấn ứng viên : Giai đoạn phỏng vấn");
}



function interviewFormCheck() {
    var input = document.querySelectorAll("#interview input");
    var notify = document.querySelectorAll(".checknull");
    var kt = true;
    for (var i = 0; i < input.length; i++) {
        if (input[i].value == "") {
            notify[i].innerText = "Bạn phải điền thông tin này!!!";
            kt = false;
        }
        else {
            notify[i].innerText = "";
        }
    }
    if (kt == true) {
        setTimeout(() => {
            var cf = confirm("Bạn có chắc sẽ mời ứng viên này tham gia phỏng vấn ?!");
            if (cf == true) {
                interviewData();
                setTimeout(wait_for_response, 100);
                $("#thu-moi").css("display", "none");
                $("#wait-for-response").fadeIn(100);
            }
        }, 200);
    }
}

function wait_for_responseData(userName, offerName, time, date, employeeName, address, note, status) {
    if (status == "waiting" || status == "accept" || status == "deny" || status == "fail") {
        var array = document.getElementById("waiting1").children;
        array[0].innerHTML += userName;
        array[1].innerHTML += offerName;
        array[2].innerHTML += time;
        array[3].innerHTML += date;
        array[4].innerHTML += employeeName;
        array[5].innerHTML += address;
        array[6].innerHTML += note;
        $(".steps li:first-child").removeClass("active");
        $(".steps li:nth-child(2)").addClass("active");
        $("#process").text(" Tiến trình: Phỏng vấn");
        $("#main h1").text("Tiến trình phỏng vấn ứng viên : Giai đoạn phỏng vấn");
        console.log("alo");
    }
    if (status == "accept") {
        $("#wait-for-response #status").css("display", "none");
        $("#wait-for-response #result").css("display", "block");
        $("#wait-for-response #accept").css("display","inline-block");
        $("#wait-for-response #deny").css("display", "inline-block");
    }
    if (status == "deny") {
        $("#wait-for-response #status").css("display", "none");
        $("#wait-for-response #denyresult").css("display", "block");
    }
    if (status == "done") {
        $("#interview").css("display", "none");
        $("#elect").css("display", "block");
    }
}

function interviewData() {
    var date = $("#interview-date").val();
    var time = $("#interview-time").val();
    var address = $("#interview-address").val();
    var note = $("#note").val();

    $.ajax({
        data: { userId: userId, offerId: offerId, employeeId: employeeId, date: date, time: time, address: address, note: note },
        url: '/Employee/InterviewData',
        dataType: 'json',
        method: 'POST',
        beforeSend: function () {

        },
        success: function (res) {

        }


    });
}

function wait_for_response() {
    $.ajax({
        data: { userId: userId, offerId: offerId },
        url: '/Employee/checkStatusInterview',
        dataType: 'json',
        method: 'POST',
        beforeSend: function () {
        },
        success: function (res) {
            if (res.having == true) {
                wait_for_responseData(res.User, res.Offer, res.Time, res.Date, res.Employee, res.Address, res.Note, res.Status);
            }
        }
    });
}

function recruit_candidate() {
    $("#interview").fadeOut(100);
    $("#elect").fadeIn(100);
    $("#moi-lam").fadeIn(100);
    $(".steps li:nth-child(2)").removeClass("active");
    $(".steps li:nth-child(3)").addClass("active");
    $("#process").text(" Tiến trình: Mời làm");
    $("#main h1").text("Tiến trình phỏng vấn ứng viên : Giai đoạn mời làm");
}

function workinvitationFormCheck() {
    var input = document.querySelectorAll("#elect input");
    var notify = document.querySelectorAll(".checknull2");
    var kt = true;
    for (var i = 0; i < input.length; i++) {
        if (input[i].value == "") {
            notify[i].innerText = "Bạn phải điền thông tin này!!!";
            kt = false;
        }
        else {
            notify[i].innerText = "";
        }
    }
    if (kt == true) {
        setTimeout(() => {
            var cf = confirm("Bạn có chắc sẽ tuyển ứng viên này ??!");
            if (cf == true) {
                WorkInvitationData();
                setTimeout(wait_for_response2, 100);
                $("#moi-lam").css("display", "none");
                $("#wait-for-response2").fadeIn(100);
            }
        }, 200);
    }
}

function WorkInvitationData() {
    var date = $("#working-date").val();
    var address = $("#working-address").val();
    var salary = $("#salary").val();
    var note = $("#note2").val();

    $.ajax({
        data: { userId: userId, offerId: offerId, date: date, salary: salary, address: address, note: note },
        url: '/Employee/WorkInvitationData',
        dataType: 'json',
        method: 'POST',
        beforeSend: function () {

        },
        success: function (res) {

        }


    });
}

function wait_for_response2Data(userName, offerName, date, salary, address, note, status) {
    if (status == "waiting") {
        var array = document.getElementById("waiting2").children;
        array[0].innerHTML += userName;
        array[1].innerHTML += offerName;
        array[2].innerHTML += date;
        array[3].innerHTML += salary;
        array[4].innerHTML += address;
        array[5].innerHTML += note;
        $(".steps li:first-child").removeClass("active");
        $(".steps li:nth-child(2)").removeClass("active");
        $(".steps li:nth-child(3)").addClass("active");
        $("#process").text(" Tiến trình: Mời làm");
        $("#main h1").text("Tiến trình phỏng vấn ứng viên : Giai đoạn mời làm");
    }
    
    else if (status == "accept") {
        var array = document.getElementById("resultboard").children;
        array[0].innerHTML += userName;
        array[1].innerHTML += offerName;
        array[2].innerHTML += date;
        array[3].innerHTML += salary;
        array[4].innerHTML += address;
        $(".steps li:first-child").removeClass("active");
        $(".steps li:nth-child(3)").removeClass("active");
        $(".steps li:nth-child(4)").addClass("active");
        $("#process").text(" Tiến trình: Kết quả");
        $("#main h1").text("Tiến trình phỏng vấn ứng viên : Kết quả ứng tuyển");
        $("#elect").css("display", "none");
        $("#finalresult").css("display", "block");
    }

    else if (status == "deny") {
        var array = document.getElementById("waiting2").children;
        array[0].innerHTML += userName;
        array[1].innerHTML += offerName;
        array[2].innerHTML += date;
        array[3].innerHTML += salary;
        array[4].innerHTML += address;
        array[5].innerHTML += note;
        console.log("here");
        $(".steps li:first-child").removeClass("active");
        $(".steps li:nth-child(2)").removeClass("active");
        $(".steps li:nth-child(3)").addClass("active");
        $("#process").text(" Tiến trình: Mời làm");
        $("#main h1").text("Tiến trình phỏng vấn ứng viên : Giai đoạn mời làm");
        $("#status2").css("display", "none");
        $("#denyresult2").css("display", "block");
    }
    
}

function wait_for_response2() {
    $.ajax({
        data: { userId: userId, offerId: offerId },
        url: '/Employee/checkStatusWorkInvitation',
        dataType: 'json',
        method: 'POST',
        beforeSend: function () {
        },
        success: function (res) {
            if (res.having == true) {
                wait_for_response2Data(res.User, res.Offer, res.StartDay, res.Salary, res.Address, res.Note, res.Status);
            }
        }
    });
}



function eliminate_candidate() {
    
}


