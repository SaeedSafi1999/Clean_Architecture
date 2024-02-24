export class HeaderComponent {
    //on initialize
    static Init() {
        var showHeaderAt = 200;

        var win = $(window),
            body = $('body');

        if (win.width() > 400) {
            win.on('scroll', function (e) {

                if (win.scrollTop() > showHeaderAt) {
                    body.addClass('fixed');
                }
                else {
                    body.removeClass('fixed');
                }
            });

        }
    }

    static Alert() {
        alert(10);
    }
}


