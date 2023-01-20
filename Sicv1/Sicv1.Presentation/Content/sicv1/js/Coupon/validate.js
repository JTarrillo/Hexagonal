$(function () {
    $("#btn-enviar-validarCupon").on("click", function () {
        if ($("#modeSearch").val() === '') {
            fncreateAlert("Seleccione un modo de búsqueda.");
            return
        }
        if ($("#modoSearch").val() === 'Codigo QR') {
            var codToValidate = $("#txt-cod-toValidate").val();
            if (codToValidate == "") { fncreateAlert("Ingrese código a validar"); return; }
            var odata = { "codeQrToValidate": codToValidate };
            $.ajax({
                method: "POST",
                url: "../Category/Validate",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(odata),
                success: function (response) {
                    if (response.status == false && response.data == "NotBelong") {
                        alert('El código del cupón ingresado no pertenece a la alianza');
                        ctrl.val(""); ctrl.focus();
                        return;
                    }

                    if (response.code == 200 && response.status == true) {
                        var vscore = response.data.SCORE;
                        var vpercentage = response.data.PERCENTAGE;
                        $("#objImgCouponDet").attr("src", response.data.IMAGE_FIRST);
                        $("#objImgCouponDet2").attr("src", response.data.IMAGE_FIRST);
                        $("#lblPriceCouponDet").text(response.data.PRICE);
                        $("#lblTitleCouponDet").text(response.data.TITLE);
                        $("#lblConditionsCouponDet").text(response.data.CONDITIONS);
                        $("#lblDescriptionCouponDet").text(response.data.DESCRIPTION);

                        $("#hdf_GetID_CATEGORIES_CODE_QR").val(response.data.ID)

                        var html = "";
                        html += "<svg width='120' viewBox='0 0 94 14'>";
                        html += '<path stroke="var(--orange)" fill="none" id = "estrella1" d = "M7.263,1.444,8.756,4.471a.713.713,0,0,0,.537.39l3.341.485a.713.713,0,0,1,.4,1.216L10.612,8.92a.713.713,0,0,0-.205.631l.571,3.327a.713.713,0,0,1-1.035.752L6.955,12.059a.714.714,0,0,0-.664,0L3.3,13.63a.713.713,0,0,1-1.035-.752l.571-3.327a.713.713,0,0,0-.205-.631L.217,6.563a.713.713,0,0,1,.4-1.216l3.341-.485a.713.713,0,0,0,.537-.39L5.984,1.444A.713.713,0,0,1,7.263,1.444Z" transform = "translate(1 -.5)" />';
                        html += '<path stroke="var(--orange)" fill="none" id = "estrella2" data-name="estrella" d = "M7.263,1.444,8.756,4.471a.713.713,0,0,0,.537.39l3.341.485a.713.713,0,0,1,.4,1.216L10.612,8.92a.713.713,0,0,0-.205.631l.571,3.327a.713.713,0,0,1-1.035.752L6.955,12.059a.714.714,0,0,0-.664,0L3.3,13.63a.713.713,0,0,1-1.035-.752l.571-3.327a.713.713,0,0,0-.205-.631L.217,6.563a.713.713,0,0,1,.4-1.216l3.341-.485a.713.713,0,0,0,.537-.39L5.984,1.444A.713.713,0,0,1,7.263,1.444Z" transform = "translate(20 -.5)" />';
                        html += '<path stroke="var(--orange)" fill="none" id = "estrella3" data-name="estrella" d = "M7.263,1.444,8.756,4.471a.713.713,0,0,0,.537.39l3.341.485a.713.713,0,0,1,.4,1.216L10.612,8.92a.713.713,0,0,0-.205.631l.571,3.327a.713.713,0,0,1-1.035.752L6.955,12.059a.714.714,0,0,0-.664,0L3.3,13.63a.713.713,0,0,1-1.035-.752l.571-3.327a.713.713,0,0,0-.205-.631L.217,6.563a.713.713,0,0,1,.4-1.216l3.341-.485a.713.713,0,0,0,.537-.39L5.984,1.444A.713.713,0,0,1,7.263,1.444Z" transform = "translate(40 -.5)" />';
                        html += '<path stroke="var(--orange)" fill="none" id = "estrella4" data-name="estrella" d = "M7.263,1.444,8.756,4.471a.713.713,0,0,0,.537.39l3.341.485a.713.713,0,0,1,.4,1.216L10.612,8.92a.713.713,0,0,0-.205.631l.571,3.327a.713.713,0,0,1-1.035.752L6.955,12.059a.714.714,0,0,0-.664,0L3.3,13.63a.713.713,0,0,1-1.035-.752l.571-3.327a.713.713,0,0,0-.205-.631L.217,6.563a.713.713,0,0,1,.4-1.216l3.341-.485a.713.713,0,0,0,.537-.39L5.984,1.444A.713.713,0,0,1,7.263,1.444Z" transform = "translate(60 -.5)" />';
                        html += '<path stroke="var(--orange)" fill="none" id = "estrella5" data-name="estrella" d = "M7.263,1.444,8.756,4.471a.713.713,0,0,0,.537.39l3.341.485a.713.713,0,0,1,.4,1.216L10.612,8.92a.713.713,0,0,0-.205.631l.571,3.327a.713.713,0,0,1-1.035.752L6.955,12.059a.714.714,0,0,0-.664,0L3.3,13.63a.713.713,0,0,1-1.035-.752l.571-3.327a.713.713,0,0,0-.205-.631L.217,6.563a.713.713,0,0,1,.4-1.216l3.341-.485a.713.713,0,0,0,.537-.39L5.984,1.444A.713.713,0,0,1,7.263,1.444Z" transform = "translate(80 -.5)" />';
                        html += "</svg>";

                        $("#div-DetCouponValidate").empty();
                        $("#div-DetCouponValidate").append(html);


                        var d = parseInt(response.data.SCORE);
                        var r = response.data.SCORE;
                        var n = "orange";
                        switch (d) {
                            case -1:
                                break;
                            case 1:
                                for (var o = 1; o <= r; o++)
                                    $("#estrella" + o).css("fill", n);
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                for (o = 1; o <= r; o++)
                                    $("#estrella" + o).css("fill", n)
                        }
                        fnopenPanel();
                    }
                    else {
                        alert('El código ingresado no es válido');
                        $("#txt-cod-toValidate").val("");
                        $("#txt-cod-toValidate").focus();
                        //ctrl.val(""); ctrl.focus();
                    }
                }
            });
        } else {
            var codToValidate = $("#txt-cod-toValidate").val();
            if (codToValidate == "") { fncreateAlert("Ingrese documento a buscar"); return; }
            var odata = { "document": codToValidate };
            $.ajax({
                method: "POST",
                url: "../Category/searchByDocument",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(odata),
                success: function (response) {
                    try {
                        if (response[0] === '') {
                            fncreateAlert("El documento ingresado no existe en la base de datos");
                        } else {
                            var div = document.getElementById('list-coupons');
                            var lstCoupons = JSON.parse(response[1]);
                            lstCoupons.forEach((item) => {
                                console.log(item);
                                div.innerHTML += generatecupon(item);
                            })
                            //<div class="list-coupons" style="display: none">

                            //</div>
                        }
                    } catch (ex) {
                        fncreateAlert(ex);
                    }
                }
            });
        }
    });

    $(".list-coupons").on("click", ".confirmarCoupon", function (e) {
        console.log($(e.target).data("id"));
        console.log($(e).data('idcompany'))

        $("#objImgCouponDet").attr("src", $(e.target).data("imagefirst"));
        $("#objImgCouponDet2").attr("src", $(e.target).data("imagefirst"));
        $("#lblPriceCouponDet").text($(e.target).data("price"));
        $("#lblTitleCouponDet").text($(e.target).data("title"));
        $("#lblConditionsCouponDet").text($(e.target).data("conditions"));
        $("#lblDescriptionCouponDet").text($(e.target).data("description"));

        $("#hdf_GetID_CATEGORIES_CODE_QR").val($(e.target).data("id"));

        fnopenPanel();
    })

    function generatecupon(row) {
        return `
            <article class="card-record ">
                <div class="center-v padding">
                    <div class="card-blur filter-st">
                        <img class="blur-image scale-early" src="`+ row.IMAGE_FIRST +`" width="100" height="100">
                            <div class="box-image">
                                <img class="image" src="`+ row.IMAGE_FIRST +`" alt="image">
                        </div>
                            </div>
                            <div class="column padding-l">
                                <p id="div-title" class="text-info bold">`+ row.TITLE +`</p>
                                <p>   <input class="btn btn-orange confirmarCoupon"  id="confirmarCoupon"
                                        data-id="`+ row.ID + `" 
                                        data-idcompany="` + row.ID_COMPANY +`" 
                                        data-imageFirst="` + row.IMAGE_FIRST +`" 
                                        data-price="` + row.PRICE +`" 
                                        data-title="` + row.TITLE + `" 
                                        data-conditions="` + row.CONDITIONS +`" 
                                        data-description="` + row.DESCRIPTION +`" 
                                        type="button" value="Usar" /></p>
                            </div>
                </div>
                        <div class="column">
                            <span id="lblDescripcion" class="padding">
                                <span class="text-info">Descripción</span>
                                <br>
                                   `+ row.DESCRIPTION.substring(0, 80)  +`...
                    </span>
                </div>
                               
            </article>`;
    }

  
});

