$(function () {
    fncreatefeedbackToUser("Cargando dashboard...", 1);
    fncompaniesList();
});

function fncompaniesList() {
    $("#loading").addClass("flex");
    $.ajax({
        method: "GET",
        url: "../Company/List",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if (response == "" || response == null) {
            } else {
                for (var i = 0; i < response.length; i++) {
                    fnEditCompany(response[i].ID);
                }

                $("#tbl_alianzas").DataTable({
                    "destroy": true,
                    dom: 'Bfrtip',
                    "bFilter": true,
                    buttons: [{
                        extend: 'excelHtml5', text: 'Exportar a excel', className: 'btn btn-default'
                    }],
                    "bLengthChange": false,
                    "pageLength": 8,
                    "processing": true,
                    data: response,
                    "sAjaxDataProp": "",
                    "aoColumns":
                        [
                            { "mData": "ID" },
                            { "mData": "NAME" },
                            {
                                "mData": "LOGO", "mRender": function (data, type, full) {
                                    return "<img src='" + data + "' width='70' height='50' />"
                                }
                            },
                            { "mData": "RUC" },
                            {
                                "mData": "URL",
                                "mRender": function (data, type, full) {
                                    return "<a title='" + full.NAME + "' href='" + data + "' target='_blank'><i class='fab f-14 fa-facebook-square'></i></a>"
                                }
                            },
                            { "mData": "PHONE" },
                            {
                                "mData": "STATUS",
                                "mRender": function (data, type, full) {
                                    if (data == true) {
                                        return "Activo";
                                    }
                                    else {
                                        return "Inactivo";
                                    }
                                    return result
                                }
                            },                            
                            // {
                            //     "mData": "LIFEMILES_PARTICIPATES_CAMPAIGN",
                            //     "mRender": function (data, type, full) {
                            //         if (full.LIFEMILES_PARTICIPATES_CAMPAIGN == 1) {
                            //             return "<input disabled type='checkbox' name='name' checked  />"
                            //         }
                            //         else {
                            //             return "<input disabled type='checkbox' name='name' disable />"
                            //         }
                            //     }
                            // },
                            // {
                            //     "mData": "Latitude",
                            //     className: "d-none"
                                
                            // },
                            // {
                            //     "mData": "Longitude",
                            //     className: "d-none"

                            // },
                            // {
                            //     "mData": "Direction",
                            //     className: "d-none"

                            // },
                            // {
                            //     "mData": "Country",
                            //     className: "d-none"

                            // },
                        ],
                    "aoColumnDefs": [
                        {
                            "targets":7,
                            "data": null,
                            "mRender": function (data, type, full) {
                                var id = "¬" + data.ID
                                // var buttons = "<div class='io-row'>" +
                                //     "<div id='btnEdit" + id + "' class='btn btn-orange'>Editar</div>" +
                                //     "<div class='io-divider vertical'></div>" + popConfirm(data.ID) +
                                //     "<div class='io-divider vertical'></div>"+
                                //     "<div id='btnCopy' onClick='executeCopy("+data.ID+")' class='btn btn-primary'>Copiar código</div>" +
                                //     "</div>";
                                let buttons = `<div class="btn-group">
                                <button type="button" class="btn btn-info dropdown-toggle" 
                                data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-settings" width="24" height="24" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                                <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                                <path d="M10.325 4.317c.426 -1.756 2.924 -1.756 3.35 0a1.724 1.724 0 0 0 2.573 1.066c1.543 -.94 3.31 .826 2.37 2.37a1.724 1.724 0 0 0 1.065 2.572c1.756 .426 1.756 2.924 0 3.35a1.724 1.724 0 0 0 -1.066 2.573c.94 1.543 -.826 3.31 -2.37 2.37a1.724 1.724 0 0 0 -2.572 1.065c-.426 1.756 -2.924 1.756 -3.35 0a1.724 1.724 0 0 0 -2.573 -1.066c-1.543 .94 -3.31 -.826 -2.37 -2.37a1.724 1.724 0 0 0 -1.065 -2.572c-1.756 -.426 -1.756 -2.924 0 -3.35a1.724 1.724 0 0 0 1.066 -2.573c-.94 -1.543 .826 -3.31 2.37 -2.37c1 .608 2.296 .07 2.572 -1.065z" />
                                <circle cx="12" cy="12" r="3" />
                              </svg>
                                </button>
                                <div class="dropdown-menu">
                                  <a class="dropdown-item" href="#">Editar</a>               
                                  <div class="dropdown-divider"></div>
                                  <a class="dropdown-item">Eliminar</a>
                                </div>
                              </div>`;
                                return buttons
                            }
                        }
                    ]
                });
                $(".dataTables_filter input").attr("placeholder", "Buscar");
            }
        },
        complete: function () {
            $("#loading").removeClass("flex");
        },
    });
    
}

var executeCopy = function (value) {
    var copyhelper = document.createElement("input");
    copyhelper.className = 'copyhelper'
    document.body.appendChild(copyhelper);
    copyhelper.value = value;
    copyhelper.select();
    document.execCommand("copy");
    document.body.removeChild(copyhelper);
    fncreateAlert("Código de empresa copiado", "success");
};

function popConfirm(id) {
    return `
        <div class="io-popconfirm bottom-right">
            <div class="io-mask" onclick="removeTooltip()"></div>
            <div class='btn btn-pink' onclick="openTooltip(this)">Eliminar</div>
            <div class="io-popover" style="width: 150px">
                <div class="io-popover-message">
                    <svg style="margin-right: .4rem" viewBox="64 64 896 896"  width="1em" height="1em" fill="var(--Yellow)">
                        <path d="M512 64C264.6 64 64 264.6 64 512s200.6 448 448 448 448-200.6 448-448S759.4 64 512 64zm-32 232c0-4.4 3.6-8 8-8h48c4.4 0 8 3.6 8 8v272c0 4.4-3.6 8-8 8h-48c-4.4 0-8-3.6-8-8V296zm32 440a48.01 48.01 0 010-96 48.01 48.01 0 010 96z"></path>
                    </svg>
                    <div class="io-popover-message-title">¿Está seguro?</div>
                </div>
                <div class="io-popover-message-content">
                    <input type="checkbox" />
                    <div class="io-divider vertical"></div>
                    <span>Eliminar permanente</span>
                </div>
                <div class="io-popover-buttons">
                    <button type="button" class="io-btn gosth small" style="margin-right: .4rem" onclick="removeTooltip()">No</button>
                    <button type="button" class="io-btn gosth small blue" onclick="successDelete()">Si</button>
                </div>
            </div>
        </div>`;
}