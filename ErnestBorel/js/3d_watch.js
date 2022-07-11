$(document).ready(function () {
    $(".watchItem").colorbox({ inline: true, href: "#videoDiv", innerWidth: 960, innerHeight: 540,
        onOpen: function (r) {
            var videoPath = ($(r.el).attr("data-video"));
            jwplayer('mediaplayer').setup({
                'flashplayer': '/player.swf',
                'id': 'playerID',
                'width': '960',
                'height': '540',
                'file': videoPath
            });
            setTimeout(function () {
                jwplayer('mediaplayer').play();
            }, 500);
        },
        onClosed: function () {
            jwplayer('mediaplayer').stop();
            jwplayer('mediaplayer').remove();
        }
    });
});