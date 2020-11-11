$(document).ready(function () {
    $("#tim-ho-so").click(function (e) {
        e.preventDefault();
        var area = $('#ListArea option[value="' + $('#diadiem').val() + '"]').data('id');
        var job = $('#OfferMajor option[value="' + $('#linhvuc').val() + '"]').data('id');
        var name = ($("input[name='key-word']").val() != null) ? $("input[name='key-word']").val() : "";
        var experience = ($('#TTKN:selected').val() != null) ? $('#TTKN:selected').val() : 0;
        var salary = ($('#TTML:selected').val() != null) ? $('#TTML:selected').val() : 0;
        var position = ($('#TTCV:selected').val() != null) ? $('#TTCV:selected').val() : 0;
        var levelLearning = ($('#TTLL:selected').val() != null) ? $('#TTLL:selected').val() : 0;
        var dbParam = "&KeyWord=" + name + "&AreaID=" + area + "&JobID=" + job + "&experienceID=" + experience +
            "&salaryID=" + salary + "&positionID=" + position + "&levelLearningID=" + levelLearning;
        window.location.href = "/SearchCandidateResult?" + dbParam;
    })
    $("#tim-nang-cao").click(function () {
        $("#search-more").slideToggle(500);
    })
    $("#xoa-loc").click(function () {
        location.reload();
    })
   

    function checkName() {
        var name = document.getElementById("key-word").value;
        var name1 = "";
        var i = 1;
        name1 = name.charAt(0).toUpperCase();
        if (name.length != 0) {
            name1 += name[0].toUpperCase();
            while (i < name.length) {
                if (name[i] == " " && name[i + 1] == " ") {
                    i++;
                    if (i == name.length) break;
                }
                else if (name[i - 1] == " ") {
                    name1 = name1 + name[i].toUpperCase();
                    i++;
                }
                else {
                    name1 = name1 + name[i];
                    i++;
                }
            }
        }
        document.getElementById("key-word").value = name1;
    }
})