(function () {
    appModule.controller('main.views.layout.header', [
        '$rootScope', '$scope',
        function ($rootScope, $scope) {
            var vm = this;
            $scope.$on('$includeContentLoaded', function () {
                mLayout.initHeader(); // init header           
            });         
        }
    ]);
})();