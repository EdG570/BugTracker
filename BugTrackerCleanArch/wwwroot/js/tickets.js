﻿
    var ajaxTickets = (function () {

        var updateStatus = function (ticketId, status) {
            $.ajax({
                type: "POST",
                url: '/Ticket/UpdateStatus',
                data: { id: ticketId, status: status },
                success: function (data) {
                    var ticket = '#' + ticketId;
                    $(ticket).find('.status').text(data.status);
                },
                error: function () {

                }
            });
        };

        var findById = function (id) {
            $.ajax({
                type: "POST",
                url: '/Ticket/FindById',
                data: { id: id },
                success: function (data) {
                    ticketDetail.ui.constructModal(data.ticketFromDb);
                },
                error: function (e) {

                }
            });
        };

        return {
            updateStatus: updateStatus,
            findById: findById
        };

    })();

    var ticketDetail = (function () {
        var $modalCont = $('#ticket-detail-modal');
        var $title = $modalCont.find('#ticket-detail-title').find('.text-val');
        var $id = $modalCont.find('#ticket-detail-id').find('.text-val');
        var $type = $modalCont.find('#ticket-detail-type').find('.text-val');
        var $priority = $modalCont.find('#ticket-detail-priority').find('.text-val');
        var $status = $modalCont.find('#ticket-detail-status').find('.text-val');
        var $description = $modalCont.find('#ticket-detail-description').find('.text-val');
        var $createdBy = $modalCont.find('#ticket-detail-createdby').find('.text-val');
        var $createdOn = $modalCont.find('#ticket-detail-createdon').find('.text-val');
        var $card = $('.comment-card');
        var $commentList = $card.find('.comment-widgets');
        var html = '';
        var commentHtml;
        var $ticketItem = $('.ticket-item');

        var ui = {
            constructModal: function (ticket) {
                $title.text(ticket.title);
                $id.text(ticket.id);
                $type.text(helpers.determineTypeText(ticket.type));
                $priority.text(helpers.determinePriorityText(ticket.priority));
                $status.text(helpers.determineStatusText(ticket.status));
                $description.text(ticket.description);
                $createdBy.text(ticket.createdBy);
                $createdOn.text(ticket.createdAt);

                comments.init(ticket.comments);

                ui.displayModal();
            },
            displayModal: function () {
                $modalCont.modal('show');
            }
        };

        var helpers = {
            determineTypeText: function (ticketType) {
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
            },
            determinePriorityText: function (priority) {
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
            },
            determineStatusText: function (status) {
                switch (status) {
                    case 0:
                        return 'Open';
                        break;
                    case 1:
                        return 'In-Progress';
                        break;
                    case 2:
                        return 'Closed';
                        break;
                    default:
                        return '';
                }
            }
        };

        var comments = {
            clearComments: function () {
                $commentList.empty();
            },
            displayNoCommentsMessage: function () {
                html += '<p class="text-center no-comments-msg mt-3">There are no comments for this ticket</p>';
                $commentList.html(html);
            },
            addComments: function (comments) {
                $.each(comments, function (index, item) {
                    var interpHtml = `<div class="d-flex flex-row comment-row m-t-0">
                                        <div class="comment-text w-100">
                                            <h6 class="font-medium text-muted">${ item.createdBy}</h6> <span class="m-b-15 d-block">${item.description} </span>
                                            <div class="comment-footer"> <span class="text-muted float-right">${ item.createdAt}</span> </div>
                                        </div>
                                      </div>`;

                    $commentList.append(interpHtml);
                });
            },
            init: function (comments) {
                this.clearComments();

                if (comments.length == 0) {
                    this.displayNoCommentsMessage();
                    return false;
                } 

                this.addComments(comments);
            }
        };

        $ticketItem.on('click', function () {
            var $ticket = $(this);
            var ticketId = $ticket.attr('id');
            $('.comment-widgets').attr('id', ticketId);

            ajaxTickets.findById(ticketId);
        });
        

        return {
            ui: ui
        };

    })();

    var createTicket = (function () {
        var $modalInitBtn = $('#ticket-create-init-btn');
        var $createModal = $('#ticket-create-modal');

        $modalInitBtn.on('click', function () {
            $createModal.modal('toggle');
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
                ajaxTickets.updateStatus(ticketId, 'Open');
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
                ajaxTickets.updateStatus(ticketId, 'InProgress');
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
                ajaxTickets.updateStatus(ticketId, 'Closed');
            },
            over: function (event, elem) {
                $(this).addClass("over");
            },
            out: function (event, elem) {
                $(this).removeClass("over");
            }
        });

    })();

    //var editTicket = (function () {
    //    var $formBtn = $('#ticket-edit-form-btn');
    //    var $form = $('#ticket-edit-form');
    //    var $titleInput = $form.find('#edit-title');
    //    var $typeInput = $form.find('#edit-type');
    //    var $priorityInput = $form.find('#edit-priority');
    //    var $assignInput = $form.find('#edit-assign');
    //    var $descriptionInput = $form.find('#edit-description');

    //    $formBtn.on('click', function (e) {
    //        e.preventDefault();

    //        var title = $titleInput.val();
    //        var type = $typeInput.val();
    //        var priority = $priorityInput.val();
    //        var assign = $assignInput.val();
    //        var description = $descriptionInput.val();

    //        ajaxTickets.editTicket(title, type, priority, assign, description);

    //    });

    //    return {

    //    };

    //})();


