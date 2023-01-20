$(function () {
    fnGetPopupConfiguration();
    $("#btn-actualizar-pop-conf").on("click", function () {

        var vIsLinkeable;
        var vIsActive;
        var vIsActiveBanner=false;

        fncreatefeedbackToUser("Actualizando...");
        $("#loading").addClass("flex");

        setTimeout(function () {
            if ($('#rbLinkearSi').is(':checked')) { vIsLinkeable = 'True' }
            else { vIsLinkeable = 'False' }

            if ($('#rbEstadoAct').is(':checked')) { vIsActive = 'True' }
            else { vIsActive = 'False' }

            if ($('#rbEstadoActBanner').is(':checked')) { vIsActiveBanner = 'True' }
            else { vIsActiveBanner = 'False' }
            var obj = {
                'Id': parseInt($("#hd_Id").val()),
                'Url': $("#txt_url").val(),
                'Description': $("#txt_description").val(),
                'LinkImage': $("#txt_linkImage").val(),
                'IsLinkeable': vIsLinkeable,
                'IsActive': vIsActive,
                'UpdateUser': '',

                'TermsBanner': $("#txttermBanners").val(),
                'LinkBanner': $("#txt_lnkImage_banner").val(),
                'IsActiveBanner': vIsActiveBanner
            };

            $.ajax({
                method: "POST",
                url: "../PopupConfiguration/Update",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(obj),
                success: function (response) {
                    $("#loading").removeClass("flex");
                    fncreateAlert('Cambios actualizados', 'success');
                }
            });
        }, 2000);
    });
});

function fnGetPopupConfiguration() {
    $.ajax({
        method: "GET",
        url: "../PopupConfiguration/Get",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response.LINK_IMAGE_BANNER);

            $("#hd_Id").val(response.ID)
            $("#txt_url").val(response.URL);
            $("#txt_description").val(response.DESCRIPTION);
            $("#txt_linkImage").val(response.LINK_IMAGE);


            $("#txt_lnkImage_banner").val(response.LINK_IMAGE_BANNER);
            $("#txttermBanners").val(response.TERMS_CONDITION_BANNER);

            if (response.IS_LINKEABLE == true) {
                $("#rbLinkearSi").attr("checked", "checked");
                console.log('enable');
            } else {
                $("#rbLinkeaNo").attr("checked", "checked");
                console.log('disable');
            }

            console.log(response.IS_ACTIVE);
            if (response.IS_ACTIVE) { $("#rbEstadoAct").attr("checked", "checked"); }
            else { $("#rbEstadoInact").attr("checked", "checked"); }

            if (response.IS_ACTIVE_BANNER) { $("#rbEstadoActBanner").attr("checked", "checked"); }
            else { $("#rbEstadoInactBanner").attr("checked", "checked"); }
            stateInitial();
        }
    });
}


function IoUrlOpen(e) {
    let article = e.parentNode;
    let input = article.querySelector('input').value;
    window.open(input);
}

let boxEnable = document.querySelector('.js-box-url');

function onLinkVisible() {
    boxEnable.classList.remove('hidden');
}

function blurLinkVisible() {
    boxEnable.classList.add('hidden');
}

function stateInitial() {
    if (rbLinkearSi.checked === true) {
        onLinkVisible();
    } else {
        blurLinkVisible();
    }
}


