const Toastito = Swal.mixin({
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

const ModalToast = async(icon,title,body,footer=null) =>{
  Swal.fire({
    icon: icon,
    title: title,
    text: body,
    footer: footer
  })
}

const ConfirmDialog = async(title,icon,confirmButtonText) =>{
    let res = false;
    await Swal.fire({
        title: title,
        text: 'Este cambio no se puede revertir.',
        icon: icon,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText:  'Cancelar',
        confirmButtonText: confirmButtonText
      }).then((result) => {
        if (result.isConfirmed) {
            res = true;
        }else{
            res = false;
        }
      })

    return res;
}

