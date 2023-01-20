
var optLifeMiles = 0;
var numMillas = 0;
$(function () {

	$(document).on("click", "#btnGrabar", function () {

		if ($("#txtNombreAddCompany").val() == "") {
			fncreateAlert("Ingrese nombre de la alianza", "warning");
			$("#txtNombreAddCompany").focus();
			return;
		}
		if (optLifeMiles == 0) {
			numMillas = 0;
		}
		var vlength = $("#iflogoAddCompany")[0].files[0];
		if (typeof vlength == "undefined") {
			fncreateAlert("Seleccione una imagen", "warning");
			return;
		}
		if (!/\.(jpg|png)$/i.test(vlength.name)) {
			fncreateAlert("Debe seleccionar un archivo de extensión jpg o png", "warning");
			return;
		}
		if (vlength.size > 1024 * 1024 * 2) {
			fncreateAlert("El archivo debe pesar menos de 2 MB.", "warning");
			return;
		}
		if ($("#txtRucAddCompany").val() == "") {
			fncreateAlert("Ingrese número de ruc", "warning");
			$("#txtRucAddCompany").focus();
			return;
		}
		if (document.getElementById("txtRucAddCompany").value.length < 11) {
			fncreateAlert("El ruc debe tener 11 dígitos", "warning");
			return;
		}
		if ($("#txtUrlRedSocialAddCompany").val() == "") {
			fncreateAlert("Ingrese url de su 'fan page' de facebook", "warning");
			$("#txtUrlRedSocialAddCompany").focus();
			return;
		}
		if ($("#txtTelefAddCompany").val() == "") {
			fncreateAlert("Ingrese teléfono", "warning");
			$("#txtTelefAddCompany").focus();
			return;
        }
        //direcciones
        if ($("#txtLatitude").val() == "") {
            fncreateAlert("Ingrese latitud", "warning");
            $("#txtLatitude").focus();
            return;
        }
        if ($("#txtLongitude").val() == "") {
            fncreateAlert("Ingrese longitud", "warning");
            $("#txtLongitude").focus();
            return;
        }
        if ($("#txtDirection").val() == "") {
            fncreateAlert("Ingrese direcciones", "warning");
            $("#txtDirection").focus();
            return;
        }

		if (!$("#cboCountry").val()) {
			fncreateAlert("Seleccione Ciudad", "warning");
			$("#cboCountry").focus();
			return;
		}

		var { yearNow, monthNow, dayNow, hourNow, minNow, secNow, millsecNow } = fnSetFileName();
		var fileName = 'alianza_' + yearNow + '' + parseInt(monthNow + 1) + '' + dayNow + '' + hourNow + '' + minNow + '' + secNow + '' + millsecNow;

		//var oData = {
		//    "Nombre": $("#txtNombreAddCompany").val(),
		//    "Logo": fileName,
		//    "Ruc": $("#txtRucAddCompany").val(),
		//    "Url": $("#txtUrlRedSocialAddCompany").val(),
		//    "Telefono": $("#txtTelefAddCompany").val(),
		//    "ImageBase64": $("#objImgAddCompany").attr("src"),
		//    "LIFEMILES_PARTICIPATES_CAMPAIGN": optLifeMiles,
		//    "NUMBER_MILES": numMillas,
		//};

		let oData = new FormData();
		oData.append("Nombre", $("#txtNombreAddCompany").val());
		//oData.append("Logo", fileName);
		oData.append("Ruc", $("#txtRucAddCompany").val());
		oData.append("Url", $("#txtUrlRedSocialAddCompany").val());
		oData.append("Telefono", $("#txtTelefAddCompany").val());
		oData.append("ImageBase64", $("#objImgAddCompany").attr("src"));
		oData.append("LIFEMILES_PARTICIPATES_CAMPAIGN", optLifeMiles);
        oData.append("NUMBER_MILES", numMillas);
        //====================================================//
        oData.append("Latitude", $("#txtLatitude").val());
        oData.append("Longitude", $("#txtLongitude").val());
        oData.append("Direction", $("#txtDirection").val());
		oData.append("Country", $("#cboCountry").val());
        //====================================================//
		oData.append("__RequestVerificationToken", $("#div_modal_company input[name='__RequestVerificationToken']").val());
       
		//$.ajax({
		//	method: "POST",
		//	url: "../Company/Save",
		//	//processData: false,
		//	contentType: "application/json; charset=utf-8",
		//	dataType: "json",
		//	data: JSON.stringify(oData),// oData,
		//	success: function (response) {
		//		$("#mdl-add-company").hide();
		//		fncompaniesList();
		//	}
		//});

		fncreatefeedbackToUser("Grabando...");
		$("#loading").addClass("flex");

		let xhr = new XMLHttpRequest();
		xhr.responseType = "json";
		xhr.onreadystatechange = () => {
			if (xhr.readyState == XMLHttpRequest.DONE) {
				if (xhr.status == 200) {
					let res = xhr.response;
					$("#mdl-add-company").hide();
					fncompaniesList();
					fncreateAlert("Registrado Correctamente", "success");
				}
				else {
					fncreateAlert("Error al registrar, inténtelo nuevamente", "error");
					console.log(xhr);
				}
				$("#loading").removeClass("flex");
			}
		};

		xhr.open("POST", "../Company/Save");
		//xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
		xhr.send(oData);
	});
});
