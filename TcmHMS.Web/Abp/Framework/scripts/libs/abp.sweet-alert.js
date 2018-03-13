var abp = abp || {};
(function ($) {
    if (!layer || !$) {
        return;
    }

    /* DEFAULTS *************************************************/

    abp.libs = abp.libs || {};

    var showMessage = function (type, message, title) {
        if (!title) {
            title = message;
            message = undefined;
        }

        layer.open({
            icon: type,
            title: title,
            content: message,
            skin: 'layui-layer-lan'
        });
    };

    abp.message.info = function (message, title) {
        return showMessage(-1, message, title);
    };

    abp.message.success = function (message, title) {
        return showMessage(1, message, title);
    };

    abp.message.warn = function (message, title) {
        return showMessage(0, message, title);
    };

    abp.message.error = function (message, title) {
        return showMessage(2, message, title);
    };

    abp.message.confirm = function (message, titleOrCallback, callback) {
        var userOpts = {
            icon: 3,
            title: '提示',
            skin: 'layui-layer-lan'
        };

        if ($.isFunction(titleOrCallback)) {
            callback = titleOrCallback;
        } else if (titleOrCallback) {
            userOpts.title = titleOrCallback;
        };

        layer.confirm(message, userOpts, function (index) {
            callback && callback(true),
                layer.close(index);
        }, function (index) {
            callback && callback(false),
                layer.close(index);
        });
    };

})(jQuery);