let dataList = [];
let estado = 0;

oTable = $('.box-table').DataTable({
	"paging": true,
	"info": true,
	searching: true,
	responsive: true,
	"autoWidth": true,
	"dom": '',
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
			data: "NAME_COUNTRY",
			title: "Ciudad",
			className: "cell"
		},
		{
			width: "150",
			data: "IMAGE_FIRST",
			title: "Portada",
			className: "cell",
			render: function (data, type, row, meta){
				return '<img src="'+data+'" alt="" class="img-thumbnail" style="max-width:100px;">';
			}
		},				
		{
			width: "150",
			data: "STATUS",
			title: "Estado",
			className: "cell text-center",
			render: function (data, type, row, meta) {
				return data?'<span class="badge badge-info p-2">Activo</span>':'<span class="badge badge-danger p-2">Inactivo</span>';
			
			}
		},		
		{
			width: "150",
			data: "ID",
			title: "Acciones",
			className: "cell text-center",
			render: function (data, type, row, meta) {
				if(row.STATUS){
					return '<button type="button" onclick=editModal('+data+') class="btn btn-orange btn-sm"><i class="fas fa-edit"></i></button>'
					+' <button type="button" onclick=deleteModal('+data+') class="btn btn-pink btn-sm"><i class="fas fa-times"></i></button>';
				}else{
					return '<button type="button" onclick=ActiveModal('+data+') class="btn btn-success btn-sm"><i class="fas fa-check"></i></button>';
				}
				
			}
		},
		
	],
});

$(function () {
    console.log("hola mundo")
	getData();

	$("#btn-add-country").on('click',()=>{
		createModal();
	})

	$("input#txtImgFirst").on('change keyup',()=>{
		var img = $("input#txtImgFirst").val();		
		$("img#preview").attr("src",img);
	})



	$(document).on("click", "#btn-x, #btn-close, #btn-close-Add", function () {
		$("#modalAddCountry").hide();
		//$(".bd-status").removeClass('on-modal');
	});

	$(document).on("click", "#btn-save-add", function () {
		if(estado==0){
			save();
		}else{
			update();
		}
				
	});


});


const getData = async () => {

	fetch('../Country/GetCountry')
		.then(response => response.json())
		.then(data => {

			for (let i = 0; i < data.length; i++) {
				
				dataList.push({
					ID:data[i].ID,
					NAME_COUNTRY:data[i].NAME_COUNTRY,
					STATUS: data[i].STATUS,
					IMAGE_FIRST:data[i].IMAGE_FIRST
				});

				
			}	
			
			oTable.clear().draw();
			oTable.rows.add(dataList).draw();
			//console.log(dataList)
			
		})
}

const resetModal = () =>{
	$("#txtCountry").val("");
	$("#txtImgFirst").val("");
}

const createModal = () =>{
	resetModal();
	estado = 0;
	$("#modal-title").html('Agregar Ciudad');
	$("#modalAddCountry").show();
}

const editModal = (id) =>{
	resetModal();
	estado = 1;

	let found = dataList.find(x=>x.ID == id );

	$("#modal-title").html('Editar Ciudad');
	$("#txtCountry").val(found.NAME_COUNTRY);
	$("#txtImgFirst").val(found.IMAGE_FIRST);
	$("#idCountry").val(found.ID);
	$("img#preview").attr("src",found.IMAGE_FIRST);
	$("#modalAddCountry").show();
}

const save = () =>{	
	
	var odata = {
		'NAME_COUNTRY':$("#txtCountry").val(),
		'IMAGE_FIRST':$("#txtImgFirst").val()
	};	

	fetch('../Country/SaveCountry',{
		method:'POST',
		body: JSON.stringify(odata),
		headers:{
			'Content-Type': 'application/json'
		}
	})
	.then(response => response.json())
	.then(data=>{
		console.log(data)

		dataList.push({
			ID:data.message,
			NAME_COUNTRY:$("#txtCountry").val(),
			IMAGE_FIRST:$("#txtImgFirst").val(),
			STATUS: true
		});

		oTable.clear().draw();
		oTable.rows.add(dataList).draw();
		$("#modalAddCountry").hide();

	})
}

const update = () =>{
	var odata = {
		'NAME_COUNTRY':$("#txtCountry").val(),
		'IMAGE_FIRST':$("#txtImgFirst").val(),
		'ID':$("#idCountry").val()
	};

	fetch('../Country/UpdateCountry',{
		method:'POST',
		body: JSON.stringify(odata),
		headers:{
			'Content-Type': 'application/json'
		}
	})
	.then(response => response.json())
	.then(data=>{
		
		var id = $("#idCountry").val();
		
		let found = dataList.find(x=>x.ID == id );

		found.NAME_COUNTRY = $("#txtCountry").val();
		found.IMAGE_FIRST = $("#txtImgFirst").val();

		oTable.clear().draw();
		oTable.rows.add(dataList).draw();
		$("#modalAddCountry").hide();

	})


}

const deleteModal = (id) =>{	

	fetch('../Country/DeleteCountry',{
		method:'POST',
		body: JSON.stringify({ ID : id }),
		headers:{
			'Content-Type': 'application/json'
		}
	})
	.then(response => response.json())
	.then(data=>{
		
		let found = dataList.find(x => x.ID == id);
		found.STATUS = false;

		oTable.clear().draw();
		oTable.rows.add(dataList).draw();
		
	})


}

const ActiveModal = (id) =>{

	fetch('../Country/ActiveCountry',{
		method:'POST',
		body: JSON.stringify({ ID : id }),
		headers:{
			'Content-Type': 'application/json'
		}
	})
	.then(response => response.json())
	.then(data=>{
		
		let found = dataList.find(x => x.ID == id);
		found.STATUS = true;

		//console.log(found)

		oTable.clear().draw();
		oTable.rows.add(dataList).draw();
		
	})
}

