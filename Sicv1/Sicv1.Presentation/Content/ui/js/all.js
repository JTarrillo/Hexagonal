function removeTooltip() {
    let tooltip = document.querySelectorAll('.io-popconfirm');
    for (const item of tooltip) {
        item.classList.remove('open')
    }
}

function openTooltip(e) {
    removeTooltip();
    e.parentNode.classList.add('open')
}


function successDelete() {
    fncreateAlert('Cupon eliminado', 'success')
    setTimeout(removeTooltip, 400)
}