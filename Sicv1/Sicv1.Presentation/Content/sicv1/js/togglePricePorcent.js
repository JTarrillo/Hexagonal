function toggleLeft(toggleId, ctrlId, text) {
    //price
    document.getElementById(toggleId).classList.remove("toggle-status-on");

    //guardo el porcentaje en el hidden
    $("#hdPorcentEdit").val($("#txtValPorcentPrice").val());
    $("#hdPorcentAdd").val($("#txtValPorcentPriceAdd").val());

    $("#txtValPorcentPrice").val("");
    $("#txtValPorcentPriceAdd").val("");
    $("#txtValPorcentPrice").focus();
    $("#txtValPorcentPriceAdd").focus("");

    $(ctrlId).text(text);

    $("#txtValPorcentPrice").val($("#hdPriceEdit").val());
    $("#txtValPorcentPriceAdd").val($("#hdPriceAdd").val());
}

function toggleRight(toggleId, ctrlId, text) {
    //percentage
    document.getElementById(toggleId).classList.add("toggle-status-on");

    //guardo el precio en el hidden
    $("#hdPriceEdit").val($("#txtValPorcentPrice").val());
    $("#hdPriceAdd").val($("#txtValPorcentPriceAdd").val());

    $("#txtValPorcentPrice").val("");
    $("#txtValPorcentPriceAdd").val("");
    $("#txtValPorcentPrice").focus();
    $("#txtValPorcentPriceAdd").focus("");

    $(ctrlId).text(text);

    $("#txtValPorcentPrice").val($("#hdPorcentEdit").val());
    $("#txtValPorcentPriceAdd").val($("#hdPorcentAdd").val());
}