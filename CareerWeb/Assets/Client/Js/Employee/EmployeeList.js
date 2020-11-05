var employeelist = $(".employee-rows");
var displayNum = parseInt(document.getElementById("display").value);

$(document).ready(function () {
    pagelist(displayNum, employeelist);
    $("#display").change(function () {
        displayNum = parseInt(document.getElementById("display").value);
        pagelist(displayNum, employeelist);
    })
})

function deleteEmployee() {
    var element = $(event.target);
    var employeeId = element.attr("employee");
    console.log("here");
    $.ajax({
        data: { employeeId: employeeId },
        url: '/Employee/DeleteEmployee',
        dataType: 'json',
        method: 'POST',
        beforeSend: function () {

        },
        success: function (res) {
            if (res.status == true) {
                element.parent().parent().css("display", "none");
            }
            console.log(res.status);
        }
    })
}

function pagelist(pageSize, employee) {
    employee.slice(0, pageSize).css({ display: 'table-row' });
    employee.slice(pageSize, employee.length).css({ display: 'none' });

    function addSlice(num) {
        return num + pageSize;
    }

    function subtractSlice(num) {
        return num - pageSize;
    }

    var slice = [0, pageSize];

    $('.next').click(function () {
        if (slice[1] < employee.length) {
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
        employee.css('display', 'none');
        employee.slice(slice[0], slice[1]).css('display', 'table-row');
    }
}