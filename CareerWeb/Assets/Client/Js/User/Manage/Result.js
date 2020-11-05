$(document).ready(function () {
    $(".stage:lt(3)").mouseenter(function () {
        var ind = $(this).index();
        $(".stage").eq(ind / 3).css({ "background-color": "#f07e1d", "cursor": "pointer", "color": "white" });
        $(".triangleStageGray").eq(ind / 3).css({ "border-left-color": "#f07e1d" });
    })
    $(".stage:lt(3)").mouseleave(function () {
        var ind = $(this).index();
        $(".stage").eq(ind / 3).css({ "background-color": "rgb(236, 236, 236)", "color": "black" });
        $(".triangleStageGray").eq(ind / 3).css({ "border-left-color": "rgb(236, 236, 236)" });
    })
})