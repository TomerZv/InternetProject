$(document).ready(function () {
    var canvas = document.getElementById("canvas");
    var ctx = canvas.getContext("2d");
    ctx.font = "12px Comic Sans MS";
    ctx.fillStyle = "red";
    ctx.textAlign = "left";
    ctx.fillText("Eyze Amatzya? Amatzya shelanu?", 0, canvas.height / 2);

    $.ajax({
        url: '/statistics/GetPopularComments',
        type: 'POST',
        contentType: 'application/json'
    }).done(function (posts) {

        jQuery.each(posts, function (index, currPost) {
            var newLine = '<li><a href="/#post ' + currPost.Id + '" >' + currPost.Post + '</a></li>';
            $('#popularList').append(newLine);
        });

    });

});