var uiComments = (function (description) {
    var $comments = $('.comment-widgets');
    var $newCommentForm = $('#new-comment-form');

    var addNewCommentToDom = function (comment) {
        $comments.append(comment);
    };

    var getCommentHtml = function (comment) {
        return `<div class="d-flex flex-row comment-row m-t-0">
                    <div class="comment-text w-100">
                        <h6 class="font-medium text-muted">${ comment.createdBy}</h6> <span class="m-b-15 d-block">${comment.description} </span>
                        <div class="comment-footer"> <span class="text-muted float-right">${ comment.createdAt}</span> </div>
                     </div>
                </div>`;
    };

    var generateNewComment = function (comment) {
        var commentHtml = getCommentHtml(comment);
        hideNoCommentsMessage();
        addNewCommentToDom(commentHtml);
        clearCommentInput();
    };

    var clearCommentInput = function () {
        $newCommentForm.find('#comment-create').val('');
    };

    var hideNoCommentsMessage = function () {
        $('.no-comments-msg').hide();
    };

    return {
        generateNewComment: generateNewComment
    };

})();

var ajaxComments = (function () {

    var create = function (ticketId, description) {
        $.ajax({
            type: "POST",
            url: '/Comment/Create',
            data: { ticketId: ticketId, description: description },
            success: function (data) {
                uiComments.generateNewComment(data.comment);
            },
            error: function () {

            }
        });
    };

    return {
        create: create
    };

})();

var evenListenersComments = (function () {
    var $newCommentForm = $('#new-comment-form');
    var $newCommentBtn = $('#new-comment-btn');

    $newCommentBtn.on('click', function () {

        event.preventDefault();

        var description = $newCommentForm.find('#comment-create').val();
        var ticketId = $('.comment-widgets').attr('id');

        ajaxComments.create(ticketId, description);
    });
})();