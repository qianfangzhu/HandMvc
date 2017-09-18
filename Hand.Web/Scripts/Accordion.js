function ShowMenu(obj, n) {
    var Nav = obj.parentNode;
    if (!Nav.id) {
        var BName = Nav.getElementsByTagName("ol");
        var HName = Nav.getElementsByTagName("h2");
        var t = 2;
    } else {
        var BName = document.getElementById(Nav.id).getElementsByTagName("span");
        var HName = document.getElementById(Nav.id).getElementsByTagName(".header");
        var t = 1;
    }
    for (var i = 0; i < HName.length; i++) {
        HName[i].innerHTML = HName[i].innerHTML.replace("-", "+");
        HName[i].className = "";
    }
    obj.className = "h" + t;
    for (var i = 0; i < BName.length; i++) { if (i != n) { BName[i].className = "no"; } }
    if (BName[n].className == "no") {
        BName[n].className = "";
        obj.innerHTML = obj.innerHTML.replace("+", "-");
    } else {
        BName[n].className = "no";
        obj.className = "";
        obj.innerHTML = obj.innerHTML.replace("-", "+");
    }
}
$(document).ready(function () {

    /* 滑动/展开 */
    $("ul.expmenuu li > div.header").click(function () {

        var arrow = $(this).find("span.arrow");

        if (arrow.hasClass("up")) {
            arrow.removeClass("up");
            arrow.addClass("down");
        } else if (arrow.hasClass("down")) {
            arrow.removeClass("down");
            arrow.addClass("up");
        }
        $(this).parent().find("ul.menu").slideToggle();

    });

});