var ajax = (function () {

    var updateTicketStatus = function (ticketId, status) {
        $.ajax({
            type: "POST",
            url: '/Ticket/UpdateStatus',
            data: { id: ticketId, status: status },
            success: function (data) {
                var ticket = '#' + ticketId;
                $(ticket).find('.status').text(status);
            },
            error: function () {

            }
        });
    };

    var findTicketById = function (id) {
        $.ajax({
            type: "POST",
            url: '/Ticket/FindById',
            data: { id: id },
            success: function (data) {
                console.log(data);
                ui.buildTicketDetailModal(data.ticketFromDb);
            },
            error: function (e) {

            }
        });
    };

    return {
        updateTicketStatus: updateTicketStatus,
        findTicketById: findTicketById
    };
})();

var modalManager = (function () {

    var displayModal = function ($modal) {
        console.log('display');
        $modal.modal('show');
    };

    return {
        displayModal: displayModal
    };

})();

var ui = (function () {

    var buildTicketDetailModal = function (ticket) {
        var $modalCont = $('#ticket-detail-modal');
        var $title = $modalCont.find('#ticket-detail-title').find('.text-val');
        var $id = $modalCont.find('#ticket-detail-id').find('.text-val');
        var $type = $modalCont.find('#ticket-detail-type').find('.text-val');
        var $priority = $modalCont.find('#ticket-detail-priority').find('.text-val');
        var $status = $modalCont.find('#ticket-detail-status').find('.text-val');
        var $description = $modalCont.find('#ticket-detail-description').find('.text-val');
        var $createdBy = $modalCont.find('#ticket-detail-createdby').find('.text-val');
        var $createdOn = $modalCont.find('#ticket-detail-createdon').find('.text-val');

        $title.text(ticket.title);
        $id.text(ticket.id);
        $type.text(determineTypeText(ticket.type));
        $priority.text(determinePriorityText(ticket.priority));
        $status.text(determineStatusText(ticket.status));
        $description.text(ticket.description);
        $createdBy.text(ticket.createdBy);
        $createdOn.text(ticket.createdAt);

        addComments(ticket.comments);

        modalManager.displayModal($modalCont);
    };

    var determineTypeText = function (ticketType) {
        switch (ticketType) {
            case 0:
                return 'Task';
                break;
            case 1:
                return 'Feature';
                break;
            case 2:
                return 'Bug';
                break;
            default:
                return '';
        }
    };

    var determinePriorityText = function (priority) {
        switch (priority) {
            case 0:
                return 'Low';
                break;
            case 1:
                return 'Medium';
                break;
            case 2:
                return 'High';
                break;
            case 3:
                return 'Urgent';
                break;
            default:
                return '';
        }
    };

    var determineStatusText = function (status) {
        switch (status) {
            case 0:
                return 'Open';
                break;
            case 2:
                return 'In-Progress';
                break;
            case 4:
                return 'Closed';
                break;
            default:
                return '';
        }
    };

    var addComments = function (comments) {
        var $card = $('.comment-card');
        var $commentList = $card.find('.comment-widgets');
        var html = '';
        var commentHtml;

        $commentList.empty();

        if (comments.length == 0) {
            html += '<p class="text-center no-comments-msg mt-3">There are no comments for this ticket</p>';
            $commentList.html(html);

            return false;
        }

        $.each(comments, function (index, item) {
            var interpHtml = `<div class="d-flex flex-row comment-row m-t-0">
                                <div class="comment-text w-100">
                                    <h6 class="font-medium text-muted">${ item.createdBy}</h6> <span class="m-b-15 d-block">${item.description} </span>
                                    <div class="comment-footer"> <span class="text-muted float-right">${ item.createdAt}</span> </div>
                                </div>
                              </div>`;

            $commentList.append(interpHtml);
        });

    };

    return {
        buildTicketDetailModal: buildTicketDetailModal,
        addComments: addComments
    };

})();

var detail = (function () {
    var $ticketItem = $('.ticket-item');

    $ticketItem.on('click', function () {
        var $ticket = $(this);
        var ticketId = $ticket.attr('id');

        ajax.findTicketById(ticketId);
    });


    return {

    };

})();

var dragDrop = (function () {

    var $backlogContainer = $('#backlog-container');
    var $devContainer = $('#dev-container');
    var $completedContainer = $('#completed-container');

    var $ticketBacklog = $('.ticket-item', $backlogContainer);
    var $ticketDev = $('.ticket-item', $devContainer);
    var $ticketCompleted = $('.ticket-item', $completedContainer);

    $ticketBacklog.draggable({
        revert: "invalid",
        containment: "document",
        helper: "clone",
        cursor: "move"
    });

    $ticketDev.draggable({
        revert: "invalid",
        containment: "document",
        helper: "clone",
        cursor: "move"
    });

    $ticketCompleted.draggable({
        revert: "invalid",
        containment: "document",
        helper: "clone",
        cursor: "move"
    });

    $backlogContainer.droppable({
        drop: function (event, ui) {
            var dropped = ui.draggable;
            var droppedOn = $(this);
            var ticketId = dropped.attr('id');

            $(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);
            ajax.updateTicketStatus(ticketId, 'Open');
        },
        over: function (event, elem) {
            $(this).addClass("over");
        },
        out: function (event, elem) {
            $(this).removeClass("over");
        }
    });

    $devContainer.droppable({
        drop: function (event, ui) {
            var dropped = ui.draggable;
            var droppedOn = $(this);
            var ticketId = dropped.attr('id');

            $(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);
            ajax.updateTicketStatus(ticketId, 'InProgress');
        },
        over: function (event, elem) {
            $(this).addClass("over");
        },
        out: function (event, elem) {

        }
    });

    $completedContainer.droppable({
        drop: function (event, ui) {
            var dropped = ui.draggable;
            var droppedOn = $(this);
            var ticketId = dropped.attr('id');

            $(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);
            ajax.updateTicketStatus(ticketId, 'Closed');
        },
        over: function (event, elem) {
            $(this).addClass("over");
        },
        out: function (event, elem) {
            $(this).removeClass("over");
        }
    });

})();

$(document).ready(function () {

});