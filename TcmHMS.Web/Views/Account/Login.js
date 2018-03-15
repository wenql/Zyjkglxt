var CurrentPage = function () {
    var handleLogin = function () {
        var $loginForm = $('#loginForm');
        $loginForm.submit(function (e) {
            e.preventDefault();
            if (!$loginForm.valid()) {
                return;
            }
            abp.ui.setBusy(
                null,
                abp.ajax({
                    contentType: 'application/x-www-form-urlencoded',
                    url: $loginForm.attr('action'),
                    data: $loginForm.serialize()
                })
            );
        });
        $('a.social-login-link').click(function () {
            var $a = $(this);
            var $form = $a.closest('form');
            $form.find('input[name=provider]').val($a.attr('data-provider'));
            $form.submit();
        });

        $loginForm.find('input[name=returnUrlHash]').val(location.hash);

        $('input[type=text]').first().focus();
    }
    return {
        init: function () {
            handleLogin();
        }
    };
}();
$(function () {
    if (CurrentPage) {
        CurrentPage.init();
    }
});