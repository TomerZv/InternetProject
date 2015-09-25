$(document).ready(function () {

    $('.addCommentForm').submit(function(e){

        var currComment = JSON.stringify({
            'PostId': $(this).find('#PostId').val(),
            'Headline': $(this).find('#Headline').val(),
            'Author': $(this).find('#Author').val(),
            'Website': $(this).find('#Website').val(),
            'Content': $(this).find('#Content').val()
        });
        
        $.ajax({
            url: '/comments/create',
            type: 'POST',
            contentType: 'application/json',
            data: currComment
        }).done(function (response) {
            
            var reG = /-?\d+/;
            var m = reG.exec(response.Timestamp);
            var date = new Date(parseInt(m[0])).toLocaleString('fr-FR', { hour12: false });
           
            var newComment ="<article> " +
                                "<header>" +
                               //   "<h4>" + response.Headline + "</h4>"  +
                                    "<a href=\"" + response.Website + "\">" + response.Author + "</a> on <time>" + date + "</time>" +
                                 "</header>" +
                                 "<p>" + response.Content + "</p>" +
                            "</article>";

            $("section[id='comments " + response.PostId + "']").append(newComment);
        });

        $(this).find('#Headline').val('');
        $(this).find('#Author').val('');
        $(this).find('#Website').val('');
        $(this).find('#Content').val('');

        e.preventDefault();
    });
    

});