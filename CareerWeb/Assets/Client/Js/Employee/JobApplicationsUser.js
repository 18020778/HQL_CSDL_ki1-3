var appliedCandidate = $("#tab-appli .candidate-rows");
var savedCandidate = $("#tab-save .candidate-rows");
var displayNum = parseInt(document.getElementById("display").value);

$(document).ready(function () {

    pagelist(displayNum, appliedCandidate);
    pagelist(displayNum, savedCandidate);
    $("#display").change(function () {
        displayNum = parseInt(document.getElementById("display").value);
        pagelist(displayNum, appliedCandidate);
        pagelist(displayNum, savedCandidate);
    })
    $("#search").keyup(function () {
        
        search();
    })
    
})

function actionDetail() {
    var element = $(event.target);
    var userId = element.attr("user");
    var offerId = element.attr("offer");
    var dbParam = "userId=" + userId + "&offerId=" + offerId;
    window.location.href = '/Interview?' + dbParam;
}



function active_tab_appli() {
    var tab_appli = document.getElementById("tab-appli");
    var tab_save = document.getElementById("tab-save");
    if ($("#tab-appli").css('display') == "none") {
        tab_save.classList.remove("tab-active");
        tab_appli.classList.add("tab-active");
        document.getElementById("save-list").classList.remove("list-active");
        document.getElementById("appli-list").classList.add("list-active");
    }

}

function active_tab_save() {
    var tab_appli = document.getElementById("tab-appli");
    var tab_save = document.getElementById("tab-save");
    if ($("#tab-save").css('display') == "none") {
        tab_appli.classList.remove("tab-active");
        tab_save.classList.add("tab-active");
        document.getElementById("appli-list").classList.remove("list-active");
        document.getElementById("save-list").classList.add("list-active");
    }

}

function pagelist(pageSize, candidate) {
    candidate.slice(0, pageSize).css({ display: 'table-row' });
    candidate.slice(pageSize, candidate.length).css({ display: 'none' });

    function addSlice(num) {
        return num + pageSize;
    }

    function subtractSlice(num) {
        return num - pageSize;
    }

    var slice = [0, pageSize];

    $('.next').click(function () {
        if (slice[1] < candidate.length) {
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
        candidate.css('display', 'none');
        candidate.slice(slice[0], slice[1]).css('display', 'table-row');
    }
}

function search() {
    var textSearch = document.getElementById("search").value;
    appliedCandidate = $("#tab-appli .candidate-rows");
    var candidate2 = appliedCandidate.slice(0, appliedCandidate.length);
    if (textSearch) {
        for (var i = 0; i < candidate2.length; i++) {
            var childText = candidate2[i].children;
            kt = false;
            for (var j = 0; j < childText.length; j++) {
                if (childText[j].textContent.indexOf(textSearch) != -1) {
                    console.log(childText[j].textContent);
                    kt = true;
                    break;
                }
            }
            if (kt == false) {
                candidate2[i].remove();
            }
        }
        pagelist(displayNum, candidate2);
        console.log(appliedCandidate.length);
        console.log(candidate2.length);
    }
    
}

function deleteSavedCandidate() {
    var element = $(event.target);
    var userId = element.attr("stt");
    console.log("here");
    $.ajax({
        data: { userId: userId },
        url: '/Employee/DeleteSavedCandidate',
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