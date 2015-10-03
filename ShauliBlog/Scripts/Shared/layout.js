$(document).ready(function () {
    var canvas = document.getElementById("canvas");
    var ctx = canvas.getContext("2d");
    ctx.font = "12px Comic Sans MS";
    ctx.fillStyle = "red";
    ctx.textAlign = "left";
    ctx.fillText("Eyze Amatzya? Amatzya shelanu?", 0, canvas.height / 2);
});