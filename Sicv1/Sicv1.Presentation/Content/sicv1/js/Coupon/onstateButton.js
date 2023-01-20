function onStateButton(id) {
    document.getElementById(id).classList.toggle('btn-toggle-status-on')
    var vcategoryId = id.replace("btnUpdateStatus", "");

    var odata = "";
    if ($("#" + id).hasClass("btn-toggle-status-on")) {
        odata = {
            "Id": vcategoryId,
            "Status": "True"
        };
        $JSON("POST", "../Category/UpdateCategoriesStatusById", "application/json; charset=utf-8", odata, vcategoryId);
        $("#article" + vcategoryId).removeClass("card-record-disabled");
    }
    else {
        odata = {
            "Id": vcategoryId,
            "Status": "False"
        };
        $JSON("POST", "../Category/UpdateCategoriesStatusById", "application/json; charset=utf-8", odata, vcategoryId);
        $("#article" + vcategoryId).addClass("card-record-disabled");

    }
}

function onRemoveCoupon(id) {
    document.getElementById(id).classList.toggle('btn-toggle-status-on')
    var vcategoryId = id.replace("btnUpdateStatus", "");

    var odata = { "Id": vcategoryId};
    console.log(id)
    $.ajax({
        method: "POST",
        url: "../Category/DeleteCategoriesStatusById",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(odata),
        success: function (response) {
            //if (response.status == status && response.code == 200) {
                window.location.reload();
            //}
        }
    });

    
}

function $JSON(method, url, contentType, data, id) {
    $.ajax({
        method: method,
        url: url,
        contentType: contentType,
        data: JSON.stringify(data),
        success: function (response) {
            if (response.status == status && response.code == 200) {
            }
        }
    });
}