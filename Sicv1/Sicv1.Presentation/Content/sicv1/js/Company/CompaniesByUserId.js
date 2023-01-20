
$(function () {

    $.ajax({
        method: "GET",
        url: "../Company/GetCompaniesByUserId",
        success: function (response) {

            var cbo = $("#cbo-alianzas");
            var cbo2 = $("#cbo-lstCatAdd");
            var cbo3 = $("#cbo-lstCatEdit");
            $.each(response, function () {
                cbo2.append($("<option />").val(this.ID).text(this.NAME));
                cbo3.append($("<option />").val(this.ID).text(this.NAME));
            });

            cbo3.val("--");

            if (response.length <= 2) {
                cbo.empty();

                $.each(response, function () {
                    cbo.append($("<option />").val(this.ID).text(this.NAME));
                    fnlistadoArticles(cbo.val(), $("#txtCouponSearch").val(), $("#txtCouponSearch").val());
                });
            }
            else {
                $(cbo).append('<option value="0" selected="selected">[Seleccione una alianza]</option>');
                fnlistadoArticles(cbo.val(), $("#txtCouponSearch").val(), $("#txtCouponSearch").val());
                $.each(response, function () {
                    cbo.append($("<option />").val(this.ID).text(this.NAME));
                });
            }
        }
    });

    $('#cbo-alianzas').on('change', function () {
        fnlistadoArticles(this.value, $("#txtCouponSearch").val(), $("#txtCouponSearch").val());
    });
    /*
     --------------------------------------------------------------------------
     */

    $('#cbo-lstCatAdd').on('change', function () {
        fnCheckIfHaveLifeMiles($(this).val(), "Add");
    });

    $('#cbo-lstCatEdit').on('change', function () {
        fnCheckIfHaveLifeMiles($(this).val(), "Edit");
    });
    /*                                                                         
     --------------------------------------------------------------------------
     */
})