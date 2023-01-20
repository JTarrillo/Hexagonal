let data = {};
let dataList = [];
let dataListChildren = [];
let estado = 0;
let idParameter = 0;

oTable = $('.box-table').DataTable({
	createdRow: function( row, data, dataIndex ) {
        // Set the data-status attribute, and add a class
        $(row).addClass("all-row");
    },
    buttons: [{
		extend: 'excel',
		className: "d-none",
		exportOptions: {
			columns: [0, 1, 2, 3]
		}
	}],
	"paging": true,
	"info": true,
	searching: true,
	responsive: true,
	"autoWidth": true,
	"dom": 'Bitp',
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
		{	data: "ID", targets:0, className: "cell"	},
		{   data: "NAME",targets:1, className: "cell" },
		{   data: "DESCRIPTION",targets:3, className: "cell" },
		{   data: "VALUE",targets:4, className: "cell" },
		{   
			data: "STATUS",
			targets:5, 
			className: "cell",
			render :  function (data, type, row, meta) {
				if(data==1){
					return '<span class="text-primary">ACTIVO</span>';
				}else{
					return '<span class="">INACTIVO</span>';
				}

			}
		},
		{
			targets:5,
			className: "cell text-center",
			render: function (data, type, row, meta) {
			
				return `<a role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">				
							<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-dots" width="24" height="24" viewBox="0 0 24 24" stroke-width="1.5" stroke="#9e9e9e" fill="none" stroke-linecap="round" stroke-linejoin="round">
								<path stroke="none" d="M0 0h24v24H0z" fill="none"/>
								<circle cx="5" cy="12" r="1" />
								<circle cx="12" cy="12" r="1" />
								<circle cx="19" cy="12" r="1" />
							</svg>
						</a><div class="dropdown-menu" aria-labelledby="dropdownMenuButton">

						<a role"button" onClick="editModal(${row.ID})" class="dropdown-item"><i class="fas fa-pen text-danger"></i> Editar</a>

						<a role"button" onClick="createModalItems(${row.ID})" class="dropdown-item"><i class="fas fa-plus text-success"></i> Agregar Item</a>						
						<div class="dropdown-divider"></div>

						<a role"button" onClick="changeStatus(${row.ID})" class="dropdown-item"><i class="fas ${row.STATUS==1?'fa-ban text-danger':'fa-check text-success'}"></i> ${row.STATUS==1?'Inhabilitar':'Habilitar'}</a>
					  </div>`;
			}
		}

	],
});


$(function () {

	getDataParameter();
	getCompanies();

	$("#search").on("keyup search input paste cut change", function () {
		//console.log(this.value);
		oTable.search(this.value).draw();
	});

	$("#btn-cancelar-busq").on("click", function () {
		//console.log(this.value);
		$("#search").val('');	
		oTable.search('').draw();
	});

	$("#btn-modal-params").on('click',()=>{
		createModal();
	})	

	$(document).on("click", "#btn-save-add", function () {
		if(estado==0){
			save();
		}else{
			update();
		}				
	});
	
});


const getDataParameter = async () => {

	dataList = [];

	requestAll(API.PARAMETERS.LIST)
        .then((res)=>{
			
			dataList = [];
			dataList = res.data;
            $("#timeServer").html(res.timeStamp)
			oTable.clear().draw();
			oTable.rows.add(dataList).draw();			
        })
        .catch((err)=>{
            Toastito.fire({
                icon: 'error',
                title: err.response
            })
        });
}

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

const changeStatus = async(id)=>{
	try {				

		requestAll(API.PARAMETERS.CHANGE_STATUS, {id:id})
        .then((res)=>{
            

			//COMO MODIFICIAR UNA SELDA DE DATATABLES
            if(res.status){ 

				let found = dataList.find(x=>x.ID == id); 
				
				found["STATUS"] 	= 	(found.STATUS==1)?0:1;

				oTable.clear().draw();
				oTable.rows.add(dataList).draw();

                Toastito.fire({
                    icon: 'success',
                    title: res.message
                })
                
            }else{
                Toastito.fire({
                    icon: 'error',
                    title: res.message
                })                
            }
        })
        .catch((err)=>{
            Toastito.fire({
                icon: 'error',
                title: err.response
            })
        })
		

	} catch (err) {
		Toastito.fire({
			icon: 'error',
			title: err
		})
	}
}

const resetModal = () =>{
    // $("#txtName").val("");
	// $("#cboEstado").val(1),  
    // $("#txtRuc").val(""); 
}

const createModal = () =>{
	resetModal();
	estado = 0;
	$("#modal-title").html('Agregar Parametro');
	$('#modal').modal('show')
}

const createModalItems = (id) => {

	//resetModalAddItem();
	estado = 0;
	let found = dataList.find(x => x.ID == id);

	// $("#modal-title-item").html('Agregar Item Parametro Padre: ' + found.NAME);
	// $("#idParameter2").val(found.ID);
	// $("#companyId").val(found.COMPANY_ID);
	// $("#updateOsave2").val(estado);
	getDataParameterChildren(found.ID);

	
	$("#modalItems").modal('show')

}

const editModal = async (id) =>{
	resetModal();
	estado = 1;
	idParameter = id;
	let found = dataList.find(x=>x.ID == id);
    console.log(found)

	$("#modal-title").html('Editar Parametro');	

	$("#cboCompany").val(found.COMPANY_ID);	
	
	$("#nombre_parametro").val(found.NAME);
	$("#descrip_parametro").val(found.DESCRIPTION);
	$("#valor_parametro").val(found.VALUE);
	$('#modal').modal('show')
}

const save = async() =>{	

	try {
		await validateData();
		await getDataFormParameter();	
		
		// console.log(data);

		requestAll(API.PARAMETERS.SAVE, data)
        .then((res)=>{
            
            if(res.status){

				data["ID"] = res.result;//adicionamos el id generado
				data["STATUS"] = 1;

                dataList.push(data);

				oTable.row.add(data).draw();
				
				ModalToast('success','Parametro creado');

                $('#modal').modal('hide')
				
                
            }else{
                Toastito.fire({
                    icon: 'error',
                    title: res.message
                })
                //console.log(res.message.data.message)
            }
        })
        .catch((err)=>{
            Toastito.fire({
                icon: 'error',
                title: err.response
            })
        })
		

	} catch (err) {
		Toastito.fire({
			icon: 'error',
			title: err
		})
	}	


}

const update = async () => {

	try {
		await validateData();
		await getDataFormParameter();		

		requestAll(API.PARAMETERS.EDIT, data)
        .then((res)=>{
            
            if(res.status){

                Toastito.fire({
                    icon: 'success',
                    title: res.message
                })

				let item  = dataList.find(x=>x.ID == data.ID);
				item["COMPANY_ID"] 		= data["COMPANY_ID"];				
				item["NAME"] 			=  data["NAME"];
				item["DESCRIPTION"] 	=  data["DESCRIPTION"];   
				item ["VALUE"] 			=  data ["VALUE"];	
				
				
				//console.log(dataList);

				oTable.clear().draw();
				oTable.rows.add(dataList).draw();

                $('#modal').modal('hide')
                
            }else{
                Toastito.fire({
                    icon: 'error',
                    title: res.message
                })
                //console.log(res.message.data.message)
            }
        })
        .catch((err)=>{
            Toastito.fire({
                icon: 'error',
                title: err.response
            })
        })
		

	} catch (err) {
		Toastito.fire({
			icon: 'error',
			title: err
		})
	}
}

const getDataFormParameter = async()=>{
	
	data["ID"]				=  idParameter;
	data["NAME"] 			=  $("#nombre_parametro").val();
    data["DESCRIPTION"] 	=  $("#descrip_parametro").val();   
	data["VALUE"] 			=  $("#valor_parametro").val();
	//Como entero x1
	data["COMPANY_ID"] 		=  $("#cboCompany").val()*1;
	data["SAVE_OR_UPDATE"] 	=  estado;
}  

const validateData = async() =>{	
   
	if(!$('#cboCompany').val())
		throw "revise el campo Empresa"	
    if(!$('#nombre_parametro').val())
		throw "revise el campo Nombres"	
	if(!$('#descrip_parametro').val()) 
		throw "revise el campo descripción" 	
	if(!$('#valor_parametro').val()) 
		throw "revise el campo de valores" 	


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

			dataListChildren = data;

			// data.map(x => {
			// 	dataListChildren.push({
			// 		ID: x.ID,
			// 		NAME: x.NAME,
			// 		DESCRIPTION: x.DESCRIPTION,
			// 		VALUE: x.VALUE,
			// 		COMPANY_ID: x.COMPANY_ID,
			// 		STATUS: x.STATUS
			// 	});
			// })
			//oTable2.clear().draw();
			//oTable2.rows.add(dataListChildren).draw();
			console.log(dataListChildren)

		})
}