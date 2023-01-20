let dataList = [];

function fnEditCompany(i) {

	$(document).on("click", "#btnEdit¬" + i, function () {

		let oData = new FormData();
		oData.append("Nombre", $(this).closest("tr").find("td").eq(1)[0].innerText);
		oData.append("Logo", $(this).closest("tr").find("td").eq(2)[0].childNodes[0].attributes[0].nodeValue);
		oData.append("Ruc", $(this).closest("tr").find("td").eq(3).text());
		oData.append("Telefono", $(this).closest("tr").find("td").eq(5).text());
		oData.append("Url", $(this).closest("tr").find("td").eq(4)[0].children[0].attributes[1].nodeValue);
		oData.append("Status", $(this).closest("tr").find("td").eq(6).text() == "Activo" ? true : false);
		oData.append("CompanyId", $(this).attr("id").split('¬')[1]);
		let lifeMiles = $(this).closest("tr").find("td").eq(7).find('[type=checkbox]').attr("checked");
		oData.append("LIFEMILES_PARTICIPATES_CAMPAIGN", (typeof lifeMiles === 'undefined' ? -1 : 1));

        //=================================================================//
        oData.append("Latitude", $(this).closest("tr").find("td").eq(8).text());
        oData.append("Longitude", $(this).closest("tr").find("td").eq(9).text());
        oData.append("Direction", $(this).closest("tr").find("td").eq(10).text());
		//oData.append("Country", $(this).closest("tr").find("td").eq(11).text());

		let Country = $(this).closest("tr").find("td").eq(11).text();


        //var html = "<div id='divAlertEditCompany'></div>";
		//html += "<input type='hidden' id='hdStatusOriginalImageCompany' value='' />";
		//html += "<input type='hidden' id='hdcompanyIdEditCompany' value=" + companyId + " />";
		//html += "<input type='hidden' id='hdimgBase64EditCompany' />";
		//html += "<div class='ant-form-group'><span>Nombre de alianza:</span><input class='input' type='text' id='txtNombreEditCompany' value='" + nombre + "' /></div>";

		//html += "<div class='ant-form-group'><span>¿Participa LifeMiles? </span> ";
		//html += "<br/>";
		//html += " <span>No</span> <input type='radio' class='text-center' value='0' checked data-value='0' name='optParticipateLifeMilesEdit' id='chkNoParticipateLifesMilesEdit'> ";
		//html += " &nbsp;<span>Si</span> <input type='radio' class='text-center' value='1' data-value='1' name='optParticipateLifeMilesEdit' id='chkSiParticipateLifesMilesEdit'> ";
		//html += "</div>";
		//html += "<br/>";


		//html += "<div class='box-image h200p margin-b'><label class='absolute margin btn btn-upload' for='iflogoEditCompany'><svg class=\"margin-r\" width=\"20\" height=\"20\" viewBox=\"0 0 98.5 108.615\"><g id=\"Grupo_16\" data-name=\"Grupo 16\" transform=\"translate(-703 -357.879)\"><g id=\"Grupo_15\" data-name=\"Grupo 15\" transform=\"translate(-8)\"><line id=\"Línea_6\" data-name=\"Línea 6\" y1=\"80\" transform=\"translate(760 360)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"3\"/><line id=\"Línea_7\" data-name=\"Línea 7\" x1=\"22\" y1=\"22\" transform=\"translate(760 360)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"3\"/><line id=\"Línea_8\" data-name=\"Línea 8\" y1=\"22\" x2=\"22\" transform=\"translate(738 360)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"3\"/></g><path id=\"Trazado_32\" data-name=\"Trazado 32\" d=\"M786.833,675.667V712s.795,13.454,12.68,13.667,69.338,0,69.338,0,13.482-1.333,13.482-13.667V676.5\" transform=\"translate(-82.333 -260.761)\" fill=\"none\" stroke=\"#fff\" stroke-linecap=\"round\" stroke-width=\"3\"/></g></svg>Seleccione imagen</label><img class='image contain' src='" + logo + "' id='objImgEditCompany' /></div>";
		//html += "<input class='hidden' type='file' id='iflogoEditCompany' accept='image/jpeg, image/png' />";
		//html += "<div class='row'>";
		//html += "<div class='ant-form-group col-6'><span>Número de ruc:</span><input class='input' type='text' id='txtRucEditCompany' value=" + ruc + " maxlength='11'/></div>";
		//html += "<div class='ant-form-group col-6'><span>Teléfono:</span><input type='text' class='input' id='txtTelefEditCompany' value=" + telef + " maxlength='9'/></div>";
		//html += "</div>";
		//html += "<div class='ant-form-group'><span>Url de fan page de facebook:</span><input class='input' type='text' id='txtUrlRedSocialEditCompany' value=" + rsocial + "/></div>";
		//html += "<div class='ant-form-group'><span>Estado</span>&nbsp;";
		//html += "<select class='input' id='cboEstado'>";
		//html += "<option value='True'>Activo</option>";
		//html += "<option value='False'>Inactivo</option>";
		//html += "</select></div>";

		//var setModalId = "mdl-edit-company";
		//var fnmodal = fncreateModal(setModalId, "Editar", html, "Actualizar");
		//if (estado == "Activo") { $("#cboEstado").val('True'); } else { $("#cboEstado").val('False'); }
				
		//var numMiles = $(this).closest("tr").find("td").eq(7).text();

		//if (typeof lifeMiles === 'undefined') {
			//$("#chkNoParticipateLifesMilesEdit").attr("checked", "checked")
			//$("#chkSiParticipateLifesMilesEdit").removeAttr("checked");
		//}
		//else {
			//$("#chkSiParticipateLifesMilesEdit").attr("checked", "checked")
			//$("#chkNoParticipateLifesMilesEdit").removeAttr("checked");
		//}

		$.ajax({
			method: "POST",
			url: "../Company/Edit",
			//contentType: "application/json; charset=utf-8",
			//dataType: "json",
			//data: JSON.stringify(oData),
			data: oData,
			processData: false,
			contentType: false,
			success: function (response) {
				$("#div_modal_company").empty();
				$("#div_modal_company").html(response);
				$("#cboCountry").append('<option value="0" disabled '+((Country=='--')?"selected":"")+'>[Seleccione Ciudad]</option>');
				for (let i = 0; i < dataList.length; i++) {
					const element = dataList[i].NAME_COUNTRY;
					$("#cboCountry").append('<option value="'+element+'" '+((Country==element)?"selected":"")+'>'+element+'</option>')
				}
				$("#hdStatusOriginalImageCompany").val(0);
				$("#mdl-edit-company").show();
			}
		});		
	});
}

$(function () {
	$(document).on("change", "#iflogoEditCompany", function () {
		fnRenderImage(this, "#objImgEditCompany", "#hdimgBase64EditCompany", "#hdStatusOriginalImageCompany", 512000);
	});

	$(document).on("click", "#btn-x,#btn-close", function () {
		$("#mdl-edit-company").hide();
		$(".bd-status").removeClass('on-modal');
	});

	$(document).on("keypress", "#txtRucEditCompany", function (e) {
		fnvalidateOnlyNumbers(this);
	});

	$(document).on("keypress", "#txtTelefEditCompany", function (e) {
		fnvalidateOnlyNumbers(this);
	});

	$(document).on("change", "#chkSiParticipateLifesMilesEdit", function () {
		$("#chkSiParticipateLifesMilesEdit").attr("checked", "checked");
		$("#chkNoParticipateLifesMilesEdit").removeAttr("checked");
		optLifeMilesEdit = 1;
	});

	$(document).on("change", "#chkNoParticipateLifesMilesEdit", function () {
		$("#chkNoParticipateLifesMilesEdit").attr("checked", "checked");
		$("#chkSiParticipateLifesMilesEdit").removeAttr("checked");
		optLifeMilesEdit = 0;
	});

	getcountry();


});


const getcountry = async () => {

	fetch('../Country/GetCountry')
		.then(response => response.json())
		.then(data => {

			for (let i = 0; i < data.length; i++) {
				
				dataList.push({
					ID:data[i].ID,
					NAME_COUNTRY:data[i].NAME_COUNTRY,
					STATUS: data[i].STATUS
				});

				
			}
			console.log(dataList)
			
		})
}
