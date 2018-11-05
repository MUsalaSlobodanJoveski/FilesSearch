$(function () {
    var chat = $.connection.searchHub;
    chat.client.broadcastMessage = function (name, message) {
        var encodedMsg = $('<div />').text(message).html();
        var arrMsg = JSON.parse(encodedMsg);
        arrMsg.forEach(function (item) {
            $('#discussion').append('<li>' + item.ItemValue + '</li>');
        });
    };

    $('#message').focus();
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            chat.server.send($('#displayname').val(), $('#message').val());
            $('#message').val('').focus();
        });
    });
});