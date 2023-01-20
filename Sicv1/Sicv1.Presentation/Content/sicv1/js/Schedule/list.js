function fnGetSchedule(companyId) {
    var table;
    var oData = {
        'companyId': companyId
    };

    $.ajax({
        method: "GET",
        url: "../Schedule/GetSchedule",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: oData,
        success: function (response) {
            if (response == "" || response == null) {
                table = $('#tbl').DataTable({
                    "bLengthChange": false,
                    "bFilter": false,
                    destroy: true,
                }).clear().draw();
            }
            else {

                table = $("#tbl").DataTable({
                    destroy: true,
                    dom: 'Bfrtip',
                    buttons: [
                        {
                            extend: 'excelHtml5',
                            customize: function (xlsx) {
                                var sheet = xlsx.xl.worksheets['sheet1.xml'];
                                // Loop over the cells in column `C`
                                $('row c[r^="C"]', sheet).each(function () {
                                    // Get the value
                                    if ($('is t', this).text() == 'Agenda') {
                                        $(this).attr('s', '20');
                                    }
                                });
                            },
                            text: 'Exportar a excel',
                            className: 'btn btn-default',
                            exportOptions:
                            {
                                columns: [0, 1, 2, 3, 4, 5, 7]
                            }
                        }],
                    "bFilter": true,
                    "bLengthChange": false,
                    "pageLength": 10,
                    "processing": true,
                    "sAjaxDataProp": "",
                    data: response,
                    "aoColumns":
                        [
                            { "mData": "NAME" },
                            { "mData": "LASTNAME" },
                            { "mData": "DOCUMENT" },
                            { "mData": "PHONE" },
                            { "mData": "CUPON" },
                            { "mData": "COMPANY" },
                            { "mData": "CREATED_AT" }
                        ],
                    'aoColumnDefs':
                        [
                            {
                                'aTargets': [6], "visible": false,
                                render: function (data) {
                                    return moment(data).format('MM/DD/YYYY');
                                }
                            }
                            ,
                            {
                                'aTargets': [7], 'searchable': false, 'orderable': false,
                                "render": function (data, type, row) {
                                    return moment(row.CREATED_AT).format('DD/MM/YYYY');
                                },
                            }
                        ]
                });
                $(".dataTables_filter input").attr("placeholder", "Buscar");
            }
        }
    });
}