let numTotalRegistros = 0;
let numTotalPaginas = 0;
let numRegistrosPorPagina = 10;

$(function () {
    fncreatefeedbackToUser("Cargando dashboard...", 1);  
});


window.onload = function () {

    $("#txt-pagina-actual").prop("readonly", true);
    this.fnNumTotalRegistros($("#cboRoleFilter").val());
    this.fnCrearTabla("", "", 1, numRegistrosPorPagina, "Cargando...");

    this.fnBtnSiguiente();
    this.fnBtnAnterior();
    this.fnBtnPrimero();
    this.fnBtnUltimo();
    this.fnbtnBuscar();
    this.fnbtnCancelarBusqueda();
};

function fnNumTotalRegistros(roleId = null)
{
    var data = { 'roleId': roleId };
    $.ajax({
        method: "POST",
        url: "../User/GetCount",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data),
        success: function (e)
        {
            let data = e;
            numTotalRegistros = data;
            document.getElementById("hd-total-registros").value = data;
            numTotalPaginas = Math.ceil(parseInt(document.getElementById("hd-total-registros").value) / parseInt(numRegistrosPorPagina));
            document.getElementById("lbltotalpaginas").innerText = numTotalPaginas;
            document.getElementById("lbl-total-registros").innerText = data;
            document.getElementById("lbl-num-registros-actual").innerText = parseInt(document.getElementById("txt-pagina-actual").value) * parseInt(numRegistrosPorPagina);
        }
    });
}

function fnCrearTabla(NumDniSearch = null, RoleId = null, CurrentPage = null, RecordsPerPage = null, FeedBackText = null) {
    fncreatefeedbackToUser(FeedBackText);  
    $("#loading").addClass("flex");

    setTimeout(function () {
        var xhr = new XMLHttpRequest();
        var data = {
            'NumDniSearch': NumDniSearch,
            'RoleId': RoleId,
            'CurrentPage': CurrentPage,
            'RecordsPerPage': RecordsPerPage
        };

        xhr.open("post", "../User/GetUsersPaginate", true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.responseType = "json";
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var data = this.response;

                var tbody = "";
                var buttonEditId;
                for (let i = 0; i < data.length; i++) {
                    buttonEditId = "btnEditUser¬" + data[i].ID;
                    tbody += "<tr class='all-row'>";
                    tbody += "<td class='cell primary'>" + data[i].ID + "</td>";
                    //tbody += "<td class='cell primary'>" + data[i].CODE_LIFEMILES + "</td>";
                    tbody += "<td class='cell'>" + data[i].NAME + "</td>";
                    tbody += "<td class='cell'>" + data[i].LASTNAME_P + "</td>";
                    tbody += "<td class='cell'>" + data[i].LASTNAME_M + "</td>";
                    tbody += "<td class='cell'>" + data[i].TYPE_DOCUMENT + "</td>";
                    tbody += "<td class='cell'>" + data[i].DOCUMENT + "</td>";
                    tbody += "<td class='cell'>" + data[i].TYPE_SEGMENT + "</td>";
                    //tbody += "<td class='cell'>" + data[i].PHONE1 + "</td>";
                    //tbody += "<td class='cell'>" + data[i].EMAIL + "</td>";
                    //tbody += "<td class='cell'>" + data[i].ADDRESS + "</td>";
                    //tbody += "<td class='cell'>" + data[i].DEPARTAMENT + "</td>";
                    //tbody += "<td class='cell'>" + data[i].PROVINCE + "</td>";
                    //tbody += "<td class='cell'>" + data[i].DISTRICT + "</td>";
                    //tbody += "<td class='cell'>" + data[i].CARD.replace("-1", "") + "</td>";
                    tbody += "<td class='cell'>" + data[i].ROL + "</td>";
                    if (data[i].ADDITIONALS_PURPOSES == false) { tbody += "<td class='cell'>" + "No" + "</td>"; } else { tbody += "<td class='cell'>" + "Si" + "</td>"; }
                    tbody += "<td class='cell'>" + data[i].REGISTERED_BY + "</td>";
                    tbody += "<td class='cell'>" + moment(data[i].CREATED_AT).format('D/MM/YYYY H:mm:ss') + "</td>";
                    tbody += "<td class='cell'>" + data[i].UPDATED_BY + "</td>";
                    tbody += "<td class='cell'>" + moment(data[i].UPDATED_AT).format('D/MM/YYYY H:mm:ss') + "</td>";
                    tbody += "<td class='icon-active'>";
                    /**/
                    tbody += "<div class='box-item' onclick='fnEditUser(this.id);' id='" + buttonEditId + "'  ";
                    tbody += " data-name='" + data[i].NAME + "' ";
                    tbody += " data-lastnamep='" + data[i].LASTNAME_P + "' ";
                    tbody += " data-lastnamem='" + data[i].LASTNAME_M + "' ";
                    tbody += " data-typeDocument='" + data[i].TYPE_DOCUMENT + "' ";
                    tbody += " data-telef='" + data[i].PHONE1 + "' ";
                    tbody += " data-numDoc='" + data[i].DOCUMENT + "' ";
                    tbody += " data-dptoId='" + data[i].DEPARTAMENT_ID + "' ";
                    tbody += " data-provId='" + data[i].PROVINCE_ID + "' ";
                    tbody += " data-distId='" + data[i].DISTRICT_ID + "' ";
                    tbody += " data-email='" + data[i].EMAIL + "' ";
                    tbody += " data-address='" + data[i].ADDRESS + "' ";
                    tbody += " data-rol='" + data[i].ROL + "' ";
                    tbody += " data-gender='" + data[i].GENDER + "' ";
                    //tbody += " data-password='' ";
                    tbody += " data-FkRole='" + data[i].FK_ROLE + "' ";
                    tbody += " data-estado='" + data[i].ESTADO + "' ";
                    tbody += " data-username='" + data[i].USERNAME + "' ";
                    tbody += " data-card='" + data[i].CARD + "' ";
                    tbody += " data-companyId='" + data[i].ID_COMPANY + "' ";
                    tbody += " >";
                    tbody += "<i class='fas fa-edit' title='Editar'></i>";
                    tbody += "</div>";
                    tbody += "</td>";
                    tbody += "</tr>";
                }
                document.getElementById("tbody-users").innerHTML = tbody;
                $("#loading").removeClass("flex");
            }
        };
        xhr.send(JSON.stringify(data));
    }, 1000);
}

function fnBtnUltimo() {
    document.getElementById("btn-ultimo").addEventListener("click", function () {
        if (parseInt(document.getElementById("txt-pagina-actual").value) < parseInt(document.getElementById("lbltotalpaginas").innerText)) {
            document.getElementById("txt-pagina-actual").value = parseInt(numTotalPaginas);
            fnCrearTabla("", $("#cboRoleFilter").val(), parseInt(numTotalPaginas), parseInt(numRegistrosPorPagina), "Última página...");
            if (parseInt(document.getElementById("txt-pagina-actual").value) === numTotalPaginas)
                document.getElementById("lbl-num-registros-actual").innerText = numTotalRegistros;
            else
                document.getElementById("lbl-num-registros-actual").innerText = parseInt(document.getElementById("txt-pagina-actual").value) * parseInt(numRegistrosPorPagina);
        }
    });
}

function fnBtnPrimero() {
    document.getElementById("btn-primero").addEventListener("click", function () {
        if (parseInt(document.getElementById("txt-pagina-actual").value) > 1) {
            let primeraPagina = 1;
            document.getElementById("txt-pagina-actual").value = primeraPagina;
            fnCrearTabla("", $("#cboRoleFilter").val(), primeraPagina, parseInt(numRegistrosPorPagina), "Primera página...");
            document.getElementById("lbl-num-registros-actual").innerText = parseInt(document.getElementById("txt-pagina-actual").value) * parseInt(numRegistrosPorPagina);
        }
    });
}

function fnBtnSiguiente() {
    document.getElementById("btn-siguiente").addEventListener("click", function ()
    {
        if (parseInt(document.getElementById("txt-pagina-actual").value) < parseInt(document.getElementById("lbltotalpaginas").innerText)) {
            document.getElementById("hd-acumulador").value = parseInt(document.getElementById("txt-pagina-actual").value) + parseInt(1);
            document.getElementById("txt-pagina-actual").value = document.getElementById("hd-acumulador").value;
            fnCrearTabla("", $("#cboRoleFilter").val(), parseInt(document.getElementById("txt-pagina-actual").value), parseInt(numRegistrosPorPagina), "Siguiente página...");

            if (parseInt(document.getElementById("txt-pagina-actual").value) === numTotalPaginas) {
                document.getElementById("lbl-num-registros-actual").innerText = numTotalRegistros;
            }
            else {
                document.getElementById("lbl-num-registros-actual").innerText = parseInt(document.getElementById("txt-pagina-actual").value) * parseInt(numRegistrosPorPagina);
            }
        }
    });
}

function fnBtnAnterior() {
    document.getElementById("btn-anterior").addEventListener("click", function () {
        if (parseInt(document.getElementById("txt-pagina-actual").value) > 1) {

            document.getElementById("txt-pagina-actual").value = parseInt(document.getElementById("txt-pagina-actual").value) - parseInt(1);
            fnCrearTabla("", $("#cboRoleFilter").val(), parseInt(document.getElementById("txt-pagina-actual").value), parseInt(numRegistrosPorPagina), "Página anterior...");
            document.getElementById("lbl-num-registros-actual").innerText = parseInt(document.getElementById("txt-pagina-actual").value) * parseInt(numRegistrosPorPagina);
        }
    });
}