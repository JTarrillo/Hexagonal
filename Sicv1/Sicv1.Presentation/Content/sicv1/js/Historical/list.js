function fnInit() {
    $.ajax({
        method: "GET",
        url: "../Company/GetCompaniesByUserId",
        success: function (response) {
            var cbo = $("#cbo-alianzas-historial");
            if (response[0].FK_ROLE != 5 && response[0].FK_ROLE != 2) {
                cbo.empty();
                $("#cbo-alianzas-historial option[value='0']").remove();
                $.each(response, function () {
                    cbo.append($("<option />").val(this.ID).text(this.NAME));
                });
                fnGetHistorialAlianza($("#cbo-alianzas-historial").val());
            }
            else {
                cbo.empty();
                cbo.append('<option value="0" selected="selected">[Seleccione una alianza]</option>');
                $.each(response, function () {
                    cbo.append($("<option />").val(this.ID).text(this.NAME));
                });
                fnGetHistorialAlianza($("#cbo-alianzas-historial").val());
            }
        }
    });
}

$(function () {
    fnInit();
    $("#cbo-alianzas-historial").on("change", function () {
        fnGetHistorialAlianza($(this).val());
    });
});

function fnGetHistorialAlianza(companyId) {
    var odata = {
        'companyId': companyId
    };
    $.ajax({
        method: "GET",
        url: "../Category/GetCategoriesCodeQrByCompanyId",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: odata,
        success: function (response) {

            if (response == "" || response == null) {
                table = $('#tbl-historial').DataTable({
                    "bLengthChange": false,
                    "bFilter": false,
                    destroy: true,
                }).clear().draw();
            }

            $("#tbl-historial").DataTable({
                destroy: true,
                dom: 'Bfrtip',
                "bFilter": true,
                buttons: [{
                    extend: 'excelHtml5',
                    text: 'Exportar a excel',
                    className: 'btn btn-default',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 5, 6, 7, 9, 10, 11]
                    }
                }],
                "bLengthChange": false,
                "pageLength": 5,
                "processing": true,
                data: response,
                "sAjaxDataProp": "",
                "aoColumns": [
                    { "mData": "NAME" },                                                        //0
                    { "mData": "LASTNAME" },                                                    //1
                    { "mData": "DOCUMENT" },                                                    //2
                    { "mData": "TITLE" },                                                       //3
                    {                                                                           //
                        "mData": "IMAGE_FIRST", "mRender": function (data, type, full) {        //4
                            return "<img src=" + data + " width='70' height='50' />";           //
                        }                                                                       //
                    },                                                                          //
                    { "mData": "CODE_QR" },                                                     //5
                    { "mData": "PRICE" },                                                       //6
                    { "mData": "PERCENTAGE" },                                                  //7
                    { "mData": "DATE_USE" },                                                    //8
                    { "mData": "ALLIANCE" },                                                    //9
                    // fecha de uso                                                             //10
                    { "mData": "CODE_LIFEMILES" },                                               //11                       
                    { "mData": "QUANTITY" }
                ],
                'aoColumnDefs': [
                    {
                        'aTargets': [8], 'searchable': true, 'orderable': false, "visible": false,
                        render: function (data) {
                            return moment(data).format('MM/DD/YYYY');
                        }
                    },
                    {
                        'aTargets': [10], 'searchable': false, 'orderable': false,
                        "render": function (data, type, row) {
                            return moment(row.DATE_USE).format('DD/MM/YYYY HH:mm:ss');
                        }
                    },
                    {
                        'aTargets': [11], 'searchable': true, 'orderable': false,
                        "render": function (data, type, row) {
                            return row.CODE_LIFEMILES; //row.QUANTITY
                        }
                    },
                    {
                        'aTargets': [12], 'searchable': true, 'orderable': false,
                        "render": function (data, type, row) {
                            return row.QUANTITY;
                        }
                    }
                ]
            });
            $(".dataTables_filter input").attr("placeholder", "Buscar");
        }
    });
}
