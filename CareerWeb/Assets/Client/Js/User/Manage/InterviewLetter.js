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
})