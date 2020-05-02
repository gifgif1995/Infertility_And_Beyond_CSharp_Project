// function toggle() {
//     var x = document.getElementById("myToggle");
//     if (x.style.display === "none") {
//         x.style.display = "block";
//     } else {
//         x.style.display = "none";
//     }
// }
$(document).ready(function () {
    $("#btntog").click(function () {
        if ($("#tog").is(":hidden")) {
            $("#tog").show(50);
        }
        else {
            $("#tog").hide(50);
        }
    });
    $("#btntog2").click(function () {
        if ($("#tog2").is(":hidden")) {
            $("#tog2").slideDown(50);
        }
        else {
            $("#tog2").slideUp(50);
        }
    });
    $("#btntog3").click(function () {
        if ($("#tog3").is(":hidden")) {
            $("#tog3").slideDown(50);
        }
        else {
            $("#tog3").slideUp(50);
        }
    });

});



