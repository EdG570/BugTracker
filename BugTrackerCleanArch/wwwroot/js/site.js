var modals = (function () {
    var projectCreateModal = $('#project-create-modal');
    var projectEditModal = $('#project-edit-modal');
    var ticketCreateModal = $('#ticket-create-modal');

    var projectCreateBtn = $('#project-create-btn');
    var ticketCreateBtn = $('#create-ticket-btn');

    projectCreateBtn.on('click', function () {
        projectCreateModal.modal('toggle');
    });

    ticketCreateBtn.on('click', function () {
        ticketCreateModal.modal('toggle');
    });

    var init = function () {
        projectEditModal.modal('show');
    };

    return {
        init: init
    };

})();

$(document).ready(function () {
    var ticketSearchInput = $('#ticket-table-search');
    var ticketSearchBtn = $('#ticket-search-btn');

    modals.init();

    ticketSearchBtn.on('click', function () {
        var textVal = ticketSearchInput.val();
        var str = 'tr:not(:has(th)):not(:contains(' + textVal + '))';
        var show = 'tr:not(:has(th)):contains(' + textVal + ')';
        $(str).hide()
        $(show).show();
    });
});