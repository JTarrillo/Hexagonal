let data = {};
let dataList = [];
let estado = 0;

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
        {data: "ID", targets:0, className: "cell"},
		{data: "NAME", targets:1, className: "cell"},
		{data: "RUC", targets:2, className: "cell"},
		{
			data: "ESTADO", 
			targets:3, 
			className: "cell text-center",
			render: function (data, type, row, meta) {
				if(data==1){
					return '<span class="text-primary">ACTIVO</span>';
				}else{
					return '<span class="">INACTIVO</span>';
				}
				
			}
		},
        {
			targets:4,
			className: "cell text-center",
			render: function (data, type, row, meta) {
				let html = `
				<ul class="list-unstyled" style="width: 200px;">
					<li><h6><span role="button" class="badge badge-warning p-1">Editar</span></h6></li>
					<li><h6><span role="button" class="badge badge-danger p-1">Desactivar</span></h6></li>
				</ul>
				`;
				return `<a role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">				
							<svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-dots" width="24" height="24" viewBox="0 0 24 24" stroke-width="1.5" stroke="#9e9e9e" fill="none" stroke-linecap="round" stroke-linejoin="round">
								<path stroke="none" d="M0 0h24v24H0z" fill="none"/>
								<circle cx="5" cy="12" r="1" />
								<circle cx="12" cy="12" r="1" />
								<circle cx="19" cy="12" r="1" />
							</svg>
						</a><div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
						<a role"button" onClick="editModal(${row.ID})" class="dropdown-item"><i class="fas fa-pen text-danger"></i> Editar</a>						
						<div class="dropdown-divider"></div>
						<a role"button" onClick="changeStatus(${row.ID})" class="dropdown-item"><i class="fas ${row.ESTADO==1?'fa-ban text-danger':'fa-check text-success'}"></i> ${row.ESTADO==1?'Inhabilitar':'Habilitar'}</a>						
					  </div>`;
			}
		}
	],
});

$(function () {	

    $('#ExportReporttoExcel').click(() => {
		oTable.buttons(0, 0).trigger()
	});
	
	$("#search").on("keyup search input paste cut change", function () {
		//console.log(this.value);
		oTable.search(this.value).draw();
	});

	$("#btn-cancelar-busq").on("click", function () {
		//console.log(this.value);
		$("#search").val('');	
		oTable.search('').draw();
	});

	$("#btn-modal-user").on('click',()=>{
		createModal();
	})

	$(document).on("click", "#btn-save-add", function () {
		if(estado==0){
			save();
		}else{
			update();
		}				
	});

	$('#btn-filter-icon').on('click',function(){
		$('#btn-filter-busq').toggleClass( "active" );
	})

	$('#cboRoleFilter').on('change',function(){
		let role = $( this ).val();
		getData(role);
	})


	getData();
});

const getData = async () => {

	requestAll(API.COMPANIES.LIST)
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

const resetModal = () =>{
    $("#txtName").val("");
	$("#cboEstado").val(1),  
    $("#txtRuc").val(""); 
}

const createModal = () =>{
	resetModal();
	estado = 0;
	$("#modal-title").html('Agregar Empresa');
	$('#modal').modal('show')
}

const editModal = async (id) =>{
	resetModal();
	estado = 1;
	let U = dataList.find(x=>x.ID == id);
    console.log(U)
    $("#idCompany").val(U.ID);
    $("#txtName").val(U.NAME);
    $("#cboEstado").val(U.ESTADO);
    $("#txtRuc").val(U.RUC);

	$("#modal-title").html('Editar Empresa');
	$('#modal').modal('show')
}

const save = async() =>{	

	try {
		await validateData();
		await getDataUser();		

		requestAll(API.COMPANIES.SAVE, data)
        .then((res)=>{
            
            if(res.status){

				data["ID"] = res.result;//adicionamos el id generado

                dataList.push(data);

				oTable.row.add(data).draw();
				
				ModalToast('success','Empresa creada');

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

const update = async() =>{	

	try {
		await validateData();
		await getDataUser();		

		requestAll(API.COMPANIES.EDIT, data)
        .then((res)=>{
            
            if(res.status){

                Toastito.fire({
                    icon: 'success',
                    title: res.message
                })

				let itemIndex  = dataList.findIndex(x=>x.ID == data.ID);
				dataList[itemIndex] = data;
				// found["NAME"] 			=	data.NAME;
				// found["LASTNAME_P"] 	=  	data.LASTNAME_P;
				// found["LASTNAME_M"] 	=  	data.LASTNAME_M;    
				// found["TYPE_DOCUMENT"] 	=  	data.TYPE_DOCUMENT;
				// found["DOCUMENT"] 		= 	data.DOCUMENT;
				// found["EMAIL"]			= 	data.EMAIL;
				// found["ESTADO"] 		= 	data.ESTADO;
				// found["FK_ROLE"]		=	data.FK_ROLE;

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


const getDataUser = async()=>{ 
	
	data["ID"]				=  $("#idCompany").val();
	data["RUC"] 			=  $("#txtRuc").val();
    data["NAME"] 			=  $("#txtName").val();   
	data["ESTADO"] 			=  $("#cboEstado").val();
}    

const validateData = async() =>{	
   
	if(!$('#txtRuc').val())
		throw "revise el campo RUC"	
    if(!$('#txtName').val())
		throw "revise el campo Nombres"	
	if(!$('#cboEstado').val()) 
		throw "revise el campo Estado" 	

}

const changeStatus = async(id)=>{
	try {				

		requestAll(API.COMPANIES.CHANGE_STATUS, {id:id})
        .then((res)=>{
            
            if(res.status){

				let found = dataList.find(x=>x.ID == id);
				found["ESTADO"] 	= 	(found.ESTADO==1)?0:1;

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