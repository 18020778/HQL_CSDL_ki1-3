$(document).ready(function (){


})

function interviewDetail() {

    var element = $(event.target);
    var interviewId = element.attr("interview");
    var dbParam = "interviewId=" + interviewId;
    window.location.href = '/Manage/InterviewLetter?' + dbParam;
    
}