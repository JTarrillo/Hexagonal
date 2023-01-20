function fnbtnBuscar() {
    document.getElementById("btn-buscar").addEventListener("click", function () {
        if ($("#txt-buscar").val() == "") {
            fncreateAlert("Ingrese Nro. de documento");
            $("#txt-buscar").focus();
            return;
        }
        fnCrearTabla(document.getElementById("txt-buscar").value, "", 1, numRegistrosPorPagina, "Buscando...");
    });
}

function fnbtnCancelarBusqueda() {
    $("#btn-cancelar-busq").on("click", function () {
        fnNumTotalRegistros(null);
        $("#txt-buscar").val("");
        fnCrearTabla(document.getElementById("txt-buscar").value, "", parseInt(document.getElementById("txt-pagina-actual").value), numRegistrosPorPagina, "Cargando...");
        $("#cboRoleFilter").val("");
    });
}