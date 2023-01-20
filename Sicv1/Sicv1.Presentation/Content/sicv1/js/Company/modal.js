function fncreateModal(modalId = null,
    modalTitle = null,
    modalBody = null,
    modalbuttonText = null) {
    var modal = '';
    modal += '<div class="customModal" id=' + modalId + '>';
    modal += '    <div class="modal-dialog modal-dialog-centered" role="document">                                                                     ';
    modal += '        <div class="modal-content">                                                                                                      ';
    modal += '            <div class="modal-header">                                                                                                   ';
    modal += '                <h5 class="modal-title">' + modalTitle + '</h5>                                                      ';
    modal += '                <button type="button" id="btn-x" class="close" data-dismiss="modal" aria-label="Close">                                             ';
    modal += '                    <span aria-hidden="true">&times;</span>                                                                              ';
    modal += '                </button>                                                                                                                ';
    modal += '            </div>                                                                                                                       ';
    modal += '            <div class="modal-body">                                                                                                     ';
    modal += '                ' + modalBody + '                                                                                    ';
    modal += '            </div>                                                                                                                           ';
    modal += '            <div class="modal-footer">                                                                                                   ';
    modal += '                <button type="button" id="btn-close" class="btn btn-pink" data-dismiss="modal">Cerrar</button>                                      ';
    modal += '                <button type="button" id="btn' + modalbuttonText + '" class="btn btn-blue">' + modalbuttonText + '</button>                                                      ';
    modal += '            </div>                                                                                                                       ';
    modal += '        </div>                                                                                                                           ';
    modal += '    </div>                                                                                                                               ';
    modal += '</div>                                                                                                                                   ';
    return modal;
}