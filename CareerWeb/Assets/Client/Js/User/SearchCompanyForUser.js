$(document).ready(function () {
    $(".colClick").click(function () {
        var index = $(".colClick").index(this);
        var listForm = $(".career-search-filter-toggle ul");
        $(listForm[index]).slideToggle();

    });
    $("#clearAll").click(function () {
        location.reload();
    })
    $("#clearAll").mouseover(function () {
        $("#clearAll").css({ "cursor": "pointer", "color": "red"})
    })
    $("#clearAll").mouseout(function () {
        $("#clearAll").css({"color" : "black"})
    })
    $("#findIcon").mouseover(function () {
        $("#findIcon").css({"cursor": "pointer"})
    })
    $("#findIcon").click(function () {
        checkName();
        var enterprise = $("input[name='enterprise']").val();
        var career = $('#selectedCareer option[value="' + $('#careerBrowser').val() + '"]').data('id');
        var listArea = $('#selectedState option[value="' + $('#areaState').val() + '"]').data('id');
        var size = ($("input[name='selectSize']:checked").val() != null) ? $("input[name='selectSize']:checked").val() : 0;
        var dbParam = "EName=" + enterprise + "&EArea=" + listArea + "&ECareer=" + career
            + "&ESize=" + size;
        window.location.href = "/SearchCompanyForUser?" + dbParam;
    })

    function checkName() {
        var name = $("input[name='enterprise']").val();
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

        $("input[name='enterprise']").val(name1);
    }
})