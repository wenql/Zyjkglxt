﻿(function () {
    appModule.controller('main.views.layout', [
        '$scope',
        function ($scope) {
            $scope.$on('$viewContentLoaded', function () {
                mApp.initComponents(); // init core components
            });
        }
    ]);
})();