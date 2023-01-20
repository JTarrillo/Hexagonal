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
			columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
		}
	}],
	"pageLength": 5,
	//"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
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
		{data: "LASTNAME_P", targets:2, className: "cell"},
		{data: "LASTNAME_M", targets:3, className: "cell"},
		{data: "TYPE_DOCUMENT", targets:4, className: "cell text-center"},	
		{data: "DOCUMENT", targets:5, className: "cell text-center"},
		{
			data: "COMPANY", 
			targets:6, 
			className: "cell text-center"
		},	
		{data: "EMAIL", targets:7, className: "cell"},	
		{
			data: "FK_ROLE", 
			targets:8, 
			className: "cell text-center",
			render: function (data,type,row,meta){
			
				switch (data) {
					case 1:	
						return 'SUPER ADMINISTRADOR';					
						break;
					case 2:	
						return 'ADMINISTRADOR';					
						break;
					case 3:	
						return 'SUPERVISOR';				
						break;
					case 4:	
						return 'COLABORADOR';					
						break;
					default:
						return '-'
						break;
				}
			}
		},		
		{
			data: "ESTADO", 
			targets:9, 
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
			targets:10,
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
						<a role"button" onClick="regeneratePassword(${row.ID})" class="dropdown-item"><i class="fas fa-key text-secondary"></i> Regenerar clave</a>
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
	getCompanies();

});

const getCompanies = async () => {
	fetch('../Company/GetCompaniesAll', {
		method: 'POST',
		//body: JSON.stringify(odata),
		headers: {
			'Content-Type': 'application/json'
		}
	})
	.then(response => response.json())
	.then(data => {			
		data.map(x=>{
			$("#cboCompany").append(`<option value="${x.ID}">${x.NAME}</option>`)
		})	
	})
}


const getData = async (role=2) => {

	requestAll(API.USERS.LIST,{RoleId:role})
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

const getDataDetail = async (id) =>{
	return requestAll(API.USERS.DETAIL,{id:id})
        .then((res)=>{            
            return res.data;			
        })
        .catch((err)=>{
            Toastito.fire({
                icon: 'error',
                title: err.response
            })
        });
}

const resetModal = () =>{
	$("#txtName").val(""),
	$("#txtlastNameP").val(""),
	$("#txtlastNameM").val(""),      
	$("#cboTipoDoc").val(""),
	$("#txtNumDoc").val(""),
	$("#txtTelefono").val(""),
	$("#cboGender").val(""),
	$("#txtEmail").val(""),
	$("#txtDireccion").val(""),  
	$("#cboRole").val(""), 
	$("#cboEstado").val(1),        
	$("#cboCompany").val(""),
	$("#txtNacimiento").val("")
}

const createModal = () =>{
	resetModal();
	estado = 0;
	$("#modal-title").html('Agregar Usuario');
	$('#modal').modal('show')
}

const editModal = async (id) =>{
	resetModal();
	estado = 1;
	let U = dataList.find(x=>x.ID == id);
	//await getDataDetail(id);
	

	$("#idUser").val(U.ID),
	$("#txtName").val(U.NAME||''),
	$("#txtlastNameP").val(U.LASTNAME_P),
	$("#txtlastNameM").val(U.LASTNAME_M),      
	$("#cboTipoDoc").val(U.TYPE_DOCUMENT),
	$("#txtNumDoc").val(U.DOCUMENT),
	$("#txtTelefono").val(U.PHONE1),
	$("#cboGender").val(U.GENDER),
	$("#txtEmail").val(U.EMAIL),
	$("#txtDireccion").val(U.ADDRESS),  
	$("#cboRole").val(U.FK_ROLE), 
	$("#cboEstado").val(U.ESTADO),        
	$("#cboCompany").val(U.ID_COMPANY),
	$("#txtNacimiento").val(U.BIRTHDAY)

	if(U.FK_ROLE==1)//rol de super administrador
	{
		//ocultar empresas
		$("#rowCboCompany").addClass('d-none');
	}else{
		$("#rowCboCompany").removeClass('d-none');
	}


	$("#modal-title").html('Editar Usuario');
	$('#modal').modal('show')
	


	// let found = dataList.find(x=>x.ID == id );

	// $("#modal-title").html('Editar Ciudad');
	// $("#txtCountry").val(found.NAME_COUNTRY);
	// $("#txtImgFirst").val(found.IMAGE_FIRST);
	// $("#idCountry").val(found.ID);
	// $("img#preview").attr("src",found.IMAGE_FIRST);
	// $("#modalAddCountry").show();
}

const save = async() =>{	

	try {
		await validateData();
		await getDataUser();		

		requestAll(API.USERS.SAVE, data)
        .then((res)=>{
            
            if(res.status){

				data["ID"] = res.result;//adicionamos el id generado				

				dataList.push(data);

				oTable.row.add(data).draw();


				
				ModalToast('success','Usuario creado',res.data,'Tome nota de la nueva clave.')

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
		

	// fetch('../User/SaveUser',{
	// 	method:'POST',
	// 	body: JSON.stringify(data),
	// 	headers:{
	// 		'Content-Type': 'application/json'
	// 	}
	// })
	// .then(response => response.json())
	// .then(data=>{
	// 	console.log(data)
	// })
}

const update = async() =>{	

	try {
		await validateData();
		await getDataUser();		

		requestAll(API.USERS.EDIT, data)
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

const validateData = async() =>{

	if($(document).find("#cboCompany").length == 1)
	{
		if(!$('#cboCompany').val())
		{
			throw "revise el campo Empresa";
		}
	}
   
	if(!$('#txtName').val())
		throw "revise el campo Nombres"
	if(!$('#txtlastNameP').val()) 
		throw "revise el campo Apellido Paterno" 
	if(!$('#txtlastNameM').val()) 
		throw "revise el campo Apellido Materno"
	if(!$('#cboTipoDoc').val()) 
		throw "revise el campo Tipo de documento"
	if(!$('#txtNumDoc').val()) 
		throw "revise el campo Numero de documento"
	if(!$('#txtTelefono').val()) 
		throw "revise el campo Telefono de contacto"
	if(!$('#cboGender').val()) 
		throw "revise el campo Género" 
	if(!$('#txtEmail').val()) 
		throw "revise el campo Email" 
	if(!$('#txtDireccion').val()) 
		throw "revise el campo Direccion" 
	if(!$('#cboRole').val()) 
		throw "revise el campo Roles" 
	if(!$('#cboEstado').val()) 
		throw "revise el campo Estado" 
	if(!$('#txtNacimiento').val()) 
		throw "revise el campo Fecha de nacimiento" 

	

}

const getDataUser = async()=>{ 
	
	data["ID"]				=  $("#idUser").val()
	data["NAME"] 			=  $("#txtName").val(),
	data["LASTNAME_P"] 		=  $("#txtlastNameP").val(),
	data["LASTNAME_M"] 		=  $("#txtlastNameM").val(),      
	data["TYPE_DOCUMENT"] 	=  $("#cboTipoDoc").val(),
	data["DOCUMENT"] 		=  $("#txtNumDoc").val(),
	data["PHONE1"] 			=  $("#txtTelefono").val(),
	data["GENDER"] 			=  $("#cboGender").val(),
	data["EMAIL"] 			=  $("#txtEmail").val(),
	data["ADDRESS"] 		=  $("#txtDireccion").val(),  
	data["FK_ROLE"] 		=  $("#cboRole").val()*1, 
	data["ESTADO"] 			=  $("#cboEstado").val(),        
	data["COMPANY_ID"] 		=  $("#cboCompany").val(),
	data["BIRTHDAY"] 		=  $("#txtNacimiento").val(),
	data["COMPANY"]			=  $("#cboCompany option:selected").text()

    console.log(data);
}

const regeneratePassword = async(id)=>{
	try {				

		requestAll(API.USERS.REGENERATE_PASS, {id:id})
        .then((res)=>{
            
            if(res.status){

                ModalToast('success','Contraseña Regenerada',res.data,'Tome nota de la nueva clave.') 
                
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

const changeStatus = async(id)=>{
	try {				

		requestAll(API.USERS.CHANGE_STATUS, {id:id})
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