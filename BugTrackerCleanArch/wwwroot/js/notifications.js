var acknowledge = (function () {
    var $declineBtn = $('.decline');
    var $acceptBtn = $('.accept');
    var $notificationCont = $('.notification-cont');

    var ajax = {
        respondProjectInvite: function (notificationId, isAccepted) {
            $.ajax({
                type: "POST",
                url: '/Notification/Acknowledge',
                data: { notificationId, isAccepted },
                success: function (data) {
                    ui.removeNotification(data.notificationId);
                },
                error: function (e) {

                }
            });
        }
    };

    var ui = {
        removeNotification: function (id) {
            var notificationId = '#' + id;
            var $buttonCont = $notificationCont.find(notificationId);

            $buttonCont.prev('div').prev('p').remove();
            $buttonCont.prev('div').remove();
            $buttonCont.next('div').remove();
            $buttonCont.remove();

            redirect.redirectToAction('/Project/Index');
        }

    };

    var redirect = {
        redirectToAction: function (action) {
            window.location.href = action;
        }
    };

    $declineBtn.on('click', function () {
        ajax.respondProjectInvite($(this).parent('.dropdown-item').attr('id'), false);
    });

    $acceptBtn.on('click', function () {
        ajax.respondProjectInvite($(this).parent('.dropdown-item').attr('id'), true);
    });

    return {

    };
})();