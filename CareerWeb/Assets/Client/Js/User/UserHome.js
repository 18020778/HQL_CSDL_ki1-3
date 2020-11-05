
$(document).ready(function () {
    $(".colClick").click(function () {
        var index = $(".colClick").index(this);
        var listForm = $(".borderColJobMenu form");
        $(listForm[index]).slideToggle();
    })
    $("#clearAll").click(function () {
        location.reload();
    })
    $("#findIcon").click(function () {
        checkName();
        var jobBrowser = $("input[name='jobBrowser']").val();
        var OfferMajor = $('#OfferMajor option[value="' + $('#as').val() + '"]').data('id');
        var listArea = $('#ListArea option[value="' + $('#bs').val() + '"]').data('id');
        var salary = $('#Salary option[value="' + $('#cs').val() + '"]').data('id');
        var positionEmployee = $('#Position option[value="' + $('#ds').val() + '"]').data('id');
        var experience = $('#Experince option[value="' + $('#es').val() + '"]').data('id');
        var levelLearning = $('#Lever option[value="' + $('#fs').val() + '"]').data('id');

        var dbParam = "OfferName=" + jobBrowser + "&Area=" + listArea + "&OfferMajor=" + OfferMajor
            + "&OfferSalary=" + salary + "&PositionJobID=" + positionEmployee + "&ExperienceRequest=" + experience
            + "&LearningLevelRequest=" + levelLearning;
        window.location.href = "/SearchJobForUser?" + dbParam;
    })
    function checkName() {
        var name = $("input[name='jobBrowser']").val();
        var name1 = "";
        var i = 1;
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

        $("input[name='jobBrowser']").val(name1);
    }
})
