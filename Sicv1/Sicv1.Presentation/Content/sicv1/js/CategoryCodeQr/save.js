
$(function () {
    $("#btnconfirmValidaCupon").on("click", function () {

        //var odata = {
        //    "ID_CATEGORY": $("#hdCategoryId").val(),
        //    "ID_USER": $("#hdUserId").val(),
        //    "CODE_QR": $("#hdQrCode").val(),
        //    "VIA": "WEB",
        //    "CREATED_USER": parseInt(-1)
        //};

        var odata = {
            "p_CODE_QR": $("#txt-cod-toValidate").val(),
            "p_ID": parseInt($("#hdf_GetID_CATEGORIES_CODE_QR").val()),
            "p_TYPE": $("#modeSearch").val()
        };

        $.ajax({
            method: "POST",
            url: "../CategoryCodeQr/Confirm",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(odata),
            success: function (response) {
                if (response.code == 200 && response.status == true) {
                    alert('Cupón validado correctamente');
                    $("#txt-cod-toValidate").val("");
                    fnopenPanel();
                }
            }
        });
    });
});