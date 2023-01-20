$(function () {
    let newsocialNetwork = "";

    $("#btn-save-add").on("click", function () {



        newsocialNetwork = newcreateJsonCatUrl();   
        
        if (v_optLifeMilesAddCoupon == 0) {
            v_numMillasAddCoupon = 0;
        }

        if (v_optLifeMilesAddCoupon == 1 && $("#txtnumeromillasAddCoupon").val() == "") {
			fncreateAlert("Ingrese número de millas", 'warning');
            $("#txtnumeromillasAddCoupon").focus();
            return;
        }
        else {
            v_numMillasAddCoupon = $("#txtnumeromillasAddCoupon").val();
        }

        if (parseInt($("#txtnumeromillasAddCoupon").val()) == 0) {
            alert('El número de millas no puede ser cero(0).');
            $("#txtnumeromillasAddCoupon").focus();
            return;
        }

        var ID_PARENT = fnGetIdParent($("#cbo-catsChildByParentIdLevel0"), $("#cbo-catParents"), $("#cbo-catsChildByParentIdLevel1"));
        var vlength = $("#inputFileAdd")[0].files[0];

		if (typeof vlength == "undefined") {
			fncreateAlert("Seleccione una imagen", 'warning');
			return;
		}

		if (!/\.(jpg|png)$/i.test(vlength.name)) {
			fncreateAlert("Debe seleccionar un archivo de extensión jpg o png", 'warning');
			return;
		}
		if (vlength.size > 1024 * 1024 * 2) {
			fncreateAlert("El archivo debe pesar menos de 2 MB.", 'warning');
			return;
		}

		if ($("#txtTitleAdd").val() == "") {
			fncreateAlert("Ingrese título del cupón", 'warning');
			$("#txtTitleAdd").focus();
			return;
		}
		if ($("#txtDescriptionAdd").val() == "") {
			fncreateAlert("Ingrese descripción", 'warning');
			$("#txtDescriptionAdd").focus();
			return;
		}
		if ($("#txtConditionsAdd").val() == "") {
			fncreateAlert("Ingrese condiciones", 'warning');
			$("#txtConditionsAdd").focus();
			return;
		}

        var vImageBase64Add = $("#hdimgBase64Add").val();

        

        if ($("#toggle-status-add").hasClass("toggle-status-on")) {
            if ($("#txtValPorcentPriceAdd").val() == "") {
				fncreateAlert("Ingrese porcentaje", 'warning')
                return;
            }
            $("#hdpercentageAdd").val($("#txtValPorcentPriceAdd").val());
            $("#hdpriceAdd").val(0);
        }
        else {
            if ($("#txtValPorcentPriceAdd").val() == "") {
				fncreateAlert("Ingrese precio", 'warning');
                return;
            }
            $("#hdpriceAdd").val($("#txtValPorcentPriceAdd").val());
            $("#hdpercentageAdd").val(0);
        }

        var ini = $("#dtpIniAdd").val();
        var fin = $("#dtpFinAdd").val();
		if (ini == "") {
			fncreateAlert("Ingrese fecha inicial", 'warning');
			return;
		}
		if (fin == "") {
			fncreateAlert("Ingrese fecha final", 'warning');
			return;
		}
		if (fncompareDates(ini, fin) == false) {
			fncreateAlert("La fecha final no debe ser menor que la fecha inicial.", 'warning');
			return;
        }
        if ($("#cboCouponTypeAdd").val() == "URL") {
            if ($("#urlLink").val() == "") {
                fncreateAlert("Ingrese una URL.", 'warning');
                return;
            }
        }

        if ($("#cboCouponTypeAdd").val() == -1) {
            fncreateAlert("Seleccione un tipo valido", "warning");
            return;
        }

        var { yearNow, monthNow, dayNow, hourNow, minNow, secNow, millsecNow } = fnSetFileName();
        var vfileName = $("#hdCategoryId").val() + '' + '_cupon_' + yearNow + '' + parseInt(monthNow + 1) + '' + dayNow + '' + hourNow + '' + minNow + '' + secNow + '' + millsecNow;

        $("#hdFileNameAdd").val(vfileName);

        //var model = {
        //    "imageBase64": vImageBase64Add,
        //    "Title": $("#txtTitleAdd").val(),
        //    "Description": $("#txtDescriptionAdd").val(),
        //    "Conditions": $("#txtConditionsAdd").val(),
        //    "Price": $("#hdpriceAdd").val(),
        //    "Percentage": $("#hdpercentageAdd").val(),
        //    "hdFileName": $("#hdFileNameAdd").val(),
        //    "StartDate": ini,
        //    "EndDate": fin,
        //    "CreatedUser": $("#hduserId").val(),
        //    "CompanyId": $("#cbo-lstCatAdd").val(),
        //    "IdParent": ID_PARENT,
        //    "Type": $("#cboCouponTypeAdd").val(),
        //    "LIFEMILES_PARTICIPATES_CAMPAIGN": v_optLifeMilesAddCoupon,
        //    "NUMBER_MILES": v_numMillasAddCoupon
        //}

		let model = new FormData();
		model.append("imageBase64", vImageBase64Add);
		model.append("Title", $("#txtTitleAdd").val());
		model.append("Description", $("#txtDescriptionAdd").val());
		model.append("Conditions", $("#txtConditionsAdd").val());
		model.append("Price", $("#hdpriceAdd").val());
		model.append("Percentage", $("#hdpercentageAdd").val());
		model.append("hdFileName", $("#hdFileNameAdd").val());
		model.append("StartDate", ini);
		model.append("EndDate", fin);
		model.append("CreatedUser", $("#hduserId").val());
        model.append("CompanyId", $("#cbo-lstCatAdd").val());
        model.append("SHOW_IMAGE", $("#cboShowImage").val());
        model.append("CAT_URL", newsocialNetwork);
		model.append("IdParent", ID_PARENT);
        model.append("Type", $("#cboCouponTypeAdd").val());
        model.append("URL_LINK", $("#urlLink").val());
		model.append("LIFEMILES_PARTICIPATES_CAMPAIGN", v_optLifeMilesAddCoupon);
        model.append("NUMBER_MILES", v_numMillasAddCoupon);

        model.append("BARCODE", $("#barcodeX").val());
        model.append("BARCODE_FORMAT", $("#cboBarcodeFormat").val());
        model.append("TYPE_CODE", $("#cboTypeStatic").val());
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
		model.append("__RequestVerificationToken", $("#AddCouponModal input[name='__RequestVerificationToken']").val());

        fncreatefeedbackToUser("Grabando...");
        $("#loading").addClass("flex");

		let xhr = new XMLHttpRequest();
		xhr.responseType = "json";
		xhr.onreadystatechange = () => {
			if (xhr.readyState == XMLHttpRequest.DONE) {
				if (xhr.status == 200) {
					fnSaveCategoryCard(xhr.response.result);
                    $("#AddCouponModal").hide();
                    $(".bd-status").removeClass('on-modal');
					fnlistadoArticles($("#cbo-alianzas").val(), $("#txtCouponSearch").val(), $("#txtCouponSearch").val());
					fncreateAlert("Registrado correctamente.", 'success');
				}
				else {
					fncreateAlert("Error al registrar, inténtelo nuevamente", 'error');
					console.log(xhr);
				}
				$("#loading").removeClass("flex");
			}
		};
        console.log(model);
		xhr.open("POST", "../Category/SaveCategories");
		//xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
        xhr.send(model);

        newsocialNetwork = "";
        cat_url = [];


  //      $.ajax({
  //          method: "POST",
  //          url: "../Category/SaveCategories",
  //          contentType: "application/json; charset=utf-8",
  //          dataType: "json",
  //          data: JSON.stringify(model),
  //          success: function (response) {
  //              if (response.code == 200 && response.status == true) {
  //                  fnSaveCategoryCard(response.result);
  //                  $("#AddCouponModal").hide();
  //                  $(".bd-status").removeClass('on-modal');
  //                  fnlistadoArticles($("#cbo-alianzas").val(), $("#txtCouponSearch").val(), $("#txtCouponSearch").val());
  //                  $("#loading").removeClass("flex");
  //              }
  //          }
  //      });


    });

});

const newcreateJsonCatUrl = () => {

    newsocialNetwork = "";
    cat_url = [];
    
    const newsectionSocialNetwork = document.getElementsByClassName('newsectionSocialNetwork');

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
            url     : urlNetwork,
            status  : true
        });
        
    });

    return JSON.stringify(cat_url);
   
}

function fnSaveCategoryCard(categoryId) {
    var CategoryID = categoryId;
    var QueTienenTarjeta = $("#chkcardSi_AddCupon").is(":checked");
    var QueNoTienenTarjeta = $("#chknocardSi_AddCupon").is(":checked");

    var vHaveCard = ['S', 'N'];
    var vHaveAccess;
    if (QueTienenTarjeta == true && QueNoTienenTarjeta == true) { vHaveAccess = [1, 1]; }
    else if (QueTienenTarjeta == true && QueNoTienenTarjeta == false) { vHaveAccess = [1, 0]; }
    else if (QueTienenTarjeta == false && QueNoTienenTarjeta == true) { vHaveAccess = [0, 1]; }
    else if (QueTienenTarjeta == false && QueNoTienenTarjeta == false) { vHaveAccess = [0, 0]; }

    for (var i = 0; i < 2; i++) {
        var obj = {
            'ID_CATEGORY': CategoryID,
            'HAVE_CARD': vHaveCard[i],
            'HAVE_ACCESS': vHaveAccess[i]
        };

        $.ajax({
            method: "POST",
            url: "../CategoryCard/Save",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(obj),
            success: function () { }
        });
    }
}

function addNewSocialNetwork() {
    socialNetwork = "";
    cat_url = [];

    console.log('add');
    const newframeSocialNetwork = document.getElementsByClassName('newframeSocialNetwork')[0];

    console.log(newframeSocialNetwork);

    const newsectionSocialNetwork = newframeSocialNetwork.children[0];

    var obj = document.createElement("div");
    obj.className = 'row newsectionSocialNetwork';


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
    newsectionSocialNetwork.append(obj);


}
