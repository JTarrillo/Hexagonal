function fnArticles(id = "", imagePath, desc, condi,
    price, perc, rating, title,
    bgcolor = "", isActive = "", startDate = "", endDate = "",
    id_parent, id_parent2, id_parent3, id_company = null, type = null,
    sin_accesoSi = null, sin_accesoNo = null,
    id_sin_accesoSi = null, id_sin_accesoNo = null,
    lifesMiles = null, numMillas = null, companyHaveLifeMiles = null, barcode = '', barcode_format = '', type_code = 'dynamic', segment = '', cat_url = '',
    url_link = ''
) {


    var idBtnToggle = "btnUpdateStatus" + id;
    var idBtnEdit = "btnEditCoupon" + id;

    var popConfirm = `
        <div class="io-popconfirm bottom-right">
            <div class="io-mask" onclick="removeTooltip()"></div>
            <div class='box-icon' title='Eliminar' onclick="openTooltip(this)"><i class='fas fa-trash-alt ifas-icon' style='background: var(--pinkGrad); color: #fff'></i></div>
            <div class="io-popover" style="width: 130px">
                <div class="io-popover-message">
                    <svg style="margin-right: .4rem" viewBox="64 64 896 896"  width="1em" height="1em" fill="var(--Yellow)">
                        <path d="M512 64C264.6 64 64 264.6 64 512s200.6 448 448 448 448-200.6 448-448S759.4 64 512 64zm-32 232c0-4.4 3.6-8 8-8h48c4.4 0 8 3.6 8 8v272c0 4.4-3.6 8-8 8h-48c-4.4 0-8-3.6-8-8V296zm32 440a48.01 48.01 0 010-96 48.01 48.01 0 010 96z"></path>
                    </svg>
                    <div class="io-popover-message-title">¿Está seguro?</div>
                </div>
                <div class="io-popover-buttons">
                    <button type="button" class="io-btn gosth small" style="margin-right: .4rem" onclick="removeTooltip()">No</button>
                    <button type="button" class="io-btn gosth small blue" id=`+ id + `  onclick="onRemoveCoupon(this.id)">Si</button>
                </div>
            </div>
        </div>`;

    var html = "";
    html += "<article id=article" + id + " class='card-record " + bgcolor + "'  data-idparent1='" + id_parent + "' data-idparent2='" + id_parent2 + "' data-idparent3='" + id_parent3 + "'>" ;
    html += "<div class='center-v padding'>";
    html += " <div class='card-blur filter-st'>";
    html += "<img class='blur-image scale-early' src=" + imagePath + " width='100' height='100' />";
    html += "<div class='box-image'>";
    html += "<img class='image' src=" + imagePath + " alt='image' />";
    html += "</div>";
    html += "</div>";
    html += "<div class='column padding-l'>";
    html += "<span class='text-info'>Título</span><p id='div-title' class='text-info bold'>" + title + "</p>";
    html += "<svg width='120' viewBox='0 0 94 14'>";
    html += '<path stroke="var(--orange)" fill="none" id = "estrella1¬' + id + '" d = "M7.263,1.444,8.756,4.471a.713.713,0,0,0,.537.39l3.341.485a.713.713,0,0,1,.4,1.216L10.612,8.92a.713.713,0,0,0-.205.631l.571,3.327a.713.713,0,0,1-1.035.752L6.955,12.059a.714.714,0,0,0-.664,0L3.3,13.63a.713.713,0,0,1-1.035-.752l.571-3.327a.713.713,0,0,0-.205-.631L.217,6.563a.713.713,0,0,1,.4-1.216l3.341-.485a.713.713,0,0,0,.537-.39L5.984,1.444A.713.713,0,0,1,7.263,1.444Z" transform = "translate(1 -.5)" />';
    html += '<path stroke="var(--orange)" fill="none" id = "estrella2¬' + id + '" data - name="estrella" d = "M7.263,1.444,8.756,4.471a.713.713,0,0,0,.537.39l3.341.485a.713.713,0,0,1,.4,1.216L10.612,8.92a.713.713,0,0,0-.205.631l.571,3.327a.713.713,0,0,1-1.035.752L6.955,12.059a.714.714,0,0,0-.664,0L3.3,13.63a.713.713,0,0,1-1.035-.752l.571-3.327a.713.713,0,0,0-.205-.631L.217,6.563a.713.713,0,0,1,.4-1.216l3.341-.485a.713.713,0,0,0,.537-.39L5.984,1.444A.713.713,0,0,1,7.263,1.444Z" transform = "translate(20 -.5)" />';
    html += '<path stroke="var(--orange)" fill="none" id = "estrella3¬' + id + '" data - name="estrella" d = "M7.263,1.444,8.756,4.471a.713.713,0,0,0,.537.39l3.341.485a.713.713,0,0,1,.4,1.216L10.612,8.92a.713.713,0,0,0-.205.631l.571,3.327a.713.713,0,0,1-1.035.752L6.955,12.059a.714.714,0,0,0-.664,0L3.3,13.63a.713.713,0,0,1-1.035-.752l.571-3.327a.713.713,0,0,0-.205-.631L.217,6.563a.713.713,0,0,1,.4-1.216l3.341-.485a.713.713,0,0,0,.537-.39L5.984,1.444A.713.713,0,0,1,7.263,1.444Z" transform = "translate(40 -.5)" />';
    html += '<path stroke="var(--orange)" fill="none" id = "estrella4¬' + id + '" data - name="estrella" d = "M7.263,1.444,8.756,4.471a.713.713,0,0,0,.537.39l3.341.485a.713.713,0,0,1,.4,1.216L10.612,8.92a.713.713,0,0,0-.205.631l.571,3.327a.713.713,0,0,1-1.035.752L6.955,12.059a.714.714,0,0,0-.664,0L3.3,13.63a.713.713,0,0,1-1.035-.752l.571-3.327a.713.713,0,0,0-.205-.631L.217,6.563a.713.713,0,0,1,.4-1.216l3.341-.485a.713.713,0,0,0,.537-.39L5.984,1.444A.713.713,0,0,1,7.263,1.444Z" transform = "translate(60 -.5)" />';
    html += '<path stroke="var(--orange)" fill="none" id = "estrella5¬' + id + '" data - name="estrella" d = "M7.263,1.444,8.756,4.471a.713.713,0,0,0,.537.39l3.341.485a.713.713,0,0,1,.4,1.216L10.612,8.92a.713.713,0,0,0-.205.631l.571,3.327a.713.713,0,0,1-1.035.752L6.955,12.059a.714.714,0,0,0-.664,0L3.3,13.63a.713.713,0,0,1-1.035-.752l.571-3.327a.713.713,0,0,0-.205-.631L.217,6.563a.713.713,0,0,1,.4-1.216l3.341-.485a.713.713,0,0,0,.537-.39L5.984,1.444A.713.713,0,0,1,7.263,1.444Z" transform = "translate(80 -.5)" />';
    html += "</svg>";
    html += " </div>";
    html += "</div>";
    html += "<div class='column'>";
    html += "<span id='lblDescripcion' class='padding'><span class='text-info'>Descripción</span><br/>";
    html += desc.substring(0, 75) + "...";
    html += "</span>";
    html += "<span id='div-condiciones' class='padding'><span class='text-info'>Condiciones</span><br/>";
    html += condi.substring(0, 75) + "...";
    html += "</span>";
    html += "</div>";
    html += "<div class='box-actions'>";
    html += "<div data-imagen='" + imagePath + "' data-catID='" + id + "' data-catID='" + id + "' data-titulo='" + title + "' data-desc='" + desc + "' data-condi='" + condi + "' data-price='" + price + "' data-percent='" + perc + "' class='btn-toggle-status box-icon " + isActive + "' id='" + idBtnToggle + "'  onclick='onStateButton(this.id)'><span class='cicle-toggle'></span></div>";
        //html += "<div data-imagen='" + imagePath + "' data-catID='" + id + "' data-titulo='" + title + "' data-desc='" + desc + "' data-condi='" + condi + "' data-price='" + price + "' data-percent='" + perc + "' class='btn-toggle-status box-icon " + isActive + "' id='" + idBtnToggle + "'  onclick='onStateButton(this.id)'><span class='cicle-toggle'></span></div>";
    html += "<div class='center-v'>";
    html += "<div class='box-icon' data-catID='" + id + "' data-TotalRating='" + rating + "' data-price='" + price + "' data-condi='" + condi + "' data-desc='" + desc + "' data-titulo='" + title + "' data-imagen='" + imagePath + "' id='btnVerDetCupon' title='Seguir leyendo'><i class='fas fa-eye ifas-icon'></i> </div>";
    html += "<div class='box-icon' data-barcode='" + barcode + "' data-segment='" + segment + "' data-barcode_format='" + barcode_format + "' data-startDate='" + startDate + "' data-endDate='" + endDate + "' data-percent='" + perc + "' data-price='" + price + "' data-typecode='" + type_code + "' title='Editar' data-desc='" + desc + "' data-condi='" + condi + "' data-titulo='" + title + "' data-imagen='" + imagePath + "' data-catID='" + id + "' data_idparent='" + id_parent + "' data_idparent2='" + id_parent2 + "' data_idparent3='" + id_parent3 + "' data_id_company='" + id_company + "' data-typeCoupon='" + type + "' data-accessTrue=" + sin_accesoSi + " data-accessFalse=" + sin_accesoNo + "  data-idSinAccesoSi='" + id_sin_accesoSi + "' data-idSinAccesoNo='" + id_sin_accesoNo + "'  data-lifeMiles='" + lifesMiles + "' data-numMillas='" + numMillas + "' data-companyHaveLifeMiles='" + companyHaveLifeMiles + "' data-cat_url='" + cat_url +"' data-url_link='"+ url_link +"'  id='" + idBtnEdit + "' onclick='fnOnClickBtnEdit(this);'><i class='far fa-edit ifas-icon'></i></i> </div>";
    html += popConfirm;
    html += "</div>";
    html += "</div>";
    html += "</article>";
    return html;
}


//function fnDisabledBtnEditWhenIsAllianceUser(id) {
//    var id = $(id).attr("id");
//    alert(id);
//}

function fnOnClickBtnEdit(id) {
    var id = $(id).attr("id");
    $(document).on("click", "#" + id, function () {

        var $idTieneTarjeta = $(this).attr("data-idSinAccesoSi");
        var $idNoTieneTarjeta = $(this).attr("data-idSinAccesoNo");

        $("#hdidTieneTarjeta").val($idTieneTarjeta);
        $("#hdidNoTieneTarjeta").val($idNoTieneTarjeta);

        var $tieneTarjeta = $(this).attr("data-accessTrue");
        var $noTieneTarjeta = $(this).attr("data-accessFalse");

        if ($tieneTarjeta == 'true') { $("#chkcardSi_EditCupon").attr("checked", "checked"); }
        else { $("#chkcardNo_EditCupon").attr("checked", "checked"); }

        if ($noTieneTarjeta == 'true') { $("#chknocardSi_EditCupon").attr("checked", "checked"); }
        else { $("#chknocardNo_EditCupon").attr("checked", "checked"); }

        var CouponType = $(this).attr("data-typeCoupon");
        if (CouponType == "" || CouponType == -1) {
            $("#cboCouponTypeEdit").val(-1);
        }
        else {
            $("#cboCouponTypeEdit").val(CouponType);
            if (CouponType == "URL") {
                $(".section-qr").addClass('d-none');
                $(".section-link").removeClass('d-none');
            } else {
                $(".section-link").addClass('d-none');
                $(".section-qr").removeClass('d-none');
            }
        }

        $("#hdPriceEdit").val("");
        $("#hdPorcentEdit").val("");

        //activamos animación 'cargando...' y le ponemos el texto que queremos.
        fncreatefeedbackToUser("Cargando...");
        $("#loading").addClass("flex");

        //Obteniendo los idParents y childs de los cupones
        var articleId = "#" + $("#article" + $(this).attr("data-catID")).attr("id");
        var vparent1 = $(articleId).attr("data-idparent1");
        var vparent2 = $(articleId).attr("data-idparent2");
        var vparent3 = $(articleId).attr("data-idparent3");

        $("#hdStatusOriginalImage").val(0);
        var dataLIFEMILES = $(this).attr("data-lifeMiles");
        var dataMILLAS = $(this).attr("data-numMillas");
        if (dataLIFEMILES == "1") {
            $("#chkSIparticipatelifesmilesEditCoupon").prop("checked", true);
            $("#chkNOparticipatelifesmilesEditCoupon").prop("checked", false);
            $("#txtnumeromillasEditCoupon").prop("disabled", false);
            $("#txtnumeromillasEditCoupon").val(dataMILLAS);
        }
        else {
            $("#chkNOparticipatelifesmilesEditCoupon").prop("checked", true);
            $("#chkSIparticipatelifesmilesEditCoupon").prop("checked", false);
            $("#txtnumeromillasEditCoupon").prop("disabled", true);
            $("#txtnumeromillasEditCoupon").val("");
        }

        if ($(this).attr("data-companyHaveLifeMiles") == "0") {
            $("#chkNOparticipatelifesmilesEditCoupon").prop("checked", true);
            $("#chkSIparticipatelifesmilesEditCoupon").prop("checked", false);

            $("#chkSIparticipatelifesmilesEditCoupon").prop("disabled", true);
            $("#chkNOparticipatelifesmilesEditCoupon").prop("disabled", true)

            $("#txtnumeromillasEditCoupon").val("");
            $("#txtnumeromillasEditCoupon").prop("disabled", true);
        }

        var segment = $(this).attr("data-segment");
        console.log(segment)
        $("#chk1").prop("checked", false)
        $("#chk2").prop("checked", false)
        $("#chk3").prop("checked", false)
        $("#chk4").prop("checked", false)
        $("#chk5").prop("checked", false)
        $("#chk6").prop("checked", false)      
        if (segment) {
            var json = JSON.parse(segment)
            console.log(json)
            if (json) {
                json.map(x => {
                    switch (x.ID) {
                        case "1":
                        case 1:
                            $("#chk1").prop("checked", true)
                            break;
                        case "2":
                        case 2:
                            $("#chk2").prop("checked", true)
                            break;
                        case "3":
                        case 3:
                            $("#chk3").prop("checked", true)
                            break;
                        case "4":
                        case 4:
                            $("#chk4").prop("checked", true)
                            break;
                        case "5":
                        case 5:
                            $("#chk5").prop("checked", true)
                            break;
                        case "6":
                        case 6:
                            $("#chk6").prop("checked", true)
                            break;
                    }
                })
            }
        }

        var cat_url = $(this).attr("data-cat_url");

        const udpframeSocialNetwork = document.getElementsByClassName('udpframeSocialNetwork')[0];

        const udpsectionSocialNetwork = udpframeSocialNetwork.children[0];

        $(".editsectionSocialNetwork").remove();//limpia la seccion donde se generaran los obj

        if (cat_url != "") {
            let url_cat = JSON.parse(cat_url);

            for (var x = 0; x < Object.keys(url_cat).length; x++) {
                console.log(url_cat[x]);


                var obj = document.createElement("div");
                obj.className = 'row editsectionSocialNetwork';


                var html = '<div class="col-4 ant-form-group">';
                html += '<span>Tipo:*</span>';
                html += '<select id="TypeUrl" class="input TypeUrl" onchange="selectTypeUrl(this)">';
                html += '<option value="-1">[Seleccionar]</option>';
                html += '<option value="1" ' + (url_cat[x].ICON != "" ? "selected" : "") + '>RED SOCIAL</option>';
                html += '<option value="2" ' + (url_cat[x].ICON == "" ? "selected" : "") + '>WEB</option>';
                html += '</select>';
                html += '<center><buttom class="btn btn-pink" onclick="removeSocialNetwork(this)">Remover</buttom></center>';
                html += '</div>';
                html += '<div class="col-8">';
                html += '<div class="ant-form-group sltSocial" style="display: ' + (url_cat[x].ICON != "" ? "grid" : "none") + ';">';
                html += '<span>Icono:</span>';
                html += '<select id="TypeSocialNetwork" class="input TypeSocialNetwork" onchange="">';
                html += '<option value="-1">[Seleccionar]</option>';
                html += '<option value="1" ' + (url_cat[x].TITLE == "Facebook" ? "selected" : "") + '>Facebook</option>';
                html += '<option value="2" ' + (url_cat[x].TITLE == "Instagram" ? "selected" : "") + '>Instagram</option>';
                html += '<option value="3" ' + (url_cat[x].TITLE == "TikTok" ? "selected" : "") + '>Tiktok</option>';
                html += '</select>';
                html += '</div>';
                html += '<div class="ant-form-group iptTitle" style="display: ' + (url_cat[x].ICON == "" ? "grid" : "none") + ';">';
                html += '<span>Titulo:</span>';
                html += '<input id="titleWeb" value="' + url_cat[x].TITLE + '" class="input titleWeb" type="text" required>';
                html += '</div>';
                html += '<div class="ant-form-group iptUrl">';
                html += '<span>URL:</span>';
                html += '<input id="urlNetwork" value="' + url_cat[x].URL + '" class="input urlNetwork" type="text" required>';
                html += '</div>';
                html += '</div>';
                html += '<hr />';
                obj.innerHTML = html;
                udpsectionSocialNetwork.append(obj);

            }
        }

       

        $("#idCouponHidden").val(id.replace("btnEditCoupon",""));
        //abrimos el modal
        $("#EditCouponModal").show();




        fnInitInputSelect("#cbo-catParents-edit", "#cbo-catsChildByParentIdLevel-edit0", "#cbo-catsChildByParentIdLevel-edit1");

        //cargo los input selects, pero no muestro la data para después poder cargarlos según el parentId
        fnGetParentCategories_edit("#cbo-catParents-edit",
            "#cbo-catsChildByParentIdLevel-edit0", "#cbo-catsChildByParentIdLevel-edit1",
            "../Category/GetParentCategories", "../Category/GetCategoriesChildByParentId", null, null, null);

        fnloadParentsId(vparent1, vparent2, vparent3);

        $(".bd-status").addClass('on-modal');
        $("#inputFile").val("");
        $("#imgModal").attr("src", $(this).attr("data-imagen"));
        $("#hdimgBase64").val("");

        //Si no has elegido una imagen nueva entonces se queda la que está.
        var dominio = "https://imagesoncobenefits.s3-sa-east-1.amazonaws.com";
        $("#hdimgBase64").val($(this).attr("data-imagen").replace(dominio, ""));

        $("#dtpIniEdit").val("");
        $("#dtpFinEdit").val("");
        $("#hdCategoryId").val($(this).attr("data-catID"));
        $("#txtTitle").val($(this).attr("data-titulo"));
        $("#txtDescription").val($(this).attr("data-desc"));

        $("#barcodeEdit").val($(this).attr("data-barcode"));
       
        document.getElementById("cboBarcodeFormatEdit").value = $(this).attr("data-barcode_format").toLowerCase();
        if ($(this).attr("data-barcode_format").toLowerCase() !== '') {
            var dvTpeStaticEdit = document.querySelector(".dvTpeStaticEdit");
            var cboTypeStaticEdit = document.getElementById("cboTypeStaticEdit");
            var dvTxtBarcodeEdit = document.querySelector(".dvTxtBarcodeEdit");
            var dvButtonViewEdit = document.querySelector(".dvButtonViewEdit");
            dvTpeStaticEdit.style.display = 'block'
            cboTypeStaticEdit.value = $(this).attr("data-typecode").toLowerCase();

            if ($(this).attr("data-typecode").toLowerCase() === 'static') {
                dvTxtBarcodeEdit.style.display = 'block'
                dvButtonViewEdit.style.display= 'block'
            } else {
                dvTxtBarcodeEdit.style.display = 'none'
                dvButtonViewEdit.style.display = 'none'
            }
           
        }

        $("#txtDescription").val($(this).attr("data-desc"));


        $("#txtConditions").val($(this).attr("data-condi"));

        if ($(this).attr("data-price") == 0 || $(this).attr("data-price") == "" || $(this).attr("data-price") == null) {
            $("#txtValPorcentPrice").val($(this).attr("data-percent"));
            $("#toggle-status").addClass("toggle-status-on");
            $("#lblValPorcentPrice").text("Porcentaje");
        }

        if ($(this).attr("data-percent") == 0 || $(this).attr("data-percent") == "" || $(this).attr("data-percent") == null) {
            $("#txtValPorcentPrice").val($(this).attr("data-price"));
            $("#toggle-status").removeClass("toggle-status-on");
            $("#lblValPorcentPrice").text("Precio");
        }

        $("#dtpIniEdit").val(fngetformatDate($(this).attr("data-startDate")));
        $("#dtpFinEdit").val(fngetformatDate($(this).attr("data-endDate")));
        $("#cbo-lstCatEdit").val($(this).attr("data_id_company"));

        $("#estrella1").attr("fill");
        $("#hdstar1").val($("#estrella1¬" + $("#hdCategoryId").val()).css("fill"));
        $("#hdstar2").val($("#estrella2¬" + $("#hdCategoryId").val()).css("fill"));
        $("#hdstar3").val($("#estrella3¬" + $("#hdCategoryId").val()).css("fill"));
        $("#hdstar4").val($("#estrella4¬" + $("#hdCategoryId").val()).css("fill"));
        $("#hdstar5").val($("#estrella5¬" + $("#hdCategoryId").val()).css("fill"));

        var { yearNow, monthNow, dayNow, hourNow, minNow, secNow, millsecNow } = fnSetFileName();
        var vfileName = $("#hdCategoryId").val() + '' + '_cupon_' + yearNow + '' + parseInt(monthNow + 1) + '' + dayNow + '' + hourNow + '' + minNow + '' + secNow + '' + millsecNow;
        $("#hdFileName").val(vfileName);

        $("#urlLinkEdit").val($(this).attr("data-url_link"));



    });

}

function removeSocialNetwork(obj) {
    //console.log(obj.parentNode.parentNode.parentNode)
    obj.parentNode.parentNode.parentNode.remove();
}

