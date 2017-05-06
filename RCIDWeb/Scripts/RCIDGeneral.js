$(document).ready(function () {
    $("#successMsgDiv").hide();
    $("#errorMsgDiv").hide();
});


function DisplayResult(response)
{
    if (response.responseText.indexOf("error") == -1) {
        $("#successMsgDiv").text(response.responseText);
        $("#successMsgDiv").alert();
        $("#successMsgDiv").fadeTo(2000, 500).slideUp(500, function () {
            $("#successMsgDiv").slideUp(500);
        });
    } else {
        $("#errorMsgDiv").text(response.responseText);
        $("#errorMsgDiv").alert();
        $("#errorMsgDiv").fadeTo(2000, 500).slideUp(500, function () {
            $("#errorMsgDiv").slideUp(500);
        });       
    }
}