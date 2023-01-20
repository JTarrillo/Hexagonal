function fn(controllerName = null) {
    if (controllerName == "H".trim()) { //Historial
        fnInit();
    }
    else if (controllerName == "A".trim()) { //Agenda
        fnGetSchedule(0);
    }
    else if (controllerName == "J".trim()) { //Jerarquía de cupones
        fnloadData();
    }
    else if (controllerName == "U".trim()) {

    }
}

function fnIniDateRange(fini = null, ffin = null, dataTableName = null, colIndex = 0) {
    $.fn.dataTable.ext.search.push(
        function (settings, data, dataIndex) {

            if (settings.nTable.id !== dataTableName) {
                return true;
            }

            var min = $(fini).datepicker("getDate");
            var max = $(ffin).datepicker("getDate");

            var startDate = new Date(data[colIndex]);
            if (min == null && max == null) { return true; }
            if (min == null && startDate <= max) { return true; }
            if (max == null && startDate >= min) { return true; }
            if (startDate <= max && startDate >= min) { return true; }
            return false;
        }
    );
}

function fndtPickerStart(dtPickerId, controllerName = null, dtPickerIdEnd = null) {
    if ($(dtPickerId).val() != "" && $(dtPickerIdEnd).val() != "") {
        $(dtPickerId).datepicker
            (
                {
                    dateFormat: 'dd/mm/yy',
                    onSelect: function (datetext) {
                        $(dtPickerId).val(datetext);
                        fn(controllerName);
                    },
                    defaultDate: new Date(),
                    changeMonth: true,
                    changeYear: true
                }
            );
    }
}

function fndtPickerEnd(dtPickerId, controllerName = null, dtPickerIdStart = null) {

    if ($(dtPickerId).val() != "" && $(dtPickerIdStart).val() != "") {
        $(dtPickerId).datepicker(
            {
                dateFormat: 'dd/mm/yy',
                onSelect: function (datetext) {
                    $(dtPickerId).val(datetext);
                    fn(controllerName);
                },
                defaultDate: new Date(),
                changeMonth: true,
                changeYear: true
            });
    }
}

function fnCancelSearchbyDates(buttonId = null, dtps, controllerName = null) {
    $(buttonId).on("click", function () {
        $(dtps).val("");
        fn(controllerName);
    });
}

function fnDefaultDate(dtpIni, dtpFin) {
    var d = new Date();
    var currMonth = d.getMonth();
    var currYear = d.getFullYear();
    var currDay = d.getDate();
    var startDate = new Date(currYear, currMonth, currDay);

    $(dtpIni).datepicker({ dateFormat: 'dd/mm/yy'});
    $(dtpIni).datepicker("setDate", startDate);
    $(dtpFin).datepicker({ dateFormat: 'dd/mm/yy',});
    $(dtpFin).datepicker("setDate", startDate);

}

$(function () {
    fnsetLanguage();
});

function fnsetLanguage() {
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '< Ant',
        nextText: 'Sig >',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);
}
