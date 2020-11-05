var interviewlist = $(".interview-rows");
var displayNum = parseInt(document.getElementById("display").value);

$(document).ready(function () {
    pagelist(displayNum, interviewlist);
    $("#display").change(function () {
        displayNum = parseInt(document.getElementById("display").value);
        pagelist(displayNum, interviewlist);
    })
})

function actionDetail() {
    var element = $(event.target);
    var userId = element.attr("user");
    var offerId = element.attr("offer");
    var dbParam = "userId=" + userId + "&offerId=" + offerId;
    window.location.href = '/Interview?' + dbParam;
}

function pagelist(pageSize, interview) {
    interview.slice(0, pageSize).css({ display: 'table-row' });
    interview.slice(pageSize, interview.length).css({ display: 'none' });

    function addSlice(num) {
        return num + pageSize;
    }

    function subtractSlice(num) {
        return num - pageSize;
    }

    var slice = [0, pageSize];

    $('.next').click(function () {
        if (slice[1] < interview.length) {
            slice = slice.map(addSlice);
        }
        showSlice(slice);

    });

    $('.prev').click(function () {
        if (slice[0] > 0) {
            slice = slice.map(subtractSlice);
        }
        showSlice(slice);

    });

    function showSlice(slice) {
        interview.css('display', 'none');
        interview.slice(slice[0], slice[1]).css('display', 'table-row');
    }
}