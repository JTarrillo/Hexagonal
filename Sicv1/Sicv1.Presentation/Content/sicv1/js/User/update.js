$(function () {
    fnActualizar();
});

function fnActualizar() {
    $(document).on("click", "#btnUpdateUser", function () {

        var ubigeo = "";
        if ($("#cboDpto").val() && $("#cboProv").val() && $("#cboDist").val()) {
            ubigeo = $("#cboDpto").val() + '' + $("#cboProv").val() + '' + $("#cboDist").val();
        }

        if ($("#cboRole").val() == -1) {
            alert('Seleccione un rol');
            $("#cboRole").focus();
            return;
        }

        if ($("#cboRole").val() == 3 && $("#cboAlianzaToEditUser").val() == -1) {
            alert('Asigne una alianza a este usuario.');
            $("#cboAlianzaToEditUser").focus();
            return;
        }

        if ($("#cboTarjeta").val() == -1 && $("#cboRole").val() == 1) {
            alert('Elija una opción de tarjeta');
            return;
        }

        //if ($("#txtPasswordEditUser").val() == "") {
        //    alert('Ingrese password');
        //    return;
        //}

        if ($("#txtNumDocEditUser").val() == "") {
            alert('Ingrese Número de documento');
            return;
        }

        var odata = {
            "ID": $("#hdUserId").val(),
            "NAME": $("#txtNameEditUser").val(),
            "LASTNAME_P": $("#txtlastNamepEditUser").val(),
            "LASTNAME_M": $("#txtlastNamemEditUser").val(),
            "USERNAME": $("#txtUsernameEditUser").val(),
            //"PASSWORD": fnEncryptClient($("#txtPasswordEditUser").val()),
            "TYPE_DOCUMENT": $("#cboTipoDoc").val(),
            "DOCUMENT": $("#txtNumDocEditUser").val(),
            "PHONE1": $("#txtTelefEditUser").val(),
            "GENDER": $("#cboGender").val(),
            "EMAIL": $("#txtEmailEditUser").val(),
            "ADDRESS": $("#txtDireccionEditUser").val(),
            "UBIGEO": ubigeo,
            "FK_ROLE": $("#cboRole").val(),
            "CARD": $("#cboTarjeta").val(),
            "ESTADO": $("#cboEstado").val(),
            "UPDATED_USER": "",
            "COMPANY_ID": $("#cboAlianzaToEditUser").val()
        };

        $.ajax({
            method: "POST",
            url: "../User/UpdateUser",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(odata),
            success: function (response) {
                if (response.code == 800) {
                    alert('Número de documento existente!');
                    return;
                }

                if (response.code == 200 && response.status == true) {
                    $("#modalEditUser").hide();
                    fnCrearTabla("", "", 1, 10, "Actualizando...");
                    $("#txt-buscar").val("");
                }
            }
        });

    });
}