$(document).on("click", "#btnGrabarAddUser", function () {

    if ($("#cboRoleAddUser").val() == -1) {
        alert('Seleccione un rol');
        $("#cboRoleAddUser").focus();
        return;
    }

    if ($("#cboRoleAddUser").val() == 3 && $("#cboAlianzaToAddUser").val() == -1) {
        alert('Asigne una alianza al usuario');
        $("#cboRoleAddUser").focus();
        return;
    }

    if ($("#txtNameAddUser").val() == "") {
        alert('Ingrese nombre');
        $("#txtNameAddUser").focus();
        return;
    }

    if ($("#txtlastNamepAddUser").val() == "") {
        alert('Ingrese Apellido Paterno');
        $("#txtlastNamepAddUser").focus();
        return;
    }

    if ($("#txtlastNamemAddUser").val() == "") {
        alert('Ingrese Apellido Materno');
        $("#txtlastNamemAddUser").focus();
        return;
    }

    //if ($("#txtPasswordAddUser").val() == "") {
    //    alert('Ingrese password');
    //    $("#txtPasswordAddUser").focus();
    //    return;
    //}

    if ($("#txtNumDocAddUser").val() == "") {
        alert('Ingrese Nro. de documento');
        $("#txtNumDocAddUser").focus();
        return;
    }

    if ($("#txtTelefAddUser").val() == "") {
        alert('Ingrese teléfono');
        $("#txtTelefAddUser").focus();
        return;
    }

    if ($("#txtEmailAddUser").val() == "") {
        alert('Ingrese email');
        $("#txtEmailAddUser").focus();
        return;
    }

    if (!fnvalidateEmail(document.getElementById("txtEmailAddUser").value)) {
        alert('Ingrese un email válido');
        $("#txtEmailAddUser").focus();
        return;
    }

    if ($("#txtDireccionAddUser").val() == "") {
        alert('Ingrese dirección');
        $("#txtDireccionAddUser").focus();
        return;
    }

    if ($("#cboDptoAddUser").val() == -1) {
        alert('Seleccione departamento');
        $("#cboDptoAddUser").focus();
        return;
    }

    if ($("#cboProvAddUser").val() == -1) {
        alert('Seleccione provincia');
        $("#cboProvAddUser").focus();
        return;
    }

    if ($("#cboDistAddUser").val() == -1) {
        alert('Seleccione distrito');
        $("#cboDistAddUser").focus();
        return;
    }

    

    if ($("#cboTarjetaAddUser").val() == -1 && $("#cboRoleAddUser").val() == 1) {
        alert('Elija una opción de tarjeta');
        return;
    }


    var v_ubigeo = "";
    if ($("#cboDptoAddUser").val() && $("#cboProvAddUser").val() && $("#cboDistAddUser").val()) {
        v_ubigeo = $("#cboDptoAddUser").val() + '' + $("#cboProvAddUser").val() + '' + $("#cboDistAddUser").val();
    }


    var odata = {
        "ID": -1,
        "NAME": $("#txtNameAddUser").val(),
        "LASTNAME_P": $("#txtlastNamepAddUser").val(),
        "LASTNAME_M": $("#txtlastNamemAddUser").val(),
        "USERNAME": $("#txtUsernameAddUser").val(),
        //"PASSWORD": $("#txtPasswordAddUser").val(),
        "TYPE_DOCUMENT": $("#cboTipoDocAddUser").val(),
        "DOCUMENT": $("#txtNumDocAddUser").val(),
        "PHONE1": $("#txtTelefAddUser").val(),
        "GENDER": $("#cboGenderAddUser").val(),
        "EMAIL": $("#txtEmailAddUser").val(),
        "ADDRESS": $("#txtDireccionAddUser").val(),
        "UBIGEO": v_ubigeo,
        "FK_ROLE": $("#cboRoleAddUser").val(),
        "CARD": $("#cboTarjetaAddUser").val(),
        "ESTADO": $("#cboEstadoAddUser").val(),
        "UPDATED_USER": -1,
        "COMPANY_ID": $("#cboAlianzaToAddUser").val()
    };

    $.ajax({
        method: "POST",
        url: "../User/SaveUser",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(odata),
        success: function (response) {
            if (response.code == 800) {
                alert('Número de documento existente!');
                return;
            }

            if (response.code == 200 && response.status == true) {
                $("#modalAddUser").hide();
                alert('guarde la contraseña generada para el usuario: ' + response.data)
                fnCrearTabla("", "", 1, 10, "Grabando...");
            }
        }
    });


});