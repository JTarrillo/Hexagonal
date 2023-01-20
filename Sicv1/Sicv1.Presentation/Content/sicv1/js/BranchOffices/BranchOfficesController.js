let dataList = [];
let dataListCountry = [];
let dataListCompany = [];
let dataListType = [];
let dataListCategories = [];
let estado = 0;
let isVideo = false;
let isExpire = false;
let idCompany = 0;

oTable = $('.box-table').DataTable({
	"paging": true,
	"info": true,
	order:[[ 0, "desc" ]],
	searching: true,
	responsive: true,
	autoWidth: false,
	dom: "ft<'row'<'col-12 col-md-6'i><'col-12 col-md-6'<'d-flex justify-content-center justify-content-md-end'p>>>",
	language: {
		"sProcessing": "Procesando...",
		"sLengthMenu": "Mostrar _MENU_ registros",
		"sZeroRecords": "No se encontraron resultados",
		"sEmptyTable": "Ningún dato disponible en esta tabla",
		"sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_",
		"sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0",
		"sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
		"sInfoPostFix": "",
		"sSearch": "",
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
			target:0,
			className: "cell text-center"
		},
        {
			width: "200",
			data: "NAME_COMPANY",
			title: "Alianza",			
			className: "cell cell-wrap-none"
		},
		{
			width: "200",
			data: "NAME",
			title: "Cede",			
			className: "cell cell-wrap-none"
		},
		{
			width: "50",
			data: "COUNTRY",
			title: "Ciudad",	
            className: "cell"		
		},
        {
			width: "100",
			data: "DIRECTION",
			title: "Direccion",	
            className: "cell"		
		},
        {
			width: "100",
			data: "ID",
			title: "Acciones",
			className: "cell text-center",
			render: function (data, type, row, meta) {
				return '<button type="button" onclick=editModal('+data+') class="btn btn-orange btn-sm"><i class="fas fa-edit"></i></button>'
					+' <button type="button" onclick=deleteModal('+data+') class="btn btn-pink btn-sm"><i class="fas fa-times"></i></button>';
				
				
			}
		},
		
	],
});

$(async ()=> {	

	await GetCompany();
	await GetCountry();
	await GetData();

    $(".dataTables_filter input").attr("placeholder", "Buscar");

    $("#btn-add-branchOffices").on('click',()=>{
		createModal();
	})

    $(document).on("click", "#btn-x, #btn-close, #btn-close-Add", function () {
		$("#modalAddBranchOffices").hide();
		//$(".bd-status").removeClass('on-modal');
	});

	$(document).on("change", "#cboCompany", async()=>{
		idCompany = $('#cboCompany').val();	
		
		await FilterCompany(idCompany);
		//$(".bd-status").removeClass('on-modal');
	});

	$(document).on("click", "#btn-save-add", function () {
		if(estado==0){
			save();
		}else{
			update();
		}
				
	});

	$('#cboCompany').select2();

    
	
})

const FilterCompany = async (id) =>{
	let dataSet =  dataList.filter(x=>x.ID_COMPANY == id);	
	oTable.clear().draw();
	oTable.rows.add(dataSet).draw();
}

const GetData = async () => {

	await fetch('../BranchOffices/GetBranchOffices')
		.then(response => response.json())
		.then(data => {
			

			for (let i = 0; i < data.length; i++) {
				let filtercompany = dataListCompany.find(x=>x.ID == data[i].ID_COMPANY);
				
				dataList.push({
					ID:data[i].ID,
					ID_COMPANY:data[i].ID_COMPANY,
					NAME_COMPANY: filtercompany.NAME,
					NAME:data[i].NAME,
					PHONE:data[i].PHONE,
					LATITUDE:data[i].LATITUDE,
					LONGITUDE:data[i].LONGITUDE,
					DIRECTION:data[i].DIRECTION,
					COUNTRY:data[i].COUNTRY,				
					
				});

				
			}
			
			//console.log(dataList)
			
			//oTable.clear().draw();
			//oTable.rows.add(dataList).draw();
			//console.log(dataList)
			
		})
}
const GetCountry = async () => {

	await fetch('../Country/GetCountry')
		.then(response => response.json())
		.then(data => {
            let html = "";
			for (let i = 0; i < data.length; i++) {
				
				dataListCountry.push({
					ID:data[i].ID,
					NAME_COUNTRY:data[i].NAME_COUNTRY,					
				});	
                
                html += '<option value="'+data[i].NAME_COUNTRY+'">'+data[i].NAME_COUNTRY+'</option>';
			}	
            
            document.getElementById("cboCountry").innerHTML += html;
			
			//console.log(dataList)
			
		})
        .catch(err=> {
            console.log('Fetch Error :', err);
        });
}
const GetCompany =  async () =>{
	await fetch('../Company/List')
		.then(response => response.json())
		.then(data => {
            let html = "";
			for (let i = 0; i < data.length; i++) {
				
				dataListCompany.push({
					ID:data[i].ID,
					NAME:data[i].NAME,					
				});	
                
                html += '<option value="'+data[i].ID+'">'+data[i].NAME+'</option>';
			}	
            
            document.getElementById("cboCompany").innerHTML += html;
			
			//console.log(dataList)
			
		})
        .catch(err=> {
            console.log('Fetch Error :', err);
        });
}

const createModal = () =>{
	resetModal();
	estado = 0;
	$("#modal-title").html('Nueva Cede');
	$("#modalAddBranchOffices").show();
}

const resetModal = () =>{    
	$("#txtName").val("");
	$("#txtPhone").val();
	$("#txtLatitude").val();
	$("#txtLongitude").val();
	$("#txtDirection").val();
	$("#cboCountry").val(0);
}

const editModal = (id) =>{
	resetModal();
	estado = 1;

	let found = dataList.find(x=>x.ID == id );

	$("#modal-title").html('Editar Cede');

	$("#idBranchOffices").val(id);
	$("#txtName").val(found.NAME);
	$("#txtPhone").val(found.PHONE);
	$("#txtLatitude").val(found.LATITUDE);
	$("#txtLongitude").val(found.LONGITUDE);
	$("#txtDirection").val(found.DIRECTION);
	$("#cboCountry").val(found.COUNTRY);	
	$("#modalAddBranchOffices").show();
}


const save = async () => {
    let odata = {
		ID_COMPANY: $('#cboCompany').val(),
		NAME: $("#txtName").val(),
		PHONE:$("#txtPhone").val(),
		LATITUDE: $("#txtLatitude").val(),
		LONGITUDE:$("#txtLongitude").val(),
		DIRECTION:$("#txtDirection").val(),		
		COUNTRY: $("#cboCountry").val()	
	};

    fetch('../BranchOffices/SaveBranchOffices',{
		method:'POST',
		body: JSON.stringify(odata),
		headers:{
			'Content-Type': 'application/json'
		}
	})
	.then(response => response.json())
	.then(data=>{
		if(data.status){
			Toast.fire({
				icon: 'success',
				title: 'Guardado Completo'
			})
			//console.log(data)
			let filtercompany = dataListCompany.find(x=>x.ID == odata.ID_COMPANY);
			let result = {
				ID:data.id,
				ID_COMPANY:odata.ID_COMPANY,
				NAME_COMPANY: filtercompany.NAME,
				NAME:odata.NAME,
				PHONE:odata.PHONE,
				LATITUDE:odata.LATITUDE,
				LONGITUDE:odata.LONGITUDE,
				DIRECTION:odata.DIRECTION,
				COUNTRY:odata.COUNTRY,
			}
			let previewData = [];
	
			dataList.push(result);
			previewData.push(result);
	
			
	
			oTable.rows.add(previewData).draw();
			$("#modalAddBranchOffices").hide();
		}
		
    })
}

const update = () =>{
	let odata = {
		ID:$("#idBranchOffices").val(),		
		NAME: $("#txtName").val(),
		PHONE:$("#txtPhone").val(),
		LATITUDE: $("#txtLatitude").val(),
		LONGITUDE:$("#txtLongitude").val(),
		DIRECTION:$("#txtDirection").val(),		
		COUNTRY: $("#cboCountry").val()	
	};

	fetch('../BranchOffices/UpdateBranchOffices',{
		method:'POST',
		body: JSON.stringify(odata),
		headers:{
			'Content-Type': 'application/json'
		}
	})
	.then(response => response.json())
	.then(data=>{

		if(data.status){
			
			Toast.fire({
				icon: 'success',
				title: 'Actualización Completa'
			})
		
			let found = dataList.find( x=>x.ID == odata.ID );
	
			
			found.NAME		=	odata.NAME;
			found.PHONE		=	odata.PHONE;
			found.LATITUDE	=	odata.LATITUDE;
			found.LONGITUDE	=	odata.LONGITUDE;
			found.DIRECTION	=	odata.DIRECTION;
			found.COUNTRY	=	odata.COUNTRY;

			let dataSet =  dataList.filter(x=>x.ID_COMPANY == found.ID_COMPANY);	
			oTable.clear().draw();
			oTable.rows.add(dataSet).draw();	
			
			$("#modalAddBranchOffices").hide();
		}
		
		

	})


}

const deleteModal = (id) =>{
	
	Swal.fire({
		title: '¿Desea continuar?',
		text: "Este proceso en irreversible!",
		icon: 'warning',
		showCancelButton: true,
		confirmButtonColor: '#3085d6',
		cancelButtonColor: '#d33',
		confirmButtonText: 'Si, Eliminalo!',
		cancelButtonText:'Cancelar'
	  }).then((result) => {
		if (result.isConfirmed) {
			fetch('../BranchOffices/DeleteBranchOffices',{
				method:'POST',
				body: JSON.stringify({ ID : id }),
				headers:{
					'Content-Type': 'application/json'
				}
			})
			.then(response => response.json())
			.then(data=>{
		
				if(data.status){
		
					Toast.fire({
						icon: 'success',
						title: 'Registro Eliminado'
					})
		
					let index = dataList.findIndex(x => x.ID == id);
					dataList.splice(index,1);
		
					let dataSet =  dataList.filter(x=>x.ID_COMPANY == idCompany);	
					oTable.clear().draw();
					oTable.rows.add(dataSet).draw();
				}
		
				
			
				
			})
		}
	  })

	


}

const Toast = Swal.mixin({
	toast: true,
	position: 'top-end',
	showConfirmButton: false,
	timer: 3000,
	timerProgressBar: true,
	didOpen: (toast) => {
	  toast.addEventListener('mouseenter', Swal.stopTimer)
	  toast.addEventListener('mouseleave', Swal.resumeTimer)
	}
})