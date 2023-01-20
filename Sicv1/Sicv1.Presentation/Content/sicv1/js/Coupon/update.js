function fnUpdate() {

    let udpsocialNetwork = "";

    $("#btn-update").on("click", function () {

        udpsocialNetwork = udpcreateJsonCatUrl();

        console.log(udpsocialNetwork);

        if ($('#chkSIparticipatelifesmilesEditCoupon').is(':checked')) {
            $("#chkNOparticipatelifesmilesEditCoupon").prop("checked", false);
            v_optLifeMilesEditCoupon = 1;
            v_numMillasEditCoupon = $("#txtnumeromillasEditCoupon").val();
        }
        else {
            $("#chkSIparticipatelifesmilesEditCoupon").prop("checked", false);
            v_optLifeMilesEditCoupon = 0;
            v_numMillasEditCoupon = 0;
        }

        if (v_optLifeMilesEditCoupon == 1 && $("#txtnumeromillasEditCoupon").val() == "") {
            alert('Ingrese número de millas');
            $("#txtnumeromillasEditCoupon").focus();
            return;
        }

        if (parseInt($("#txtnumeromillasEditCoupon").val()) == 0) {
            alert('El número de millas no puede ser cero(0).');
            $("#txtnumeromillasEditCoupon").val("");
            $("#txtnumeromillasEditCoupon").focus();
            return;
        }

        var cbo1 = $("#cbo-catParents-edit");
        var cbo2 = $("#cbo-catsChildByParentIdLevel-edit0");
        var cbo3 = $("#cbo-catsChildByParentIdLevel-edit1");
        var ID_PARENT = fnGetIdParent(cbo2, cbo1, cbo3);

		if ($("#txtTitle").val() == "") {
			fncreateAlert("Ingrese título del cupón", "warning");
			$("#txtTitle").focus();
			return;
		}
		if ($("#txtDescription").val() == "") {
			fncreateAlert("Ingrese descripción", "warning");
			$("#txtDescription").focus();
			return;
		}
		if ($("#txtConditions").val() == "") {
			fncreateAlert("Ingrese condiciones", "warning");
			$("#txtConditions").focus();
			return;
		}
		
		let input_file = $("#inputFile")[0];
		if (input_file.files.length > 0) {
			let file = input_file.files[0];
			if (!/\.(jpg|png)$/i.test(file.name)) {
				fncreateAlert("Debe seleccionar un archivo de extensión jpg o png", "warning");
				return;
			}
			if (file.size > 1024 * 1024 * 2) {
				fncreateAlert("El archivo debe pesar menos de 2 MB.", "warning");
				return;
			}
		}
        var vImageBase64 = $("#hdimgBase64").val();
        var ini = $("#dtpIniEdit").val();
        var fin = $("#dtpFinEdit").val();
		if (ini == "") {
			fncreateAlert("Ingrese fecha inicial", "warning");
			return;
		}
		if (fin == "") {
			fncreateAlert("Ingrese fecha final", "warning");
			return;
		}

        if (fncompareDates(ini, fin) == false) {
			fncreateAlert("La fecha final no debe ser menor que la fecha inicial.", "warning");
            return;
        }

        if ($("#cboCouponTypeEdit").val() == -1) {
            fncreateAlert("Seleccione un tipo valido", "warning");
            return;
        }


        if ($("#toggle-status").hasClass("toggle-status-on")) {
            if ($("#txtValPorcentPrice").val() == "") {
				fncreateAlert("Ingrese porcentaje", "warning");
                return;
            }
            $("#hdpercentageEdit").val($("#txtValPorcentPrice").val());
            $("#hdpriceEdit").val(0);
        }
        else {
            if ($("#txtValPorcentPrice").val() == "") {
				fncreateAlert("Ingrese precio", "warning");
                return;
            }
            $("#hdpriceEdit").val($("#txtValPorcentPrice").val());
            $("#hdpercentageEdit").val(0);
        }

        fncreatefeedbackToUser("Actualizando...");
        $("#loading").addClass("flex");


 
        var obj = {
            "hdCategoryId": $("#hdCategoryId").val(),
        };
        var vImageBase64Add = $("#hdimgBase64").val();

        let model = new FormData();
        model.append("ID", $("#idCouponHidden").val());
        model.append("hdCategoryId", $("#idCouponHidden").val());
        model.append("imageBase64", vImageBase64Add);
		model.append("Title", $("#txtTitle").val());
		model.append("Description", $("#txtDescription").val());
		model.append("Conditions", $("#txtConditions").val());
		model.append("Price", $("#hdpriceEdit").val());
		model.append("Percentage", $("#hdpercentageEdit").val());
		model.append("hdFileName", $("#hdFileName").val());
		model.append("StartDate", ini);
		model.append("EndDate", fin);		
		model.append("CompanyId", $("#cbo-lstCatEdit").val());
		model.append("IdParent", ID_PARENT);
		model.append("StatusOriginalImage", $("#hdStatusOriginalImage").val());
        model.append("Type", $("#cboCouponTypeEdit").val());
        model.append("URL_LINK", $("#urlLinkEdit").val());
		model.append("LIFEMILES_PARTICIPATES_CAMPAIGN", v_optLifeMilesEditCoupon);
        model.append("NUMBER_MILES", v_numMillasEditCoupon);
        model.append("BARCODE", $("#barcodeEdit").val());
        model.append("BARCODE_FORMAT", $("#cboTypeStaticEdit").val());
        model.append("TYPE_CODE", $("#cboTypeStaticEdit").val());
        model.append("SHOW_IMAGE", $("#cboShowImageEdit").val());
        model.append("CAT_URL", udpsocialNetwork);

        let arrays = []
        if ($("#chk1").is(":checked"))
            arrays.push({ id: $("#chk1").data("id") })
        if ($("#chk2").is(":checked"))
            arrays.push({ id: $("#chk2").data("id") })
        if ($("#chk3").is(":checked"))
            arrays.push({ id: $("#chk3").data("id") })
        if ($("#chk4").is(":checked"))
            arrays.push({ id: $("#chk4").data("id") })
        if ($("#chk5").is(":checked"))
            arrays.push({ id: $("#chk5").data("id") })
        if ($("#chk6").is(":checked"))
            arrays.push({ id: $("#chk6").data("id") })
        model.append("SEGMENT", JSON.stringify(arrays))
		model.append("__RequestVerificationToken", $("#EditCouponModal input[name='__RequestVerificationToken']").val());

		let xhr = new XMLHttpRequest();
		xhr.responseType = "json";
		xhr.onreadystatechange = () => {
			if (xhr.readyState == XMLHttpRequest.DONE) {
				if (xhr.status == 200) {
                    fnUpdateCategoryCard(obj.hdCategoryId, $("#barcodeEdit").val(), $("#cboBarcodeFormatEdit").val());
					$("#EditCouponModal").hide();
					$(".bd-status").removeClass('on-modal');
					fnlistadoArticles($("#cbo-alianzas").val(), $("#txtCouponSearch").val(), $("#txtCouponSearch").val());
                    fncreateAlert("Actualizado Correctamente", "success");

				}
				else {
					fncreateAlert("Error al actualizar, inténtelo nuevamente", "error");
					console.log(xhr);
				}
				$("#loading").removeClass("flex");
			}
		};

		xhr.open("POST", "../Category/UpdateCategoriesById");
		//xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
        xhr.send(model);

        udpsocialNetwork = "";
        cat_url = [];

        //$.ajax({
        //    method: "POST",
        //    url: "../Category/UpdateCategoriesById",
        //    data: obj,
        //    success: function (response) {
        //        if (response.code == 200 && response.status == true) {
        //            fnUpdateCategoryCard(obj.hdCategoryId);
        //            $("#EditCouponModal").hide();
        //            $(".bd-status").removeClass('on-modal');
        //            fnlistadoArticles($("#cbo-alianzas").val(), $("#txtCouponSearch").val(), $("#txtCouponSearch").val());
        //            $("#loading").removeClass("flex");
        //        }
        //    }
        //});
    });
}

function fnUpdateCategoryCard(categoryId, barcode, barcode_format) {
    var CategoryID = categoryId;
    var QueTienenTarjeta = $("#chkcardSi_EditCupon").is(":checked");
    var QueNoTienenTarjeta = $("#chknocardSi_EditCupon").is(":checked");

    var vHaveCardUpdate = ['S', 'N'];
    var vHaveAccessUpdate;
    if (QueTienenTarjeta == true && QueNoTienenTarjeta == true) { vHaveAccessUpdate = [1, 1]; }
    else if (QueTienenTarjeta == true && QueNoTienenTarjeta == false) { vHaveAccessUpdate = [1, 0]; }
    else if (QueTienenTarjeta == false && QueNoTienenTarjeta == true) { vHaveAccessUpdate = [0, 1]; }
    else if (QueTienenTarjeta == false && QueNoTienenTarjeta == false) { vHaveAccessUpdate = [0, 0]; }

    var idCategoryCard = [$("#hdidTieneTarjeta").val(), $("#hdidNoTieneTarjeta").val()];
    for (var i = 0; i < 2; i++) {
        var objUpdate = {
            "ID": idCategoryCard[i],
            'ID_CATEGORY': CategoryID,
            'HAVE_CARD': vHaveCardUpdate[i],
            'HAVE_ACCESS': vHaveAccessUpdate[i],
            'BARCODE': barcode,
            'BARCODE_FORMAT': barcode_format
        };

        $.ajax({
            method: "POST",
            url: "../CategoryCard/Update",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(objUpdate),
            success: function () { }
        });
    }
}

const udpcreateJsonCatUrl = () => {

    udpsocialNetwork = "";
    cat_url = [];

    const newsectionSocialNetwork = document.getElementsByClassName('editsectionSocialNetwork');

    Array.prototype.forEach.call(newsectionSocialNetwork, function (elem) {

        var TypeUrl = elem.children[0].children[1].value;
        var TypeSocialNetwork = elem.children[1].children[0].children[1].value;
        var titleWeb = elem.children[1].children[1].children[1].value;
        var urlNetwork = elem.children[1].children[2].children[1].value;

        let icon = "";
        let color = "";
        let title = "";

        switch (TypeSocialNetwork) {
            case "1":
                title = "Facebook";
                icon = "io-facebook";
                color = "#1873eb";
                break;
            case "2":
                title = "Instagram";
                icon = "io-instagram";
                color = "#d10869";
                break;
            case "3":
                title = "TikTok";
                icon = "io-tiktok";
                color = "#010101";
                break;
            default:
                title = titleWeb;
                icon = "";
                color = "#010101";
                break;
        }

        cat_url.push({
            icon: (TypeUrl == 1 ? icon : ""),
            title: (TypeUrl == 1 ? title : titleWeb),
            color: (TypeUrl == 1 ? color : "#010101"),
            url: urlNetwork,
            status: true
        });

    });

    return JSON.stringify(cat_url);

}

function addEditSocialNetwork() {
    console.log('addEdit');
    const udpframeSocialNetwork = document.getElementsByClassName('udpframeSocialNetwork')[0];

    console.log(udpframeSocialNetwork);

    const udpsectionSocialNetwork = udpframeSocialNetwork.children[0];

    var obj = document.createElement("div");
    obj.className = 'row editsectionSocialNetwork';


    var html = '<div class="col-4 ant-form-group">';
    html += '<span>Tipo:*</span>';
    html += '<select id="TypeUrl" class="input TypeUrl" onchange="selectTypeUrl(this)">';
    html += '<option value="-1">[Seleccionar]</option>';
    html += '<option value="1">RED SOCIAL</option>';
    html += '<option value="2">WEB</option>';
    html += '</select>';
    html += '<center><buttom class="btn btn-pink" onclick="removeSocialNetwork(this)">Remover</buttom></center>';
    html += '</div>';
    html += '<div class="col-8">';
    html += '<div class="ant-form-group sltSocial" style="display: none;">';
    html += '<span>Icono:</span>';
    html += '<select id="TypeSocialNetwork" class="input TypeSocialNetwork" onchange="">';
    html += '<option value="-1">[Seleccionar]</option>';
    html += '<option value="1">Facebook</option>';
    html += '<option value="2">Instagram</option>';
    html += '<option value="3">Tiktok</option>';
    html += '</select>';
    html += '</div>';
    html += '<div class="ant-form-group iptTitle" style="display: none;">';
    html += '<span>Titulo:</span>';
    html += '<input id="titleWeb" class="input titleWeb" type="text" required>';
    html += '</div>';
    html += '<div class="ant-form-group iptUrl" style="display: none;">';
    html += '<span>URL:</span>';
    html += '<input id="urlNetwork" class="input urlNetwork" type="text" required>';
    html += '</div>';
    html += '</div>';
    obj.innerHTML = html;
    udpsectionSocialNetwork.append(obj);


}

