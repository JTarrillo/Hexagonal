var v_optLifeMilesAddCoupon = 0;
var v_numMillasAddCoupon = 0;
$(function () {

    $('#txtValPorcentPriceAdd').keyup(function () {

        if ($("#lblValPorcentPriceAdd").text() == "Porcentaje") {
            if ($(this).val() > 100) {
                $(this).val('');
            }
        }
    });

    fndatePicker("#dtpIniAdd");
    fndatePicker("#dtpFinAdd");
    fnvalidateNumberWithDecimals("#txtPriceAdd");
    fnvalidateOnlyNumbers("#txtPercentageAdd");

    $('#cbo-catParents').on('change', function () {
        fnGetCatsChildByParentIdLevel0(this.value,
            "#cbo-catsChildByParentIdLevel0",
            "#cbo-catsChildByParentIdLevel1",
            "../Category/GetCategoriesChildByParentId");

        for (var i = 0; i <= 1; i++) {
            fnShowHide(this, "#cbo-catsChildByParentIdLevel" + i, 1);
        }

    });
    $('#cbo-catsChildByParentIdLevel0').on('change', function () {

        fnGetCatsChildByParentIdLevel1(this.value,
            "#cbo-catsChildByParentIdLevel1",
            "../Category/GetCategoriesChildByParentId");
        fnShowHide(this, "#cbo-catsChildByParentIdLevel1", 6);

    });
    $("#btn-add-coupon").on("click", function ()
        {

        $("#hdPriceAdd").val("");
        $("#hdPorcentAdd").val("");

        fncreatefeedbackToUser("Cargando...");
        $("#loading").addClass("flex");

        fnInitInputSelect("#cbo-catParents", "#cbo-catsChildByParentIdLevel0", "#cbo-catsChildByParentIdLevel1");


        fnCheckIfHaveLifeMiles($("#cbo-lstCatAdd").val());
        $("select#cbo-lstCatAdd").prop('selectedIndex', 0);

        $("#AddCouponModal").show();
        $(".bd-status").addClass('on-modal');

        $("#txtValPorcentPriceAdd").val("");
        $("#inputFileAdd").val("");
        $("#imgModalAdd").attr("src", "");
        $("#hdimgBase64Add").val("");
        $("#txtTitleAdd").val("");
        $("#txtDescriptionAdd").val("");
        $("#txtConditionsAdd").val("");
        $("#txtPriceAdd").val("");
        $("#txtPercentageAdd").val("");

        var d = new Date(), month = d.getMonth() + 1, day = d.getDate();
        var vInitDate = (day < 10 ? '0' : '') + day + '/' + (month < 10 ? '0' : '') + month + '/' + d.getFullYear() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds();
        $("#dtpIniAdd").val(vInitDate);
        $("#dtpFinAdd").val(vInitDate);

        $("#cbo-catParents").empty();
        $("#cbo-catsChildByParentIdLevel0").empty();
        $("#cbo-catsChildByParentIdLevel1").empty();

        setTimeout(function () {
            fnGetParentCategories("#cbo-catParents",
                "#cbo-catsChildByParentIdLevel0",
                "#cbo-catsChildByParentIdLevel1",
                "../Category/GetParentCategories",
                "../Category/GetCategoriesChildByParentId");
        }, 2000);

        $(".editsectionSocialNetwork").remove();//limpia la seccion donde se generaran los obj
        $(".newsectionSocialNetwork").remove();//limpia la seccion donde se generaran los obj

    });




    $("#btn-close-Add").on("click", function () {
        $("#AddCouponModal").hide();
        $(".bd-status").removeClass('on-modal');
    });



    $(document).on("change", "#chkSIparticipatelifesmilesAddCoupon", function () {
        $("#chkSIparticipatelifesmilesAddCoupon").attr("checked", "checked");
        $("#chkNOparticipatelifesmilesEditCoupon").removeAttr("checked");
        v_optLifeMilesAddCoupon = 1;
        $("#txtnumeromillasAddCoupon").val("");
        $("#txtnumeromillasAddCoupon").removeAttr("disabled");
        $("#txtnumeromillasAddCoupon").focus();
    });

    $(document).on("change", "#chkNOparticipatelifesmilesAddCoupon", function () {
        $("#chkNOparticipatelifesmilesAddCoupon").attr("checked", "checked");
        $("#chkSIparticipatelifesmilesAddCoupon").removeAttr("checked");
        v_optLifeMilesAddCoupon = 0;
        $("#txtnumeromillasAddCoupon").val("");
        $("#txtnumeromillasAddCoupon").attr("disabled", "disabled");
    });

    $(document).on("keypress", "#txtnumeromillasAddCoupon", function (e) {
        fnvalidateOnlyNumbers(this);
    });

    $(document).on("change", "#cboCouponTypeAdd", function () {

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