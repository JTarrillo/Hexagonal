$(function () {

    //$("#objImgAlianza").css("display", "none");
    //$("#cbo-alianzas").change(function () {
    //    if ($(this).val() != 0) {
    //        $("#objImgAlianza").css("display", "table");
    //    }
    //    else
    //        $("#objImgAlianza").css("display", "none");
    //});



    $("#btnCouponSearch").on("click", function () {

        if ($("#txtCouponSearch").val() == "") {
            fncreateAlert("Ingrese las primeras letras del título \n\o descripción del cupón que desea buscar");
            return;
        }

        if ($("#txtCouponSearch").val() != "") {
            var title = $("#txtCouponSearch").val();
            var desc = $("#txtCouponSearch").val();
            if (title != "" || title != null) {
                fnlistadoArticles($("#cbo-alianzas").val(), title, "");
                $("#txtCouponSearch").focus();
            }
            else {
                fnlistadoArticles($("#cbo-alianzas").val(), "", desc);
                $("#txtCouponSearch").focus();
            }
        }
    });

    $("#btnCouponSearchCancel").on("click", function () {
        $("#txtCouponSearch").val("");
        var title = $("#txtCouponSearch").val();
        var desc = $("#txtCouponSearch").val();
        if (title != "" || title != null) {
            fnlistadoArticles($("#cbo-alianzas").val(), title, "");
            $("#txtCouponSearch").focus();
        }
        else {
            fnlistadoArticles($("#cbo-alianzas").val(), "", desc);
            $("#txtCouponSearch").focus();
        }
    });
});