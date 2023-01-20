function fnloadData() {
    $.ajax({
        url: "../CategoryCodeQr/GetCategoriesHistoricalDetail",
        method: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if (response == "" || response == null) {
            }
            else {

                $("#tbl_hierarchy").DataTable({
                    "destroy": true,
                    dom: 'Bfrtip',
                    "bFilter": true,
                    buttons: [{
                        extend: 'excelHtml5',
                        text: 'Exportar a excel',
                        className: 'btn btn-orange',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 8]
                        }
                    }],
                    "bLengthChange": false,
                    "pageLength": 5,
                    "processing": true,
                    data: response,
                    "sAjaxDataProp": "",
                    "aoColumns":
                        [
                            { "mData": "LEVEL_3" },
                            { "mData": "LEVEL_2" },
                            { "mData": "LEVEL_1" },
                            { "mData": "COUPON_ID" },
                            { "mData": "TITLE" },
                            { "mData": "ALLIANCE_NAME" },
                            { "mData": "NUMBER_OF_TIMES_REDEEMED" },
                            { "mData": "REDEMPTION_DATE" }
                        ],
                    'aoColumnDefs':
                        [
                            { 'aTargets': [0], 'orderable': false },
                            { 'aTargets': [1], 'orderable': false },
                            { 'aTargets': [2], 'orderable': false },
                            { 'aTargets': [3], 'visible': false },
                            {
                                'aTargets': [9],
                                "render": function (data, type, row) {
                                    var contenido = " <input type='button' id='btn-hierarchy'  ";
                                    contenido += " data-couponId= " + row.COUPON_ID + " ";
                                    contenido += " class='btn btn-orange'  value = 'Ver detalle'  ";
                                    contenido += " />";
                                    return contenido;
                                }
                            },
                            {
                                'aTargets': [7], 'searchable': true, 'orderable': false, "visible": false,
                                render: function (data) {
                                    return moment(data).format('MM/DD/YYYY');
                                }
                            },
                            {
                                'aTargets': [8], 'searchable': false, 'orderable': false, "visible": false,
                                "render": function (data, type, row) {
                                    return moment(row.REDEMPTION_DATE).format('DD/MM/YYYY');
                                    //return moment(row.REDEMPTION_DATE).format('DD/MM/YYYY');
                                }
                            }
                        ]
                });
                $(".dataTables_filter input").attr("placeholder", "Buscar");
            }
        }
    });
}

$(function () {

    $("#min_date_partial, #max_date_partial").on("click", function () {
        $(".modal-open").css("z-index", "2000");
        $(".ui-datepicker").css("z-index", "2000");

        $(".modal-open").css("top", "175.188");
        $(".ui-datepicker").css("z-index", "175.188");
    });

    $("#btn-verDetHistorial").on("click", function () {
        $("#btn-cancelar-partial").click();

        //JERARQUÍA
        fnIniDateRange("#min_date_partial", "#max_date_partial", "tbl_hierarchy", 7);
        fndtPickerStart("#min_date_partial", "J");
        fndtPickerEnd("#max_date_partial", "J");
        fnCancelSearchbyDates("#btn-cancelar-partial", "#min_date_partial, #max_date_partial", "J");
        fnloadData();
        fnDefaultDate("#min_date_partial", "#max_date_partial");

        $("#hierarchyModal").modal();
    });

    $(function () {
        $("#btn_searchByDateRange_partial").on("click", function () {
            fnIniDateRange("#min_date_partial", "#max_date_partial", "tbl_hierarchy", 7);
            var myDataTable = $('#tbl_hierarchy').DataTable();
            myDataTable.draw();
        });
    });

});

$(document).on("click", "#btn-hierarchy", function () {
    var v_couponId = $(this).attr("data-couponId");
    var obj = {
        'CouponId': v_couponId
    };
    $.ajax({
        method: "POST",
        url: "/CategoryCodeQr/GetHierarchyCouponDetail",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(obj),
        success: function (d) {
            var contenido = "";
            for (var i = 0; i < d.length; i++) {
                contenido += "<tr>";
                contenido += "<td>" + d[i].EXCHANGE_USER + "</td>";
                contenido += "<td>" + d[i].ALLIANCE_USER + "</td>";
                contenido += "<td>" + moment(d[i].CREATED_AT).format("DD/MM/YYYY HH:mm:ss") + "</td>";
                contenido += "</tr>";
                $("#tbl_hierarchyDetail").find("tr:not(:first)").remove();
                $('#tbl_hierarchyDetail > tbody:first').append(contenido);
            }
        }
    });


    $("#DetailModal").css("z-index", "1070");
    $("#DetailModal").modal();
    document.getElementsByClassName("modal-backdrop")[1].style = "z-index:1060";
});