// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $('.btn-bus-tours').popover({
        container: 'body'
    })
});
$(function () {
    $('.btn-tours-abroad__phone').popover({
        container: 'body'
    })
});
$(function () {
    $('.btn-tours-abroad__email').popover({
        container: 'body'
    })
});
$("#click").click(function () {
    $('html, body').animate({
        scrollTop: $("#bus-rent").offset().top
    }, 1000);
});
/*$('#bus-tours__button').click(function () {
    $('#bus-tours__form').css('display', 'block');
    $('.btn-bus-tours').css('color', '#ff5722');
    $('#excursions__form').css('display', 'none');
    $('.btn-excursions').css('color', '#ffffff');
    $('#header__text').text("Поиск туров к морю из Орла").html();
});*/
$('#excursions__button').click(function () {
    $('#excursions__form').css('display', 'block');
    $('.btn-excursions').css('color', '#009688');
    $('#bus-tours__form').css('display', 'none');
    $('.btn-bus-tours').css('color', '#ffffff');
    $('#header__text').text("Поиск Экскурсионных туров из Орла").html();
});
// Tourist collapse
$('#AT').click(function () {
    $(this).addClass("active");
    $('#multiCollapseAT').show();
    $('#multiCollapsePP').hide();
    $('#PP').removeClass("active");
    $('#multiCollapseVP').hide();
    $('#VP').removeClass("active");
    $('#multiCollapsePO').hide();
    $('#PO').removeClass("active");
});
$('#PO').click(function () {
    $(this).addClass("active");
    $('#multiCollapsePO').show();
    $('#multiCollapsePP').hide();
    $('#PP').removeClass("active");
    $('#multiCollapseVP').hide();
    $('#VP').removeClass("active");
    $('#multiCollapseAT').hide();
    $('#AT').removeClass("active");
});
$('#PP').click(function () {
    $(this).addClass("active");
    $('#multiCollapsePP').show();
    $('#multiCollapsePO').hide();
    $('#PO').removeClass("active");
    $('#multiCollapseVP').hide();
    $('#VP').removeClass("active");
    $('#multiCollapseAT').hide();
    $('#AT').removeClass("active");
});
$('#VP').click(function () {
    $(this).addClass("active");
    $('#multiCollapseVP').show();
    $('#multiCollapsePP').hide();
    $('#PP').removeClass("active");
    $('#multiCollapsePO').hide();
    $('#PO').removeClass("active");
    $('#multiCollapseAT').hide();
    $('#AT').removeClass("active");
});
// Tourist collapse end



// Excursion Tour
var description = document.getElementById("description").textContent;
var thePriceInclude = document.getElementById("thePriceInclude").innerHTML;
function FormatDescription(text) {
    text = text.replace(/1 день:/, "<p>1 День:</p>");
    text = text.replace(/2 день:/, "<p>2 День:</p>");
    text = text.replace(/3 день:/, "<p>3 День:</p>");
    text = text.replace(/4 день:/, "<p>4 День:</p>");
    text = text.replace(/5 день:/, "<p>5 День:</p>");
    text = text.replace(/6 день:/, "<p>6 День:</p>");
    text = text.replace(/7 день:/, "<p>7 День:</p>");
    text = text.replace(/8 день:/, "<p>8 День:</p>");
    text = text.replace(/9 день:/, "<p>9 День:</p>");

    document.getElementById("description").innerHTML = text;
}
function FormatThePriceInclude(text) {
    text = text.replace(/;/gi, ";<br> <i class=\"fas fa-check\"></i> ");

    document.getElementById("thePriceInclude").innerHTML = text;
}
FormatDescription(description);
FormatThePriceInclude(thePriceInclude);