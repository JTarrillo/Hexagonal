var v_optLifeMilesEditCoupon;
var v_numMillasEditCoupon; 

var dataLIFEMILES = 0;
var dataMILLAS = 0;

function fnOnChangeRadioButtons() {
    $(document).on("change", "#chkSIparticipatelifesmilesEditCoupon", function () {
        $("#chkNOparticipatelifesmilesEditCoupon").prop("checked", false);
        //v_optLifeMilesEditCoupon = 1;
        $("#txtnumeromillasEditCoupon").val("");
        $("#txtnumeromillasEditCoupon").removeAttr("disabled");
        $("#txtnumeromillasEditCoupon").focus();
    });

    $(document).on("change", "#chkNOparticipatelifesmilesEditCoupon", function () {
        $("#chkSIparticipatelifesmilesEditCoupon").prop("checked", false);
        //v_optLifeMilesEditCoupon = 0;
        $("#txtnumeromillasEditCoupon").val("");
        $("#txtnumeromillasEditCoupon").attr("disabled", "disabled");
    });
}

$(function () {

    fnOnChangeRadioButtons();
    //fnEdit();
    fnUpdate();
    fnCboOnChange();
    fnbtnClose();

    $('#txtValPorcentPrice').keyup(function () {

        if ($("#lblValPorcentPrice").text() == "Porcentaje") {
            if ($(this).val() > 100) {
                $(this).val('');
            }
        }
    });

    $(document).on("keypress", "#txtnumeromillasEditCoupon", function (e) {
        fnvalidateOnlyNumbers(this);
    });

    $(document).on("change", "#cboCouponTypeEdit", function () {

        const type = $(this).val();
        if (type == "URL") {
            $(".section-qr").addClass('d-none');
            $(".section-link").removeClass('d-none');
            return;
        }

        $(".section-link").addClass('d-none');
        $(".section-qr").removeClass('d-none');


    })

});

function fnCboOnChange() {
    document.getElementById("cbo-catParents-edit").onchange = function (e) {
        if (e.target.options.selectedIndex > 0) {
            $("#cbo-catsChildByParentIdLevel-edit0").css("display", "none");
            $("#cbo-catsChildByParentIdLevel-edit1").css("display", "none");
        }
        else {
            $("#cbo-catsChildByParentIdLevel-edit0").css("display", "table");
            $("#cbo-catsChildByParentIdLevel-edit1").css("display", "table");
        }
    };
    document.getElementById("cbo-catsChildByParentIdLevel-edit0").onchange = function (e) {
        if (e.target.options.selectedIndex > 0) {
            $("#cbo-catsChildByParentIdLevel-edit1").css("display", "none");
        }
        else {
            $("#cbo-catsChildByParentIdLevel-edit1").css("display", "table");
        }
    };
}

//function fnEdit() {

    //$(document).on("click", "#btnEditCoupon", function () {

        //var $idTieneTarjeta = $(this).attr("data-idSinAccesoSi");
        //var $idNoTieneTarjeta = $(this).attr("data-idSinAccesoNo");

        //$("#hdidTieneTarjeta").val($idTieneTarjeta);
        //$("#hdidNoTieneTarjeta").val($idNoTieneTarjeta);

        //var $tieneTarjeta = $(this).attr("data-accessTrue");
        //var $noTieneTarjeta = $(this).attr("data-accessFalse");

        //if ($tieneTarjeta == 'true') { $("#chkcardSi_EditCupon").attr("checked", "checked"); }
        //else { $("#chkcardNo_EditCupon").attr("checked", "checked"); }

        //if ($noTieneTarjeta == 'true') { $("#chknocardSi_EditCupon").attr("checked", "checked"); }
        //else { $("#chknocardNo_EditCupon").attr("checked", "checked"); }

        //var CouponType = $(this).attr("data-typeCoupon");
        //if (CouponType == "" || CouponType == -1) { $("#cboCouponTypeEdit").val(-1); }
        //else { $("#cboCouponTypeEdit").val(CouponType); }

        //$("#hdPriceEdit").val("");
        //$("#hdPorcentEdit").val("");

        ////activamos animación 'cargando...' y le ponemos el texto que queremos.
        //fncreatefeedbackToUser("Cargando...");
        //$("#loading").addClass("flex");

        ////Obteniendo los idParents y childs de los cupones
        //var articleId = "#" + $("#article" + $(this).attr("data-catID")).attr("id");
        //var vparent1 = $(articleId).attr("data-idparent1");
        //var vparent2 = $(articleId).attr("data-idparent2");
        //var vparent3 = $(articleId).attr("data-idparent3");

        //$("#hdStatusOriginalImage").val(0);
        //var dataLIFEMILES = $(this).attr("data-lifeMiles");
        //var dataMILLAS = $(this).attr("data-numMillas");
        //if (dataLIFEMILES == "1") {
        //    $("#chkSIparticipatelifesmilesEditCoupon").prop("checked", true);
        //    $("#chkNOparticipatelifesmilesEditCoupon").prop("checked", false);
        //    $("#txtnumeromillasEditCoupon").prop("disabled", false);
        //    $("#txtnumeromillasEditCoupon").val(dataMILLAS);
        //}
        //else {
        //    $("#chkNOparticipatelifesmilesEditCoupon").prop("checked", true);
        //    $("#chkSIparticipatelifesmilesEditCoupon").prop("checked", false);
        //    $("#txtnumeromillasEditCoupon").prop("disabled", true);
        //    $("#txtnumeromillasEditCoupon").val("");
        //}

        //if ($(this).attr("data-companyHaveLifeMiles") == "0")
        //{
        //    $("#chkNOparticipatelifesmilesEditCoupon").prop("checked", true);
        //    $("#chkSIparticipatelifesmilesEditCoupon").prop("checked", false);
            
        //    $("#chkSIparticipatelifesmilesEditCoupon").prop("disabled", true);
        //    $("#chkNOparticipatelifesmilesEditCoupon").prop("disabled", true)

        //    $("#txtnumeromillasEditCoupon").val("");
        //    $("#txtnumeromillasEditCoupon").prop("disabled", true);
        //}   

        ////abrimos el modal
        //$("#EditCouponModal").show();

        


        //fnInitInputSelect("#cbo-catParents-edit", "#cbo-catsChildByParentIdLevel-edit0", "#cbo-catsChildByParentIdLevel-edit1");

        ////cargo los input selects, pero no muestro la data para después poder cargarlos según el parentId
        //fnGetParentCategories_edit("#cbo-catParents-edit",
        //    "#cbo-catsChildByParentIdLevel-edit0", "#cbo-catsChildByParentIdLevel-edit1",
        //    "../Category/GetParentCategories", "../Category/GetCategoriesChildByParentId", null, null, null);

        //fnloadParentsId(vparent1, vparent2, vparent3);

        //$(".bd-status").addClass('on-modal');
        //$("#inputFile").val("");
        //$("#imgModal").attr("src", $(this).attr("data-imagen"));
        //$("#hdimgBase64").val("");

        ////Si no has elegido una imagen nueva entonces se queda la que está.
        //var dominio = "https://imagesoncobenefits.s3-sa-east-1.amazonaws.com";
        //$("#hdimgBase64").val($(this).attr("data-imagen").replace(dominio, ""));

        //$("#dtpIniEdit").val("");
        //$("#dtpFinEdit").val("");
        //$("#hdCategoryId").val($(this).attr("data-catID"));
        //$("#txtTitle").val($(this).attr("data-titulo"));
        //$("#txtDescription").val($(this).attr("data-desc"));
        //$("#txtConditions").val($(this).attr("data-condi"));

        //if ($(this).attr("data-price") == 0 || $(this).attr("data-price") == "" || $(this).attr("data-price") == null) {
        //    $("#txtValPorcentPrice").val($(this).attr("data-percent"));
        //    $("#toggle-status").addClass("toggle-status-on");
        //    $("#lblValPorcentPrice").text("Porcentaje");
        //}

        //if ($(this).attr("data-percent") == 0 || $(this).attr("data-percent") == "" || $(this).attr("data-percent") == null) {
        //    $("#txtValPorcentPrice").val($(this).attr("data-price"));
        //    $("#toggle-status").removeClass("toggle-status-on");
        //    $("#lblValPorcentPrice").text("Precio");
        //}

        //$("#dtpIniEdit").val(fngetformatDate($(this).attr("data-startDate")));
        //$("#dtpFinEdit").val(fngetformatDate($(this).attr("data-endDate")));
        //$("#cbo-lstCatEdit").val($(this).attr("data_id_company"));

        //$("#estrella1").attr("fill");
        //$("#hdstar1").val($("#estrella1¬" + $("#hdCategoryId").val()).css("fill"));
        //$("#hdstar2").val($("#estrella2¬" + $("#hdCategoryId").val()).css("fill"));
        //$("#hdstar3").val($("#estrella3¬" + $("#hdCategoryId").val()).css("fill"));
        //$("#hdstar4").val($("#estrella4¬" + $("#hdCategoryId").val()).css("fill"));
        //$("#hdstar5").val($("#estrella5¬" + $("#hdCategoryId").val()).css("fill"));

        //var { yearNow, monthNow, dayNow, hourNow, minNow, secNow, millsecNow } = fnSetFileName();
        //var vfileName = $("#hdCategoryId").val() + '' + '_cupon_' + yearNow + '' + parseInt(monthNow + 1) + '' + dayNow + '' + hourNow + '' + minNow + '' + secNow + '' + millsecNow;
        //$("#hdFileName").val(vfileName);
    //});
//}

function fnloadParentsId(vparent1, vparent2, vparent3) {
    setTimeout(function () {
        if (vparent1 != "" && vparent2 != "" && vparent3 != "") {
            $("#cbo-catParents-edit").val(vparent3);
            $("#cbo-catsChildByParentIdLevel-edit0").val(vparent2);
            $("#cbo-catsChildByParentIdLevel-edit1").val(vparent1);
            //quitamos loading... de una vez que tenemos los parents correctos
            $("#loading").removeClass("flex");
        }
    }, 3000);
    setTimeout(function () {
        if (vparent1 != "" && vparent2 != "" && vparent3 == "-1") {
            $("#cbo-catParents-edit").val(vparent2);
            $("#cbo-catsChildByParentIdLevel-edit0").val(vparent1);
            $("#cbo-catsChildByParentIdLevel-edit1").css("display", "none");
            $("#loading").removeClass("flex");
        }
    }, 3000);
    setTimeout(function () {
        if (vparent1 != "" && vparent2 == "-1" && vparent3 == "-1") {
            $("#cbo-catParents-edit").val(vparent1);
            $("#cbo-catsChildByParentIdLevel-edit0").css("display", "none");
            $("#cbo-catsChildByParentIdLevel-edit1").css("display", "none");
            $("#loading").removeClass("flex");
        }
    }, 3000);
}

function fnbtnClose() {
    $("#btn-x, #btn-close").on("click", function () {
        $("#EditCouponModal").hide();
        $(".bd-status").removeClass('on-modal');
    });
}