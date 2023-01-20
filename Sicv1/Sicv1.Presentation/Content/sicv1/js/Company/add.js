
$(function () {
    $("#btn_add_company").on("click", function () {

        //var html = "<input type='hidden' id='hdimgBase64AddCompany' />";
        //html += "<div id='divAlertAddCompany'></div>";
        //html += "<div class='ant-form-group'><span>Nombre de alianza:</span><input class='input' type='text' id='txtNombreAddCompany'/></div>";

        //html += "<div class='ant-form-group'><span>¿Participa LifeMiles? </span> ";
        //html += "<br/>";
        //html += " <span>No</span> <input type='radio' class='text-center' value='0' checked data-value='0' name='optParticipateLifeMiles' id='chkNoParticipateLifesMiles'> ";
        //html += " &nbsp;<span>Si</span> <input type='radio' class='text-center' value='1' data-value='1' name='optParticipateLifeMiles' id='chkSiParticipateLifesMiles'> ";
        //html += "</div>";
        //html += "<br/>";

        //html += "<div class='box-image h200p margin-b'><label class='absolute margin btn btn-upload' for='iflogoAddCompany'>";
        ////html += "<svg class=\"margin-r\" width=\"20\" height=\"20\" viewBox=\"0 0 98.5 108.615\"><g id=\"Grupo_16\" data-name=\"Grupo 16\" transform=\"translate(-703 -357.879)\"><g id=\"Grupo_15\" data-name=\"Grupo 15\" transform=\"translate(-8)\"><line id=\"Línea_6\" data-name=\"Línea 6\" y1=\"80\" transform=\"translate(760 360)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"3\"/><line id=\"Línea_7\" data-name=\"Línea 7\" x1=\"22\" y1=\"22\" transform=\"translate(760 360)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"3\"/><line id=\"Línea_8\" data-name=\"Línea 8\" y1=\"22\" x2=\"22\" transform=\"translate(738 360)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"3\"/></g><path id=\"Trazado_32\" data-name=\"Trazado 32\" d=\"M786.833,675.667V712s.795,13.454,12.68,13.667,69.338,0,69.338,0,13.482-1.333,13.482-13.667V676.5\" transform=\"translate(-82.333 -260.761)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"3\"/></g></svg>";
        //html += "Seleccione imagen</label>";
        //html += "<img class='image contain' src='#' id='objImgAddCompany' />";
        //html += "</div>";

        //html += "<input class='hidden' type='file' id='iflogoAddCompany' accept='image/jpeg, image/png' />";
        //html += "<div class='row'>";
        //html += "<div class='ant-form-group col-6'><span>Ruc:</span><input class='input' type='text' id='txtRucAddCompany' maxlength='11'/></div>";
        //html += "<div class='ant-form-group col-6'><span>Teléfono:</span><input type='text' class='input' id='txtTelefAddCompany' maxlength='9' /></div>";
        //html += "</div>";
        //html += "<div class='ant-form-group'><span>Url de fan page de facebook:</span><input class='input' type='text' id='txtUrlRedSocialAddCompany'/></div>";

        //var setModalId = "mdl-add-company";
        //var fnmodal = fncreateModal(setModalId, "Agregar", html, "Grabar");
  //      $("#div_modal_company").empty();
  //      $("#div_modal_company").append(fnmodal);
		//$("#" + setModalId).show();

		$.ajax({
			method: "POST",
			url: "../Company/Create",
			contentType: "application/json; charset=utf-8",
			//dataType: "json",
			//data: JSON.stringify(oData),
			success: function (response) {
				$("#div_modal_company").empty();
				$("#div_modal_company").html(response);
                //$("#cboCountry").append('<option value="0" disabled '+((Country=='--')?"selected":"")+'>[Seleccione Ciudad]</option>');
				for (let i = 0; i < dataList.length; i++) {
					const element = dataList[i].NAME_COUNTRY;
					$("#cboCountry").append('<option value="'+element+'">'+element+'</option>')
				}
				$("#mdl-add-company").show();
			}
		});
    });

    $(document).on("change", "#iflogoAddCompany", function () {
        fnRenderImage(this, "#objImgAddCompany", "#hdimgBase64AddCompany", null, 512000);
    });

    $(document).on("click", "#btn-x,#btn-close", function () {
        $("#mdl-add-company").hide();
        $(".bd-status").removeClass('on-modal');
    });

    $(document).on("keypress", "#txtRucAddCompany", function (e) {
        fnvalidateOnlyNumbers(this);
    });

    $(document).on("keypress", "#txtTelefAddCompany", function (e) {
        fnvalidateOnlyNumbers(this);
    });




    $(document).on("change", "#chkSiParticipateLifesMiles", function () {
        $("#chkSiParticipateLifesMiles").attr("checked", "checked");
        $("#chkNoParticipateLifesMiles").removeAttr("checked");
        optLifeMiles = 1;
        $("#txtNumeroMillasAddCompany").val("");
        $("#txtNumeroMillasAddCompany").removeAttr("disabled");
        $("#txtNumeroMillasAddCompany").focus();
    });

    $(document).on("change", "#chkNoParticipateLifesMiles", function () {
        $("#chkNoParticipateLifesMiles").attr("checked", "checked");
        $("#chkSiParticipateLifesMiles").removeAttr("checked");
        optLifeMiles = 0;
        $("#txtNumeroMillasAddCompany").val("");
        $("#txtNumeroMillasAddCompany").attr("disabled", "disabled");
    });

    $(document).on("keypress", "#txtNumeroMillasAddCompany", function (e) {
        fnvalidateOnlyNumbers(this);
    });

    
});