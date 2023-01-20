$(function () {
    fncboRoleOnChangeAddUser();
    $("#btn-add-user").on("click", function () {
        fncreatefeedbackToUser("Cargando agregar usuario...");
        $("#loading").addClass("flex");

        setTimeout(function () {
            fnGetRolesAdd();
            fnGetAlianzasAddUser();
            fnInit();
            var html = "";
            html = "<div class='customModal' id='modalAddUser'>";
            html += '   <div class="modal-dialog" role="document">';
            html += "       <div class='modal-content'>";
            html += "           <div class='modal-header'>";
            html += "               <h5 class='modal-title'>Agregar usuario</h5>";
            html += "               <button type='button' id='btn-x' class='close' data-dismiss='modal' aria-label='Close'>";
            html += "                   <span aria-hidden='true'>&times;</span>";
            html += "               </button>";
            html += "           </div>";
            html += "               <div class='modal-body'>";

            html += "                   <div class='row'><div class='col-6 ant-form-group'><span>Roles</span><select id='cboRoleAddUser' class='input'></select></div>";
            html += "                   <div class='col-6 ant-form-group'><span>Estado</span><select id='cboEstadoAddUser' class='input'>";
            html += "                   <option value='1'>Activo</option>";
            html += "                   <option value='0'>Inactivo</option>";
            html += "                   </select></div></div>";

            html += "                   <div class='ant-form-group' id='divcboTarjetaAddUser'><span>Usuario</span><select id='cboTarjetaAddUser' class='input'>";
            html += "                   <option value='-1'>[Seleccione una opción de tarjeta]</option>";
            html += "                   <option value='S'>Sí, cuenta con tarjeta";
            html += "                   <option value='N'>No cuenta con tarjeta</option>";
            html += "                   </select></div>";

            html += "                <div class='ant-form-group' id='divAlianzaToAddUser' style='display:none;'>";
            html += "                   <span>Alianza</span><select id='cboAlianzaToAddUser' class='input'></select>";
            html += "                </div>";

            html += "                   <div class='ant-form-group'><span>Nombres</span><input class='input' type='text' placeholder='Nombres' id='txtNameAddUser' required /></div>";

            html += "                   <div class='row'><div class='col-6 ant-form-group'><span>Apellido Paterno</span><input class='input' type='text' placeholder='Apellido Paterno' id='txtlastNamepAddUser' /></div>";
            html += "                   <div class='col-6 ant-form-group'><span>Apellido Materno</span><input class='input' type='text' placeholder='Apellido Materno' id='txtlastNamemAddUser' /></div></div>";

            html += "               <div class='row'><div class='col-6 ant-form-group'><span>Tipo de documento</span><select id='cboTipoDocAddUser' class='input'>";
            html += "                   <option value='DNI'>DNI</option>";
            html += "                   <option value='PAS'>Pasaporte</option>";
            html += "                   <option value='CE'>Carnet de extranjería</option>";
            html += "                   <option value='SDI'>Sin documento</option>";
            html += "               <select/></div>";
            html += "               <div class='col-6 ant-form-group'><span>Número de documento</span><input type='text' class='input' placeholder='Número de documento' id='txtNumDocAddUser' maxlength='10' /></div>";

            //html += "                   <div class='row'></div>";
            //html += "                   <div class='col-12 ant-form-group'><span id='sp-pwd-addUser'>Password</span><input class='input' type='text' placeholder='Password' id='txtPasswordAddUser' /></div></div>";

            html += "               <div class='row'>";
            html += "                   <div class='col-6 ant-form-group'>";
            html += "                       <span>Teléfono de contacto</span><input type='text' class='input' placeholder='Teléfono' id='txtTelefAddUser' maxlength='10' />";
            html += "                   </div>";
            html += "                   <div class='col-6 ant-form-group'>";
            html += "                       <span>Género</span><select id='cboGenderAddUser' class='input'>   ";
            html += "                               <option value='MASCULINO'>Masculino</option>";
            html += "                               <option value='FEMENINO'>Femenino</option>";
            html += "                             <select/>";
            html += "                   </div>";
            html += "               </div>";

            html += "               <div class='row'>";
            html += "                   <div class='col-6 ant-form-group'>";
            html += "                       <span>Email</span><input type='email' class='input' placeholder='Email' id='txtEmailAddUser' />";
            html += "                   </div>";
            html += "                   <div class='col-6 ant-form-group'>";
            html += "                       <span>Dirección</span><input type='text' class='input' placeholder='Dirección' id='txtDireccionAddUser' />";
            html += "                   </div>";
            html += "               </div>";

            html += "               <div class='row'><div class='col-6 ant-form-group'><span>Departamento:</span> <select id='cboDptoAddUser' class='input'></select></div>";
            html += "               <div class='col-6 ant-form-group'><span>Provincia:</span> <select id='cboProvAddUser' class='input'></select></div></div>";

            html += "               <div class='ant-form-group'><span>Distrito:</span> <select id='cboDistAddUser' class='input'>";
            html += "               <select/></div></div>";



            html += "               <div class='modal-footer'>";
            html += "                   <button id='btn-close' class='btn btn-secondary'>Cerrar</button>";
            html += "                   <button id='btnGrabarAddUser' class='btn btn-primary'>Grabar</button>";
            html += "               </div>";
            html += "       </div>";
            html += "   </div>";
            html += "</div>";
            html += "<div id='divBaseAddUser'></div>";
            $("#divModalAddUser").append(html);
            $("#modalAddUser").show();
            fnGetDptosAddUser();
            $("#divcboTarjetaAddUser").hide();
            $("#cboTarjetaAddUser").val(-1);

            $("#divAlianzaToAddUser").hide();
            $("#loading").removeClass("flex");
        }, 2000);
    });
    fncbosUbigeoOnChangeAddUser();
});

function fnGetAlianzasAddUser() {
    $.ajax({
        method: "GET",
        url: "../Company/List",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#cboAlianzaToAddUser").empty();
            $("#cboAlianzaToAddUser").append("<option value='-1'>[Seleccione la alianza a la que pertencerá este usuario]</option>");
            for (var i = 0; i < response.length; i++) {
                $("#cboAlianzaToAddUser").append("<option value='" + response[i].ID + "'>" + response[i].NAME + "</option>");
            }
        }
    });
}

function fnGetRolesAdd() {
    $.ajax({
        method: "GET",
        url: "../Role/GetRoles",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#cboRoleAddUser").empty();
            $("#cboRoleAddUser").append("<option value='-1'>[Seleccione una opción]</option>");
            for (var i = 0; i < response.length; i++) {
                $("#cboRoleAddUser").append("<option value='" + response[i].ID + "'>" + response[i].NAME + "</option>");
            }
        }
    });
}

function fnInit() {
    $("#txtNameAddUser").val("");
    $("#txtlastNamepAddUser").val("");
    $("#txtlastNamemAddUser").val("");
    $("#txtUsernameAddUser").val("");
    //$("#txtPasswordAddUser").val("");
    $("#txtNumDocAddUser").val("");
    $("#txtTelefAddUser").val("");
    $("#txtEmailAddUser").val("");
    $("#txtDireccionAddUser").val("");
    $("#cboDptoAddUser").val(-1);
    $("#cboProvAddUser").val(-1);
    $("#cboDistAddUser").val(-1);
    $("#cboRoleAddUser").val(-1);
    $("#cboTarjetaAddUser").val(-1);
    $("#cboAlianzaToAddUser").val(-1);

    $("#cboGenderAddUser").val("MASCULINO");
    $("#cboEstadoAddUser").val(1);
    $("#cboTipoDocAddUser").val("DNI");
    //$("#txtPasswordAddUser").css("display", "table");
}

$(document).on("click", "#btn-x, #btn-close", function () {
    $("#modalAddUser").hide();
    $(".bd-status").removeClass('on-modal');
});

function fnGetDptosAddUser() {
    $.ajax({
        method: "POST",
        url: "../Ubigeo/GetDptos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#cboDptoAddUser").empty();
            $("#cboDptoAddUser").append("<option value='-1'>[Seleccione un departamento]</option>");
            for (var i = 0; i < response.length; i++) {
                $("#cboDptoAddUser").append("<option value='" + response[i].COD_DPTO + "'>" + response[i].NOM_DPTO + "</option>");
            }
        }
    });
}

function fnGetProvsAddUser(DptoId) {
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
            $("#cboProvAddUser").empty();

            $("#cboProvAddUser").append("<option value='-1'>[Seleccione una provincia]</option>");
            for (var i = 0; i < response.length; i++) {
                $("#cboProvAddUser").append("<option value='" + response[i].COD_PROV + "'>" + response[i].NOM_PROV + "</option>");
            }
        }
    });
}

function fnGetDistsAddUser(DptoId, ProvId) {
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
            $("#cboDistAddUser").empty();

            $("#cboDistAddUser").append("<option value='-1'>[Seleccione un distrito]</option>");
            for (var i = 0; i < response.length; i++) {
                $("#cboDistAddUser").append("<option value='" + response[i].COD_DIST + "'>" + response[i].NOM_DIST + "</option>");
            }
        }
    });
}

function fncbosUbigeoOnChangeAddUser() {
    $(document).on("change", "#cboDptoAddUser", function () {
        if ($(this).val() != -1) {
            fnGetProvsAddUser($(this).val());
        }
    });

    $(document).on("change", "#cboProvAddUser", function () {
        if ($(this).val() != -1) {
            fnGetDistsAddUser($("#cboDptoAddUser").val(), $(this).val());
        }
    });
}

function fncboRoleOnChangeAddUser() {
    $(document).on("change", "#cboRoleAddUser", function () {

        if ($(this).val() == 2 || $(this).val() == 5) {

            //$("#cboTarjetaAddUser").hide();
            $("#divcboTarjetaAddUser").hide();
            $("#cboTarjetaAddUser").val(-1);

            //$("#cboAlianzaToAddUser").css("display", "table");
            $("#divAlianzaToAddUser").show();

            //$("#txtPasswordAddUser").css("display", "table");
            //$("#txtPasswordAddUser").val("");
            //$("#sp-pwd-addUser").css("display", "table");
        }
        else if ($(this).val() == 1) {
            //$("#cboTarjetaAddUser").show();
            $("#divcboTarjetaAddUser").show();

            //$("#txtPasswordAddUser").css("display", "none");
            //$("#txtPasswordAddUser").val("123@abc");
            //$("#sp-pwd-addUser").css("display", "none");
        }
        else if ($(this).val() == -1) {
            //$("#cboAlianzaToAddUser").hide();
            $("#divAlianzaToAddUser").hide();
            $("#cboAlianzaToAddUser").val(-1);

            //$("#cboTarjetaAddUser").hide();
            $("#divcboTarjetaAddUser").hide();
            $("#cboTarjetaAddUser").val(-1);
        }

        if ($(this).val() == 3) {
            //$("#cboAlianzaToAddUser").show();
            $("#divAlianzaToAddUser").show();
            //$("#cboTarjetaAddUser").hide();
            $("#divcboTarjetaAddUser").hide();
            $("#cboTarjetaAddUser").val(-1);
            //$("#txtPasswordAddUser").css("display", "table");
            //$("#txtPasswordAddUser").val("");
            //$("#sp-pwd-addUser").css("display", "table");
        }
        else if ($(this).val() == 2 || $(this).val() == 5 || $(this).val() == 1) {
            //$("#cboAlianzaToAddUser").hide();
            $("#divAlianzaToAddUser").hide();
            $("#cboAlianzaToAddUser").val(-1);
        }

    });
}

function fnvalidateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

$(document).on("keypress", "#txtNumDocAddUser", function (e) {
    fnvalidateOnlyNumbers(this);
});

$(document).on("keypress", "#txtTelefAddUser", function (e) {
    fnvalidateOnlyNumbers(this);
});