

var Feedback = {};
Feedback.Show=function (ob) {
    var offset = $(ob).offset()
    var id = $(ob).parents("tr").attr("data-id")
    $(".messageContent").css("top", offset.top + "px")
    $(".messageContent").css("left", offset.left-250 + "px")
    $(".messageContent").removeClass("hidden", 200)
    Feedback.Init($(".messageContent"), id);
}
Feedback.Init = function ($messageContent,id)
{
    $("form", $messageContent)[0].reset();
    $("#Id", $messageContent).val(id);;
    $(".btn-primary",$messageContent).off("click").click(function () {
        $.ajax({
            url: "/System/UpdateFeedback",
            type:"post",
            data:$("form",$messageContent).serialize(),
            success: function (a) {
                $("form[id=SearchForm]").submit();
                $(".messageContent").addClass("hidden", 200)
            }
        })
    })
    $(".btn-cancel", $messageContent).off("click").click(function () {
        $(".messageContent").addClass("hidden",200)
    })

}
