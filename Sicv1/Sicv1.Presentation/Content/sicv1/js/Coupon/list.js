$(function () {
    $(document).on("click", "#btnVerDetCupon", function () {
        $("#modaldetaCupon").show();
        var image = $(this).attr("data-imagen");
        var title = $(this).attr("data-titulo");
        var desc = $(this).attr("data-desc");
        var condic = $(this).attr("data-condi");
        var price = $(this).attr("data-price");
        var totRating = $(this).attr("data-TotalRating");
     
        $("#objImg").attr("src", image);
        $(".modal-title").text(title);
        $(".modal-description").text(desc);
        $("#lblConditions").text(condic);
        $("#lblPrice").text("S/." + parseFloat(price).toFixed(2));
        $("#lbltotRating").text(parseInt(totRating));



        var categoryId = $(this).attr("data-catID");
        var fill = "";
        for (var i = 1; i <= 5; i++) {
            fill = $("#estrella" + i + "¬" + categoryId).css("fill");
            $("#sm" + i).attr("fill", fill);
        }
    });
    $("#btn-x, #btn-close").on("click", function () {
        $("#modaldetaCupon").hide();
    });
});

function fnlistadoArticles(id = null, title = null, desc = null) {
    var odata = {
        'CompanyId': id,
        'Title': title,
        'Desc': desc
    };

    $.ajax(
        {
            method: "POST",
            url: "../Category/GetCategoriesByCompanyId",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(odata),
            success: function (a) {

                $("#div-list").empty();
                var bgColor = "";
                var isActive = "";
                if (a.length == 0) {
                    $("#objImgAlianza").attr("src", "");
                    return;
                }

                if (a[0].LOGO == "nodata") {
                    $("#objImgAlianza").attr("src", "../Content/images/collage.png");
                    //return;
                }
                else {
                    $("#objImgAlianza").attr("src", a[0].LOGO);
                }

                for (var e = 0; e < a.length; e++) {
                    if (a[e].STATUS == false) { // status=0
                        bgColor = "card-record-disabled";
                        isActive = "";
                    }
                    else {
                        bgColor = ""; //status=1
                        isActive = "btn-toggle-status-on"; //toggle => on
                    }


                    fnDisabledBtnEditWhenIsAllianceUser(a[e].ID);


                    var fn = fnArticles(
                        a[e].ID,
                        a[e].IMAGE_FIRST,
                        a[e].DESCRIPTION,
                        a[e].CONDITIONS,
                        a[e].PRICE,
                        a[e].PERCENTAGE,
                        a[e].TOTAL_RATING,
                        a[e].TITLE,
                        bgColor,
                        isActive,
                        a[e].START_DATE,
                        a[e].END_DATE,
                        a[e].ID_PARENT,
                        a[e].ID_PARENT2,
                        a[e].ID_PARENT3,
                        a[e].ID_COMPANY,
                        a[e].TYPE,
                        a[e].HAVE_ACCESS_SI,
                        a[e].HAVE_ACCESS_NO,
                        a[e].ID_HAVE_ACCESS_SI,
                        a[e].ID_HAVE_ACCESS_NO,
                        a[e].LIFEMILES_PARTICIPATES_CAMPAIGN,
                        a[e].NUMBER_MILES,
                        a[e].COMPANY_HAVE_LIFEMILES,
                        a[e].BARCODE,
                        a[e].BARCODE_FORMAT,
                        a[e].TYPE_CODE,
                        a[e].SEGMENT,
                        a[e].CAT_URL,
                        a[e].URL_LINK

                    );
                    $("#div-list").append(fn);
                }

                for (var i = 0; i < a.length; i++) {
                    var d = parseInt(a[i].SCORE);
                    var r = a[i].SCORE;
                    var n = "orange";

                    switch (d) {
                        case -1:
                            break;
                        case 1:
                            for (var o = 1; o <= r; o++)
                                $("#estrella" + o + "¬" + a[i].ID).css("fill", n);
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            for (o = 1; o <= r; o++)
                                $("#estrella" + o + "¬" + a[i].ID).css("fill", n)
                    }
                }
                $("#loading").removeClass("flex");
            }
        });

}
