$(function () {
    fnrolesList();
});

function fnrolesList() {
    $.ajax({
        method: "GET",
        url: "../Role/GetRoles",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if (response == "" || response == null) {
            } else {

                    $("#tbl_roles").DataTable({
                        "destroy": true,
                        dom: 'Bfrtip',
                        "bFilter": true,
                        buttons: [{
                            extend: 'excelHtml5', text: 'Exportar a excel', className: 'btn btn-default'
                        }],
                        "bLengthChange": false,
                        "pageLength": 5,
                        "processing": true,
                        data: response,
                        "sAjaxDataProp": "",
                        "aoColumns":
                            [
                                { "mData": "ID" },
                                { "mData": "NAME" },
                                { "mData": "DESCRIPTION" },
                                {
                                    "mData": "ESTADO",
                                    "mRender": function (data, type, full) {
                                        if (data == 1) {
                                            return "Activo";
                                        }
                                        else {
                                            return "Inactivo";
                                        }
                                        return result
                                    }
                                }
                            ],
                        "aoColumnDefs": [
                            {
                                "targets": 4,
                                "data": null,
                                "mRender": function (data, type, full) {
                                    var id = "¬" + data.ID
                                    var buttons = "<input type='button' id='btnEditRole" + id + "' value='Editar' class='btn btn-orange' />&nbsp;";
                                    return buttons
                                }
                            }
                        ]
                    });
                    $(".dataTables_filter input").attr("placeholder", "Buscar");
            }
        }
    });
}
