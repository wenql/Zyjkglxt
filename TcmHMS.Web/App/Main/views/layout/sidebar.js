(function () {
    appModule.controller('main.views.layout.sidebar', [
        '$scope', '$state',
        function ($scope, $state) {
            var vm = this;
            vm.menu = abp.nav.menus.MainMenu;
            $scope.$on('$includeContentLoaded', function () {
                setTimeout(function () {
                    mLayout.initAside();
                    $(window).trigger('resize');
                }, 0);
            });
        }
    ]);
})();