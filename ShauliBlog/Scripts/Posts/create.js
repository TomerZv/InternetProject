function VideoCheckbox() {
    if (document.getElementById('video_checkbox').checked) {
        $("#video").removeAttr('disabled');
    }
    else {
        $("#video").attr('disabled', 'disabled');
        $("#video").val("");
    }
}

function ImageCheckbox() {
    if (document.getElementById('image_checkbox').checked) {
        $("#image").removeAttr('disabled');
    }
    else {
        $("#image").attr('disabled', 'disabled');
        $("#image").val("");
    }
}