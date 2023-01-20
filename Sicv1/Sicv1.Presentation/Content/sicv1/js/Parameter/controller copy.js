let dataList = [];
let dataListChildren = [];
oTable = $('.box-table').DataTable({
	"paging": true,
	"info": true,
	searching: true,
	responsive: true,
	"autoWidth": true,
	"dom": 'tp',
	language: {
		"sProcessing": "Procesando...",
		"sLengthMenu": "Mostrar _MENU_ registros",
		"sZeroRecords": "No se encontraron resultados",
		"sEmptyTable": "Ningún dato disponible en esta tabla",
		"sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_",
		"sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0",
		"sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
		"sInfoPostFix": "",
		"sSearch": "Buscar:",
		"sUrl": "",
		"sInfoThousands": ",",
		"sLoadingRecords": "Cargando...",
		"oPaginate": {
			"sFirst": "Primero",
			"sLast": "Último",
			"sNext": "Siguiente",
			"sPrevious": "Anterior"
		},
		"oAria": {
			"sSortAscending": ": Activar para ordenar la columna de manera ascendente",
			"sSortDescending": ": Activar para ordenar la columna de manera descendente"
		}

	},
	columns: [
		{
			width: "50",
			data: "ID",
			title: "Id",
			className: "cell text-center"
		},
		{
			//width: "150",
			data: "NAME",
			title: "Nombre",
			className: "cell"
		},
		{
			width: "150",
			data: "DESCRIPTION",
			title: "Descripcion",
			className: "cell"

		},
		{
			//width: "150",
			data: "VALUE",
			title: "Valor",
			className: "cell"

		},
		{
			width: "50",
			data: "STATUS",
			title: "Estado",
			className: "cell text-center",
			render: function (data, type, row, meta) {
				return data ? '<span class="badge badge-info p-2">Activo</span>' : '<span class="badge badge-danger p-2">Inactivo</span>';

			}
		},
		{
			width: "150",
			data: "ID",
			title: "Acciones",
			className: "cell text-center",
			render: function (data, type, row, meta) {
				if (row.STATUS) {
					return '<button type="button" title="Editar Parametro" onclick=editModal(' + data + ') class="btn btn-orange btn-sm"><i class="fas fa-edit"></i></button>'
						+ ' <button type="button" title="Eliminar Parametro" onclick=ChangeStatus(' + data + ',0) class="btn btn-pink btn-sm"><i class="fas fa-times"></i></button>'
						+ ' <button type="button" title="Agregar Item" onclick=addItemModal(' + data + ') class="btn btn-green btn-sm"><i class="fas fa-plus"></i></button>';
				} else {
					return '<button type="button" onclick=ChangeStatus(' + data + ',1) class="btn btn-success btn-sm"><i class="fas fa-check"></i></button>';
				}

			}
		},

	],
});


oTable2 = $('.box-table-t').DataTable({
	"paging": true,
	"info": true,
	searching: true,
	responsive: true,
	"autoWidth": true,
	"dom": 'tp',
	language: {
		"sProcessing": "Procesando...",
		"sLengthMenu": "Mostrar _MENU_ registros",
		"sZeroRecords": "No se encontraron resultados",
		"sEmptyTable": "Ningún dato disponible en esta tabla",
		"sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_",
		"sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0",
		"sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
		"sInfoPostFix": "",
		"sSearch": "Buscar:",
		"sUrl": "",
		"sInfoThousands": ",",
		"sLoadingRecords": "Cargando...",
		"oPaginate": {
			"sFirst": "Primero",
			"sLast": "Último",
			"sNext": "Siguiente",
			"sPrevious": "Anterior"
		},
		"oAria": {
			"sSortAscending": ": Activar para ordenar la columna de manera ascendente",
			"sSortDescending": ": Activar para ordenar la columna de manera descendente"
		}

	},
	columns: [
		{
			width: "50",
			data: "ID",
			title: "Id",
			className: "cell text-center"
		},
		{
			//width: "150",
			data: "NAME",
			title: "Nombre",
			className: "cell"
		},
		{
			width: "150",
			data: "DESCRIPTION",
			title: "Descripcion",
			className: "cell"

		},
		{
			//width: "150",
			data: "VALUE",
			title: "Valor",
			className: "cell"

		},
		{
			width: "50",
			data: "STATUS",
			title: "Estado",
			className: "cell text-center",
			render: function (data, type, row, meta) {
				return data ? '<span class="badge badge-info p-2">Activo</span>' : '<span class="badge badge-danger p-2">Inactivo</span>';

			}
		},
		{
			width: "150",
			data: "ID",
			title: "Acciones",
			className: "cell text-center",
			render: function (data, type, row, meta) {
				if (row.STATUS) {
					return '<button type="button" title="Editar Parametro" onclick=editModalChildren(' + data + ') class="btn btn-orange btn-sm"><i class="fas fa-edit"></i></button>'
						+ ' <button type="button" title="Eliminar Parametro" onclick=ChangeStatusChildren(' + data + ',0) class="btn btn-pink btn-sm"><i class="fas fa-times"></i></button>';
				} else {
					return '<button type="button" onclick=ChangeStatusChildren(' + data + ',1) class="btn btn-success btn-sm"><i class="fas fa-check"></i></button>';
				}

			}
		},

	],
});

$(function () {

	getDataParameter();
	getCompanies();

	$("#search").on("keyup search input paste cut change", function () {
		//console.log(this.value);
		oTable.search(this.value).draw();
	});

	$("#btn-cancelar-busqueda").on("click", function () {
		//console.log(this.value);
		$("#search").val('');
		oTable.search('').draw();
	});

	$("#btn-add-parameter").on("click", function () {
		fncreatefeedbackToUser("Cargando agregar usuario...");
		$("#loading").addClass("flex");

		resetModal();
		
		estado = 0;
		$("#updateOsave").val(estado);
		$("#btnGrabarAddParameter").html('Grabar');
		$("#modalParameter").show();
		$("#loading").removeClass("flex");
	});
});

	//EVENTOS CERRAR MODAL
	$(document).on("click", "#btn-x, #btn-close, #btn-close-Add", function () {
		$("#modalParameter").hide();
		$("#modalAddItemParameter").hide();
		$(".bd-status").removeClass('on-modal');
	});

	$(document).on("click", "#btn-x-children, #btn-close-children", function () {
		$("#modalAddChildren").hide();
	});


$(document).on("click", "#btnGrabarAddParameter", function () {

	//alert($("#cboCompany").val());

	if ($("#cboCompany").val() == "" || $("#cboCompany").val() == null )
	{
		alert('Seleccione Empresa');
		$("#cboCompany").focus();
		return;
	}

	if ($("#nombre_parametro").val() == "") {
		alert('Ingrese Nombre Parametro');
		$("#nombre_parametro").focus();
		return;
	}

	if ($("#descrip_parametro").val() == "") {
		alert('Ingrese Descripcion');
		$("#descrip_parametro").focus();
		return;
	}

	
	var odata = {
		"ID": $("#idParameter").val(),
		"NAME": $("#nombre_parametro").val(),
		"DESCRIPTION": $("#descrip_parametro").val(),
		"VALUE": $("#valor_parametro").val(),
		//"UPDATED_USER": -1,
		"COMPANY_ID": $("#cboCompany").val(),
		"SAVE_OR_UPDATE": $("#updateOsave").val()
	};
	
	$.ajax({
		method: "POST",
		url: "../Parameter/SaveParameter",
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: JSON.stringify(odata),
		success: function (response) {
			if (response.code == 800) {
				alert('Error');
				return;
			}

			if (response.code == 200 && response.status == true) {
				$("#modalParameter").hide();
				if ($("#updateOsave").val() == "0") {
					alert('SE GUARDO PARAMETRO' + response.data);
				} else {
					alert('SE ACTUALIZO PARAMETRO' + response.data);
                }
				
				getDataParameter();
				//fnCrearTabla("", "", 1, 10, "Grabando...");
			}
		}
	});
});

$(document).on("click", "#btnGrabarAddChildren", function () {

	if ($("#nombre_parametro_hijo").val() == "") {
		alert('Ingrese Nombre Hijo');
		$("#nombre_parametro").focus();
		return;
	}

	if ($("#descrip_parametro_hijo").val() == "") {
		alert('Ingrese Descripcion Hijo');
		$("#descrip_parametro_hijo").focus();
		return;
	}

	if ($("#valor_parametro_hijo").val() == "") {
		alert('Ingrese Valor Hijo');
		$("#valor_parametro_hijo").focus();
		return;
	}


	var odata = {
		"ID": $("#idParameter3").val(),
		"ID_PADRE": $("#idParameter2").val(),
		"NAME": $("#nombre_parametro_hijo").val(),
		"DESCRIPTION": $("#descrip_parametro_hijo").val(),
		"VALUE": $("#valor_parametro_hijo").val(),
		//"UPDATED_USER": -1,
		"COMPANY_ID": $("#companyId").val(),
		"SAVE_OR_UPDATE": $("#updateOsave2").val()
	};

	$.ajax({
		method: "POST",
		url: "../Parameter/SaveParameter",
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: JSON.stringify(odata),
		success: function (response) {
			if (response.code == 800) {
				alert('Error');
				return;
			}

			if (response.code == 200 && response.status == true) {
				$("#modalAddChildren").hide();
				if ($("#updateOsave2").val() == "0") {
					alert('SE GUARDO PARAMETRO' + response.data);
				} else {
					alert('SE ACTUALIZO PARAMETRO' + response.data);
				}

				getDataParameterChildren($("#idParameter2").val());
				//fnCrearTabla("", "", 1, 10, "Grabando...");
			}
		}
	});
});

const getCompanies = async () => {

	fetch('../Company/listCompanies', {
		method: 'POST',
		//body: JSON.stringify(odata),
		headers: {
			'Content-Type': 'application/json'
		}
	})
		.then(response => response.json())
		.then(data => {
			
			var numRows = data.length;
			if (numRows == 1) {
				data.map(x => {
					$(".cboCompany").append(`<option selected value="${x.ID}">${x.NAME}</option>`);
				})
			} else {
				//<option value="" disabled selected>[Seleccione]</option>
				$(".cboCompany").append(`<option value="" selected>Seleccione</option>`)
				data.map(x => {
					$(".cboCompany").append(`<option value="${x.ID}">${x.NAME}</option>`);
				})
            }
			
		})
}

const getDataParameter = async () => {

	dataList = [];

	fetch('../Parameter/GetParameter', {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json'
		}
	})
		.then(response => response.json())
		.then(data => {
			
			data.map(x => {
				dataList.push({
					ID: x.ID,
					NAME: x.NAME,
					DESCRIPTION: x.DESCRIPTION,
					VALUE: x.VALUE,
					COMPANY_ID: x.COMPANY_ID,
					STATUS: x.STATUS
				});
			})
			oTable.clear().draw();
			oTable.rows.add(dataList).draw();
			console.log(dataList)

		})
}

const getDataParameterChildren = async (id) => {
	
	dataListChildren = [];

	var odata = {
		'ID_PADRE': id
	};

	fetch('../Parameter/GetParameter', {
		method: 'POST',
		body: JSON.stringify(odata),
		headers: {
			'Content-Type': 'application/json'
		}
	})
		.then(response => response.json())
		.then(data => {

			data.map(x => {
				dataListChildren.push({
					ID: x.ID,
					NAME: x.NAME,
					DESCRIPTION: x.DESCRIPTION,
					VALUE: x.VALUE,
					COMPANY_ID: x.COMPANY_ID,
					STATUS: x.STATUS
				});
			})
			oTable2.clear().draw();
			oTable2.rows.add(dataListChildren).draw();
			console.log(dataListChildren)

		})
}


const btnAddItem = () => {
	
	resetModalAddItem();
	
	$("#modal-title-children").html('Agregar Parametro Hijo');
	$("#btnGrabarAddChildren").html('Grabar');
	$("#modalAddChildren").show();
}


const editModal = (id) => {
	
	resetModal();
	estado = 1;

	let found = dataList.find(x => x.ID == id);

	$("#modal-title").html('Editar Parametro Hijo');
	$("#btnGrabarAddParameter").html('Actualizar');

	$("#cboCompany").val(found.COMPANY_ID);
	
	$("#updateOsave").val(estado);
	$("#idParameter").val(found.ID);
	$("#nombre_parametro").val(found.NAME);
	$("#descrip_parametro").val(found.DESCRIPTION);
	$("#valor_parametro").val(found.VALUE);
	$("#modalParameter").show();
	
}

const addItemModal = (id) => {

	//resetModalAddItem();
	estado = 0;
	let found = dataList.find(x => x.ID == id);

	$("#modal-title-item").html('Agregar Item Parametro Padre: ' + found.NAME);
	$("#idParameter2").val(found.ID);
	$("#companyId").val(found.COMPANY_ID);
	$("#updateOsave2").val(estado);
	getDataParameterChildren(found.ID);

	$("#modalAddItemParameter").show();

}

const editModalChildren = (id) => {
	
	resetModalAddItem();
	estado = 1;

	let foundChildren = dataListChildren.find(x => x.ID == id);

	$("#modal-title-children").html('Editar Parametro Hijo');
	$("#btnGrabarAddChildren").html('Actualizar');

	$("#updateOsave2").val(estado);
	$("#idParameter3").val(foundChildren.ID);
	$("#nombre_parametro_hijo").val(foundChildren.NAME);
	$("#descrip_parametro_hijo").val(foundChildren.DESCRIPTION);
	$("#valor_parametro_hijo").val(foundChildren.VALUE);

	$("#modalAddChildren").show();

}


const ChangeStatus = (id, status) => {

	var odata = {
		'ID': id,
		'STATUS': status
	};	
	
	fetch('../Parameter/DeleteParameter', {
		method: 'POST',
		body: JSON.stringify(odata),
		headers: {
			'Content-Type': 'application/json'
		}
	})
		.then(response => response.json())
		.then(data => {
			
			getDataParameter();
		})
}

const ChangeStatusChildren = (id, status) => {
	
	var odata = {
		'ID': id,
		'STATUS': status
	};
	
	fetch('../Parameter/DeleteParameter', {
		method: 'POST',
		body: JSON.stringify(odata),
		headers: {
			'Content-Type': 'application/json'
		}
	})
		.then(response => response.json())
		.then(data => {

			getDataParameterChildren($("#idParameter2").val());
		})
}

const resetModal = () => {
	$("#nombre_parametro").val("");
	$("#descrip_parametro").val("");
	$("#valor_parametro").val("");
	
	if ($("#idUserRole").val() == 1) {
		$("#cboCompany").val("");
    }
	
}

const resetModalAddItem = () => {
	$("#nombre_parametro_hijo").val("");
	$("#descrip_parametro_hijo").val("");
	$("#valor_parametro_hijo").val("");
	
}




