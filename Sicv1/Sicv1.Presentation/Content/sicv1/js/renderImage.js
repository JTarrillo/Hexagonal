function fnRenderImage(id, objImg, imgbase64, hdStatusOriginalImage = null, maxImageSize) {
    //1000000(1MB) 512000(512KB)
    var exceededSize = (maxImageSize == 1000000) ? "1MB" : "512KB";
    if (id.files[0].size > parseInt(maxImageSize)) {
        alert("El tamaño de la imagen no debe exceder " + exceededSize);
        id.value = "";
        $(imgbase64).val('');
        $(objImg).attr("src", "");
        return;
    };

    var ext = id.files[0].name.split('.');



    if (ext.length > 2) {
        alert("el archivo no puede tener extensión múltiple");
        id.value = "";
        $(imgbase64).val('');
        $(objImg).attr("src", "");
        return;
    }
    if (ext.length < 2) {
        alert("el archivo no válido");
        id.value = "";
        $(imgbase64).val('');
        $(objImg).attr("src", "");
        return;
    }
    if (!/(jpg|png)$/i.test(ext[1])) {
        alert("Debe seleccionar un archivo de extensión jpg o png");
        return;
    }

    if (id.files && id.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(objImg).attr('src', e.target.result);
            $(imgbase64).val(e.target.result);
        };
        reader.readAsDataURL(id.files[0]);

        //1=Elegió una imagen(solo para editar cupón)
        $(hdStatusOriginalImage).val(1);
    }
    else {
        $(imgbase64).val('');
        $(objImg).attr("src", "");
        //0=No eligió una imagen(solo para editar cupón)
        $(hdStatusOriginalImage).val(0);
    }
}

function detectMimeType(b64) {
    var signatures = {
        JVBERi0: "application/pdf",
        R0lGODdh: "image/gif",
        R0lGODlh: "image/gif",
        iVBORw0KGgo: "image/png"
    };

    for (var s in signatures) {
        if (b64.indexOf(s) === 0) {
            return signatures[s];
        }
    }
}