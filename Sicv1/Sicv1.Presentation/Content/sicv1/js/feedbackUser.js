function fncreatefeedbackToUser(text,v=0) {
    if (v == 1) {
        $("#loading").addClass("master");
        var html = '<div class="icon-loading">';
        html += '<h1 class="center-h f-8 text-h1-loader">' + 'Sic.' + '</h1>';
        html += '</div>';
        $("#loading").empty();
        $("#loading").append(html);
    } else {
        $("#loading").removeClass("master");
        var html = '<div class="icon-loading">';
        html += '<span class="text-white center-h f-8 ">' + text + '</span>';
        html += '</div>';
        $("#loading").empty();
        $("#loading").append(html);
    }
    
}