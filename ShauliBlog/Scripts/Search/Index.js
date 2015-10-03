﻿$(document).ready(function () {
    $(".forms").hide();
    $("#posts").show();

    $("#formSelector").change(function () {
        $(".forms").hide();

        var option = $(this).find('option:selected').val();
        $("#" + option).show();
    });
});

function SeniorityCheckbox() {
    if (document.getElementById('seniority_checkbox').checked) {
        $("#seniority").removeAttr('disabled');
        $("#volume").val($("#seniority").val());
    }
    else {
        $("#seniority").attr('disabled', 'disabled');
        $("#volume").val("");
    }
}