$(document).ready(function () {


})


function appliedOfferDetail() {
    var element = $(event.target);
    var offerStatus = element.parent().siblings(".status").text();
    var offerId = element.attr("offer");
    var dbParam = "offerId=" + offerId;
    console.log(offerStatus);
    if (offerStatus == "Phỏng vấn" || offerStatus == "Trượt pv") {
        window.location.href = '/Manage/InterviewLetter?' + dbParam;
    }
    if (offerStatus == "Mời làm") {
        window.location.href = '/Manage/InviteWork?' + dbParam;
    }
    if (offerStatus == "Hoàn tất tuyển dụng") {
        window.location.href = '/Manage/Result?' + dbParam;
    }
}