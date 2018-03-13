var App = App || {};
(function () {

    var appLocalizationSource = abp.localization.getSource('TcmHMS');
    App.localize = function () {
        return appLocalizationSource.apply(this, arguments);
    };

})(App);