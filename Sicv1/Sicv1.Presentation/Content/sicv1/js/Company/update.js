var optLifeMilesEdit = 0;
var numMillasEdit = 0;
$(function () {
	$(document).on("click", "#btnActualizar", function () {

		if ($("#txtNombreEditCompany").val() == "") {
			fncreateAlert("Ingrese nombre de la alianza", "warning");
			$("#txtNombreEditCompany").focus();
			return;
		}

		let input_file = $("#iflogoEditCompany")[0];
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

		if ($("#txtRucEditCompany").val() == "") {
			fncreateAlert("Ingrese número de ruc", "warning");
			$("#txtRucEditCompany").focus();
			return;
		}

		if (document.getElementById("txtRucEditCompany").value.length < 11) {
			fncreateAlert("El ruc debe tener 11 dígitos", "warning");
			return;
		}

		if ($("#txtUrlRedSocialEditCompany").val() == "") {
			fncreateAlert("Ingrese url de su 'fan page' de facebook", "warning");
			$("#txtUrlRedSocialEditCompany").focus();
			return;
		}

		if ($("#txtTelefEditCompany").val() == "") {
			fncreateAlert("Ingrese teléfono", "warning");
			$("#txtTelefEditCompany").focus();
			return;
		}

		if (!$("#cboCountry").val()) {
			fncreateAlert("Seleccione Ciudad", "warning");
			$("#cboCountry").focus();
			return;
		}		

		var { yearNow, monthNow, dayNow, hourNow, minNow, secNow, millsecNow } = fnSetFileName();
		var fileName = 'alianza_' + yearNow + '' + parseInt(monthNow + 1) + '' + dayNow + '' + hourNow + '' + minNow + '' + secNow + '' + millsecNow;

		var dominio = "https://imagesoncobenefits.s3-sa-east-1.amazonaws.com";

		let oData = new FormData();
		oData.append("CompanyId", $("#hdcompanyIdEditCompany").val());
		oData.append("Nombre", $("#txtNombreEditCompany").val());
		//oData.append("Logo", fileName);
		oData.append("Ruc", $("#txtRucEditCompany").val());
		oData.append("Url", $("#txtUrlRedSocialEditCompany").val());
		oData.append("Telefono", $("#txtTelefEditCompany").val());
		oData.append("StatusOriginalImage", $("#hdStatusOriginalImageCompany").val());
		oData.append("ImageBase64", $("#objImgEditCompany").attr("src").replace(dominio, ""));
		oData.append("Status", $("#cboEstado").val());
		oData.append("LIFEMILES_PARTICIPATES_CAMPAIGN", optLifeMilesEdit);
        oData.append("NUMBER_MILES", numMillasEdit);
        //====================================================//
        oData.append("Latitude", $("#txtLatitude").val());
        oData.append("Longitude", $("#txtLongitude").val());
        oData.append("Direction", $("#txtDirection").val());
		oData.append("Country", $("#cboCountry").val());
        
		oData.append("__RequestVerificationToken", $("#div_modal_company input[name='__RequestVerificationToken']").val());

		fncreatefeedbackToUser("Actualizando...");
		$("#loading").addClass("flex");

		let xhr = new XMLHttpRequest();
		xhr.responseType = "json";
		xhr.onreadystatechange = () => {
            if (xhr.readyState == XMLHttpRequest.DONE) {
               // console.log(oData)
                //console.log(xhr)
                if (xhr.status == 200) {
                    if (xhr.response.status) {
                        $("#mdl-edit-company").hide();
                        fncompaniesList();
                        fncreateAlert("Actualizado Correctamente", "success");
                    } else {
                        //console.log('error', xhr.response.status)
                        fncreateAlert(xhr.response.message, "error");
                    }
				}
				else {
					fncreateAlert("Error al actualizar, inténtelo nuevamente", "error");
					console.log(xhr);
				}
				$("#loading").removeClass("flex");
			}
		};

		xhr.open("POST", "../Company/Update");
		xhr.send(oData);
	});
});

function fnbtnClose() {
	$(document).on("click", "#btn-x, #btn-close", function () {
		$("#EditCouponModal").hide();
		$(".bd-status").removeClass('on-modal');
	});
}
