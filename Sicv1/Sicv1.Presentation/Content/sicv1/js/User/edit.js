function fnEditUser(id) {
    $(document).on("click", "#" + id, function () {
        fncreatefeedbackToUser("Cargando editar usuario...");
        $("#loading").addClass("flex");

        var userId = $(this).attr("id").split("¬")[1];
        document.getElementById("hdUserId").value = "";
        document.getElementById("hdUserId").value = userId;
        fnGetDptos($(this).attr("data-dptoId"));
        fnGetProvs($(this).attr("data-dptoId"), $(this).attr("data-provId"));
        fnGetDists($(this).attr("data-dptoId"), $(this).attr("data-provId"), $(this).attr("data-distId"));

        $("#txtNameEditUser").val($(this).attr("data-name"));
        $("#txtlastNamepEditUser").val($(this).attr("data-lastnamep"));
        $("#txtlastNamemEditUser").val($(this).attr("data-lastnamem"));
        $("#cboTipoDoc").val($(this).attr("data-typeDocument"));
        $("#txtNumDocEditUser").val($(this).attr("data-numDoc"));
        $("#txtTelefEditUser").val($(this).attr("data-telef"));

        if ($(this).attr("data-email") == "") {
            $("#txtEmailEditUser").val("----");
        }
        else {
            $("#txtEmailEditUser").val($(this).attr("data-email"));
        }

        $("#txtDireccionEditUser").val($(this).attr("data-address"))
        $("#cboGender").val($(this).attr("data-gender"));
        $("#cboEstado").val($(this).attr("data-estado"));
        //$("#txtPasswordEditUser").val($(this).attr("data-password"))

        //----------------jbalmaceda---------------------//
        if ($(this).attr("data-FkRole") == -1) {         //
            $("#cboRole").removeAttr("disabled");        //
            fnGetRoles($(this).attr("data-FkRole"));     //
        }                                                //
        else {                                           //
            $("#cboRole").prop("disabled", "disabled");  //
        }                                                //
        //-----------------------------------------------//

        fnGetRoles($(this).attr("data-FkRole"));
        fnGetAlianzasEditUser($(this).attr("data-companyId"));
        var IdRol = $(this).attr("data-FkRole");
        if (IdRol == 1) {

            $("#div-cboTarjeta").show();
            $("#div-cboAlianzaToEditUser").hide();
            $("#cboTarjeta").val($(this).attr("data-card"))
            //$("#div-PasswordEditUser").hide();
        }
        else if (IdRol == 2) {
            $("#div-cboTarjeta").hide();
            $("#div-cboAlianzaToEditUser").hide();
            //$("#div-PasswordEditUser").show();
        }
        else if (IdRol == 3) {
            $("#div-cboAlianzaToEditUser").show();
            $("#div-cboTarjeta").hide();
            //$("#div-PasswordEditUser").show();
        }
        else if (IdRol == 5) {
            $("#div-cboTarjeta").hide()
            $("#div-cboAlianzaToEditUser").hide();
            //$("#div-PasswordEditUser").show();
        }

        setTimeout(function () {
            //mostrar modal
            $("#modalEditUser").show();
            $("#loading").removeClass("flex");
        }, 2000);
    });
}

$(function () {
    fncbosUbigeoOnChange();
    fncboRoleOnChange();
});

function fnGetAlianzasEditUser(IndiceAlianzaACargar) {
    $.ajax({
        method: "GET",
        url: "../Company/List",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#cboAlianzaToEditUser").empty();
            $("#cboAlianzaToEditUser").append("<option value='-1'>[Seleccione la alianza a la que pertencerá este usuario]</option>");
            for (var i = 0; i < response.length; i++) {
                $("#cboAlianzaToEditUser").append("<option value='" + response[i].ID + "'>" + response[i].NAME + "</option>");
            }
            $("#cboAlianzaToEditUser").val(IndiceAlianzaACargar);
        }
    });
}

function fnGetRoles(IndiceRolACargar) {
    $.ajax({
        method: "GET",
        url: "../Role/GetRoles",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#cboRole").empty();
            $("#cboRole").append("<option value='-1'>[Seleccione una opción]</option>");

            $("#cboRoleTmp").empty();
            $("#cboRoleTmp").append("<option value='-1'>[Seleccione una opción]</option>");

            for (var i = 0; i < response.length; i++) {
                $("#cboRoleTmp").append("<option value='" + response[i].ID + "'>" + response[i].NAME + "</option>");
                $("#cboRole").append("<option value='" + response[i].ID + "'>" + response[i].NAME + "</option>");
            }
            $("#cboRoleTmp").val(IndiceRolACargar);
            $("#cboRole").val(IndiceRolACargar);
        }
    });
}

function fnGetDptos(index) {
    $.ajax({
        method: "POST",
        url: "../Ubigeo/GetDptos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#cboDpto").empty();

            $("#cboDpto").append("<option value='-1'>[Seleccione un departamento]</option>");
            for (var i = 0; i < response.length; i++) {
                $("#cboDpto").append("<option value='" + response[i].COD_DPTO + "'>" + response[i].NOM_DPTO + "</option>");
            }
            $("#cboDpto").val(index);
        }
    });
}

function fnGetProvs(DptoId, index = null) {
    var obj = {
        'DptoId': DptoId
    };
    $.ajax({
        method: "POST",
        url: "../Ubigeo/GetProvs",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(obj),
        success: function (response) {
            $("#cboProv").empty();

            $("#cboProv").append("<option value='-1'>[Seleccione una provincia]</option>");
            for (var i = 0; i < response.length; i++) {
                $("#cboProv").append("<option value='" + response[i].COD_PROV + "'>" + response[i].NOM_PROV + "</option>");
            }

            $("#cboProv").val(index);
        }
    });
}

function fnGetDists(DptoId, ProvId, index = null) {
    var obj = {
        'DptoId': DptoId,
        'ProvId': ProvId
    };
    $.ajax({
        method: "POST",
        url: "../Ubigeo/GetDists",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(obj),
        success: function (response) {
            $("#cboDist").empty();

            $("#cboDist").append("<option value='-1'>[Seleccione un distrito]</option>");
            for (var i = 0; i < response.length; i++) {
                $("#cboDist").append("<option value='" + response[i].COD_DIST + "'>" + response[i].NOM_DIST + "</option>");
            }

            $("#cboDist").val(index);
        }
    });
}

function fncbosUbigeoOnChange() {
    $(document).on("change", "#cboDpto", function () {
        if ($(this).val() != -1) {
            fnGetProvs($(this).val());
        }
    });
    $(document).on("change", "#cboProv", function () {
        if ($(this).val() != -1) {
            fnGetDists($("#cboDpto").val(), $(this).val());
        }
    });
}

function fncboRoleOnChange() {
    $(document).on("change", "#cboRole", function () {
        var IdRol = $(this).val();

        if (IdRol == 1) {
            $("#div-cboTarjeta").show();
            $("#div-cboAlianzaToEditUser").hide();

            if ($(this).attr("data-card") == null) {
                $("#cboTarjeta").val(-1);
            } else {
                $("#cboTarjeta").val($(this).attr("data-card"));
            }
        }
        else if (IdRol == 2) {
            $("#div-cboTarjeta").hide();
            $("#div-cboAlianzaToEditUser").hide();
        }
        else if (IdRol == 3) {

            var idAlianza = $("#cboAlianzaToEditUser").val();
            if (idAlianza == null) {
                $("#cboAlianzaToEditUser").val(-1);
            }
            $("#div-cboAlianzaToEditUser").show();
            $("#div-cboTarjeta").hide();
        }
        else if (IdRol == 5) {

            $("#div-cboTarjeta").hide()
            $("#div-cboAlianzaToEditUser").hide();
        }
    });
}

$(document).on("click", "#btn-x, #btn-close", function () {
    $("#modalEditUser").hide();
    $(".bd-status").removeClass('on-modal');
});

$(document).on("keypress", "#txtNumDocEditUser", function (e) {
    fnvalidateOnlyNumbers(this);
});

$(document).on("keypress", "#txtTelefEditUser", function (e) {
    fnvalidateOnlyNumbers(this);
});
