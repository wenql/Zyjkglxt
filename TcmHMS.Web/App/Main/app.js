var appModule = angular.module("app", [
    "ui.router",
    "ui.bootstrap",
    'ui.utils',
    "ui.jq",
    'ui.grid',
    'ui.grid.pagination',
    "oc.lazyLoad",
    "ngSanitize",
    'angularFileUpload',
    'daterangepicker',
    'angularMoment',
    'frapontillo.bootstrap-switch',
    'abp'
]);
appModule.config([
    '$stateProvider', '$urlRouterProvider', '$locationProvider', '$qProvider',
    function ($stateProvider, $urlRouterProvider, $locationProvider, $qProvider) {
        $locationProvider.hashPrefix('');
        $urlRouterProvider.otherwise('/');
        $qProvider.errorOnUnhandledRejections(false);
        $stateProvider
            .state('dashboard',
            {
                url: '/',
                templateUrl: '/App/Main/views/dashboard/index.cshtml'
            })
            .state('roles',
            {
                url: '/roles',
                templateUrl: '/App/Main/views/administration/roles/index.cshtml'
            });
    }
]);
appModule.run(["$rootScope", "$state", '$uibModalStack', function ($rootScope, $state, $uibModalStack) {
    $rootScope.$state = $state;
    $rootScope.$on('$stateChangeSuccess', function () {
        $uibModalStack.dismissAll();
    });
    $rootScope.safeApply = function (fn) {
        var phase = this.$root.$$phase;
        if (phase === '$apply' || phase === '$digest') {
            if (fn && (typeof (fn) === 'function')) {
                fn();
            }
        } else {
            this.$apply(fn);
        }
    };
}]);