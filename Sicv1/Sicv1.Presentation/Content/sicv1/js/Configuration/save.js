$(function () {
    $.ajax({
        method: "POST",
        url: "../Configuration/GetValuesCurrent",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //data: JSON.stringify(model),
        success: function (response) {
            $(".txtPolicy").val(response.DATA.DATA_PRIVACY_POLICY);
            $(".txtPerson").val(response.DATA.TREATMENT_OF_PERSONAL_DATA);
            $(".txtName").val(response.DATA.NAME);
            $(".txtDescription").val(response.DATA.DESCRIPTION);
            $(".txtUrlLogo").val(response.DATA.URL_LOGO);
            $(".txtVersionOficial").val(response.DATA.URL_OFICIAL);
            $(".txtVersion").val(response.DATA.VERSION);
        }
    });

    $("#btn-save-add").on("click", function () {
        var model = {
            "DATA_PRIVACY_POLICY": $(".txtPolicy").val(),
            "TREATMENT_OF_PERSONAL_DATA": $(".txtPerson").val(),
            "NAME": $(".txtName").val(),
            "DESCRIPTION": $(".txtDescription").val(),
            "URL_LOGO": $(".txtUrlLogo").val(),
            "URL_OFICIAL": $(".txtVersionOficial").val(),
            "VERSION": $(".txtVersion").val()

            
        };
        $.ajax({
            method: "POST",
            url: "../Configuration/UpdateConfiguration",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(model),
            success: function (response) {
                if (response.STATUS) {
                    window.location.reload();
                } else {
                    alert(response.MESSAGE);
                }
            }
        });
    });
});
