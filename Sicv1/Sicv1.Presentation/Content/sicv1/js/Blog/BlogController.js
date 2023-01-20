let dataList = [];
let dataListType = [];
let dataListCategories = [];
let estado = 0;
let isVideo = false;
let isExpire = false;

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
			data: "TITLE",
			title: "Titulo",			
			className: "cell cell-wrap-none"
		},
		{
			width: "50",
			data: "CATEGORY",
			title: "Categoria",	
            className: "cell"		
		},
        {
			width: "50",
			data: "TYPE_NEWNESS",
			title: "Tipo",	
            className: "cell"		
		},				
		{
			width: "50",
			data: "VIDEO",
			title: "Video",
			className: "cell text-center",	
            render:function(data, type, row, meta){
                return data?'SI':'NO';
            }		
		},		
		{
			width: "70",
			data: "IMAGE_FIRST",
			title: "Imagen",
			className: "cell text-center",
            render: function (data, type, row, meta){
				return '<img src="'+data+'" alt="" class="img-thumbnail" style="max-width:60px;">';
			}			
		},
        {
			width: "100",
			data: "ID",
			title: "Acciones",
			className: "cell text-center",
			render: function (data, type, row, meta) {
				return '<button type="button" onclick=link('+data+') class="btn btn-blue btn-sm"><i class="fas fa-eye"></i></button> <button type="button" onclick=editModal('+data+') class="btn btn-orange btn-sm"><i class="fas fa-edit"></i></button>'
					+' <button type="button" onclick=deleteModal('+data+') class="btn btn-pink btn-sm"><i class="fas fa-times"></i></button>';
				
				
			}
		},
		
	],
});

$(function () {	

    $(".dataTables_filter input").attr("placeholder", "Buscar");
    getData();
    GetNewsNessType();
    GetCategories();

    $("#btn-add-blog").on('click',()=>{
		createModal();
	})

    $(document).on("click", "#btn-x, #btn-close, #btn-close-Add", function () {
		$("#modalAddBlog").hide();
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

	fetch('../Newsness/GetNewsNesses')
		.then(response => response.json())
		.then(data => {
            
			for (let i = 0; i < data.length; i++) {
				
				dataList.push({
					ID:data[i].ID,
					TITLE:data[i].TITLE,
					CATEGORY: data[i].CATEGORY,
                    TYPE_NEWNESS: data[i].TYPE_NEWNESS,
					VIDEO:data[i].VIDEO,
					EXPIRE:data[i].EXPIRE,
                    IMAGE_FIRST:data[i].IMAGE_FIRST,
                    url:data[i].URL,						
					
					ID_CATEGORY	: data[i].ID_CATEGORY,
					ID_TYPE_NEWNESS: data[i].ID_TYPE_NEWNESS,
					
					DATE_PUBLICATION:data[i].DATE_PUBLICATION,
					DATE_EXPIRATION : data[i].DATE_EXPIRATION,
					DESCRIPTION : data[i].DESCRIPTION,			
					STATUS: data[i].STATUS,
				});

				
			}	
			
			//oTable.clear().draw();
			oTable.rows.add(dataList).draw();
            //oTable.columns.adjust().draw();
			//console.log(dataList)
			
		})
}

const GetNewsNessType = async () => {

	fetch('../Newsness/GetNewsNessType')
		.then(response => response.json())
		.then(data => {
            let html = "";

			for (let i = 0; i < data.length; i++) {
				
				dataListType.push({
					ID:data[i].ID,
					NAME:data[i].NAME,				
				});	
                
                html += '<option value="'+data[i].ID+'">'+data[i].NAME+'</option>';
			}
            
            document.getElementById("cboType").innerHTML += html;            
			
			//console.log(dataListType)
			
		})
}

const GetCategories = async () => {

	fetch('../Category/GetParentCategories')
		.then(response => response.json())
		.then(data => {
            let html = "";

			for (let i = 0; i < data.length; i++) {
				
				dataListCategories.push({
					ID:data[i].ID,
					TITLE:data[i].TITLE,				
				});	
                
                html += '<option value="'+data[i].ID+'">'+data[i].TITLE+'</option>';
			}
            
            document.getElementById("cboCategory").innerHTML += html;            
			
			//console.log(data)
			
		})
}

const createModal = () =>{
	resetModal();
	estado = 0;
	$("#modal-title").html('Nuevo Blog');
	$("#modalAddBlog").show();
}

const link = (id) => {
    
    let found = dataList.find(x=>x.ID = id);
    
    window.open(found.url, '_blank');
}

function onChangeVideo(e) {
    var contentCurrent = document.getElementById("spanText").textContent;
    document.getElementById("btnChangeType").classList.toggle('btn-toggle-status-on');
    if (contentCurrent === 'NO') {
        //document.getElementById("scCustom").hidden = true;
        document.getElementById("spanText").innerHTML = "SI";
		isVideo = true;
        //typeToggle = "all";
    } else {
        //document.getElementById("scCustom").hidden = false;
        //typeToggle = "custom";
        document.getElementById("spanText").innerHTML = "NO";
		isVideo = false;
    }
}

function onChangeExpire(e) {
    var contentCurrent = document.getElementById("spanTextExpire").textContent;
    document.getElementById("btnChangeExpire").classList.toggle('btn-toggle-status-on');
    if (contentCurrent === 'NO') {
        document.getElementById("col-expiration").classList.remove('d-none');
        document.getElementById("spanTextExpire").innerHTML = "SI";
		isExpire = true;
        //typeToggle = "all";
    } else {
        document.getElementById("col-expiration").classList.add('d-none');
        //typeToggle = "custom";
        document.getElementById("spanTextExpire").innerHTML = "NO";
		isExpire = false;
		$("#txtDateExpiration").val("");
    }
}

const save = async () => {
	
	var vlength = $("#inputFileAdd")[0].files[0];

	
	if (!$("#hdimgBase64Add").val()) {
		fncreateAlert("Seleccione una imagen", 'warning');
		return;
	}

	if (!/\.(jpg|png)$/i.test(vlength.name)) {
		fncreateAlert("Debe seleccionar un archivo de extensión jpg o png", 'warning');
		return;
	}

	if (vlength.size > 1024 * 1024 * 2) {
		fncreateAlert("El archivo debe pesar menos de 2 MB.", 'warning');
		return;
	}

	if (!$("#txtTitle").val()) {
		fncreateAlert("Indique un titulo", 'warning');
		return;
	}

	if (!$("#cboCategory").val()) {
		fncreateAlert("Indique una categoria", 'warning');
		return;
	}

	if (!$("#cboType").val()) {
		fncreateAlert("Indique un tipo de blog", 'warning');
		return;
	}

	if (!$("#txtDescription").val()) {
		fncreateAlert("Indique una descripción", 'warning');
		return;
	}

	if (!$("#txtUrl").val()) {
		fncreateAlert("Indique un Enlace", 'warning');
		return;
	}

	if (!$("#txtDatePublication").val()) {
		fncreateAlert("Indique una fecha de publicacion", 'warning');
		return;
	}

	if (!$("#txtDateExpiration").val() && isExpire) {
		fncreateAlert("Indique una fecha de expiracion", 'warning');
		return;
	}

	var dateExpire = $("#txtDateExpiration").val();

	if(dateExpire==""){
		dateExpire = "9999-01-01"
	}

	var vImageBase64Add = $("#hdimgBase64Add").val();

	let odata = {
		'ID_CATEGORY': $("#cboCategory").val(),
		'ID_TYPE_NEWNESS': $("#cboType").val(),
		'URL':$("#txtUrl").val(),
		'ISVIDEO': isVideo,
		'TITLE':$("#txtTitle").val(),
		'DESCRIPTION':$("#txtDescription").val(),		
		'EXPIRE': isExpire,
		'DATE_PUBLICATION':$("#txtDatePublication").val(),
		'DATE_EXPIRATION':dateExpire,
		'IMAGEBASE64':vImageBase64Add,
		'HDFILENAME':$("#hdFileNameAdd").val(),
	};

	fetch('../Newsness/SaveNewsness',{
		method:'POST',
		body: JSON.stringify(odata),
		headers:{
			'Content-Type': 'application/json'
		}
	})
	.then(response => response.json())
	.then(data=>{

		fncreateAlert("Se guardaron los cambios", "success");

		let result = {
			ID			: data.id,		
			TITLE		: odata.TITLE,
			CATEGORY	: dataListCategories.find(x=>x.ID == odata.ID_CATEGORY).TITLE,
			ID_CATEGORY	: odata.ID_CATEGORY,
			ID_TYPE_NEWNESS : odata.ID_TYPE_NEWNESS,
            TYPE_NEWNESS: dataListType.find(x=>x.ID == odata.ID_TYPE_NEWNESS).NAME,
			VIDEO		: odata.ISVIDEO,
			EXPIRE		: odata.EXPIRE,
            IMAGE_FIRST	: odata.IMAGEBASE64,
            url			: odata.URL,
			DATE_PUBLICATION:odata.DATE_PUBLICATION,
			DATE_EXPIRATION : odata.DATE_EXPIRATION,
			DESCRIPTION : odata.DESCRIPTION,			
			STATUS: true,
		}
		
		let previewData = []
		
		dataList.push(result);
		previewData.push(result);

		console.log(result)

		//oTable.clear().draw();
		oTable.rows.add(previewData).draw();
		$("#modalAddBlog").hide();
	})

	
}

const update = async () => {
	
	var vlength = $("#inputFileAdd")[0].files[0];

	if ($("#hdimgBase64Add").val()) {
		if (!/\.(jpg|png)$/i.test(vlength.name)) {
			fncreateAlert("Debe seleccionar un archivo de extensión jpg o png", 'warning');
			return;
		}
	
		if (vlength.size > 1024 * 1024 * 2) {
			fncreateAlert("El archivo debe pesar menos de 2 MB.", 'warning');
			return;
		}
	}

	

	if (!$("#txtTitle").val()) {
		fncreateAlert("Indique un titulo", 'warning');
		return;
	}

	if (!$("#cboCategory").val()) {
		fncreateAlert("Indique una categoria", 'warning');
		return;
	}

	if (!$("#cboType").val()) {
		fncreateAlert("Indique un tipo de blog", 'warning');
		return;
	}
	

	if (!$("#txtDatePublication").val()) {
		fncreateAlert("Indique una fecha de publicacion", 'warning');
		return;
	}

	if (!$("#txtDateExpiration").val() && isExpire) {
		fncreateAlert("Indique una fecha de expiracion", 'warning');
		return;
	}

	var dateExpire = $("#txtDateExpiration").val();

	if(dateExpire==""){
		dateExpire = "9999-01-01"
	}

	var vImageBase64Add = $("#hdimgBase64Add").val();

	var id =  $("#idNewsness").val();

	let found = dataList.find( x=>x.ID == id );

	let odata = {
		'ID': id,
		'ID_CATEGORY': $("#cboCategory").val(),
		'ID_TYPE_NEWNESS': $("#cboType").val(),
		'URL':$("#txtUrl").val(),
		'ISVIDEO': isVideo,
		'TITLE':$("#txtTitle").val(),
		'DESCRIPTION':$("#txtDescription").val(),		
		'EXPIRE': isExpire,
		'DATE_PUBLICATION':$("#txtDatePublication").val(),
		'DATE_EXPIRATION':dateExpire,
		'IMAGEBASE64':vImageBase64Add,
		'HDFILENAME':$("#hdFileNameAdd").val(),
		'IMAGE_FIRST':found.IMAGE_FIRST
	};

	fetch('../Newsness/UpdateNewsness',{
		method:'POST',
		body: JSON.stringify(odata),
		headers:{
			'Content-Type': 'application/json'
		}
	})
	.then(response => response.json())
	.then(data=>{

		fncreateAlert("Se guardaron los cambios", "success");
				
		found.TITLE = odata.TITLE;
		found.CATEGORY = dataListCategories.find(x=>x.ID == odata.ID_CATEGORY).TITLE;
		found.ID_CATEGORY = odata.ID_CATEGORY;
		found.ID_TYPE_NEWNESS = odata.ID_TYPE_NEWNESS;
		found.TYPE_NEWNESS = dataListType.find(x=>x.ID == odata.ID_TYPE_NEWNESS).NAME;
		found.VIDEO =  odata.ISVIDEO;
		found.EXPIRE = odata.EXPIRE;
		found.IMAGE_FIRST = (odata.IMAGEBASE64=="")?odata.IMAGE_FIRST:odata.IMAGEBASE64;
		found.url = odata.URL;
		found.DATE_PUBLICATION = odata.DATE_PUBLICATION;
		found.DATE_EXPIRATION = odata.DATE_EXPIRATION;
		found.DESCRIPTION = odata.DESCRIPTION;			
		found.STATUS = true,
		

		oTable.clear().draw();
		oTable.rows.add(dataList).draw();
		$("#modalAddBlog").hide();
	})

	

}

const validation = () =>{
	var vlength = $("#inputFileAdd")[0].files[0];
	console.log(vlength);
	if (!$("#hdimgBase64Add").val()) {
		fncreateAlert("Seleccione una imagen", 'warning');
		return false;
	}

	if (!/\.(jpg|png)$/i.test(vlength.name)) {
		fncreateAlert("Debe seleccionar un archivo de extensión jpg o png", 'warning');
		return false;
	}

	if (vlength.size > 1024 * 1024 * 2) {
		fncreateAlert("El archivo debe pesar menos de 2 MB.", 'warning');
		return false;
	}

	if (!$("#txtTitle").val()) {
		fncreateAlert("Indique un titulo", 'warning');
		return false;
	}

	return true;
}

const editModal = (id) =>{
	resetModal();
	estado = 1;

	let found = dataList.find( x=>x.ID == id );

	console.log(found)
	$("#idNewsness").val(id);
	$("#cboCategory").val(found.ID_CATEGORY);
	$("#cboType").val(found.ID_TYPE_NEWNESS);
	$("#txtUrl").val(found.url);
	if(found.VIDEO==true){
		document.getElementById("btnChangeType").classList.add('btn-toggle-status-on');
		document.getElementById("spanText").innerHTML = "SI";
		isVideo = true;
	}	
    $("#txtTitle").val(found.TITLE);
	$("#txtDescription").val(found.DESCRIPTION);
	if(found.EXPIRE==true){
		document.getElementById("btnChangeExpire").classList.add('btn-toggle-status-on');
		document.getElementById("col-expiration").classList.remove('d-none');        
		document.getElementById("spanTextExpire").innerHTML = "SI";		
		isExpire=true;
		$("#txtDateExpiration").val(found.DATE_EXPIRATION);
	}
	
	$("#txtDatePublication").val(found.DATE_PUBLICATION);
	
	document.getElementById("imgModalAdd").setAttribute("src",found.IMAGE_FIRST)
	
		// $("#hdimgBase64Add").val(""),
		// $("#hdFileNameAdd").val("")
	$("#modal-title").html('Editar Blog');
	$("#modalAddBlog").show();
}

const deleteModal = (id) =>{
	
	Swal.fire({
		title: '¿Desea eliminar?',
		text: "Esta acción no puede deshacerse",
		icon: 'warning',
		showCancelButton: true,
		confirmButtonColor: '#3085d6',
		cancelButtonColor: '#d33',
		confirmButtonText: 'Si, Borralo Ahora!'
	  }).then((result) => {
		if (result.isConfirmed) {		
			
			fetch('../Newsness/DeleteNewsness',{
				method:'POST',
				body: JSON.stringify({ ID : id }),
				headers:{
					'Content-Type': 'application/json'
				}
			})
			.then(response => response.json())
			.then(data=>{

				if(data.status){
					let index = dataList.findIndex(x => x.ID == id);
					dataList.splice(index,1);				
			
					oTable.clear().draw();
					oTable.rows.add(dataList).draw();
	
					Swal.fire({
						title: 'Eliminado!',
						text: "El registro fue eliminado correctamente.",
						icon: 'success',
						confirmButtonColor: '#3085d6',
					})					
					
				}
				
				
				
			})
		  
		}
	  })

	


}

const resetModal = () =>{
		$("#cboCategory").val(""),
		$("#cboType").val(""),
		$("#txtUrl").val(""),
		document.getElementById("btnChangeType").classList.remove('btn-toggle-status-on');
		document.getElementById("spanText").innerHTML = "NO";
		isVideo=false,
		$("#txtTitle").val(""),
		$("#txtDescription").val(""),
		document.getElementById("btnChangeExpire").classList.remove('btn-toggle-status-on');
		document.getElementById("col-expiration").classList.add('d-none');        
        document.getElementById("spanTextExpire").innerHTML = "NO";		
		isExpire=false,
		$("#txtDatePublication").val(""),
		$("#txtDateExpiration").val(""),
		$("#hdimgBase64Add").val(""),
		$("#hdFileNameAdd").val("")
		document.getElementById("imgModalAdd").removeAttribute("src");
}

