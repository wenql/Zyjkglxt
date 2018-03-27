var appModule = angular.module("app", [
    "ui.router",
    "ui.bootstrap",
    'ui.utils',
    "ui.jq",
    "ui.sortable",
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
        $stateProvider.state('dashboard',
            {
                url: '/',
                templateUrl: '/App/Main/views/dashboard/index.cshtml'
            });

        if (abp.auth.hasPermission('Pages.Administration.Roles')) {
            $stateProvider.state('roles',
                {
                    url: '/roles',
                    templateUrl: '/App/Main/views/administration/roles/index.cshtml'
                });
        }

        if (abp.auth.hasPermission('Pages.Administration.Users')) {
            $stateProvider.state('users',
                {
                    url: '/users',
                    templateUrl: '/App/Main/views/administration/users/index.cshtml'
                });
        }
        if (abp.auth.hasPermission('Pages.Dictionaries.Departments'))
            $stateProvider.state('departments',
                {
                    url: '/departments',
                    templateUrl: '/App/Main/views/dictionaries/departments/index.cshtml'
                });
        if (abp.auth.hasPermission('Pages.Dictionaries.Diseases'))
            $stateProvider.state('diseases',
                {
                    url: '/diseases:departmentId',
                    templateUrl: '/App/Main/views/dictionaries/diseases/index.cshtml'
                });
        if (abp.auth.hasPermission('Pages.Dictionaries.Ranks'))
            $stateProvider.state('ranks',
                {
                    url: '/ranks',
                    templateUrl: '/App/Main/views/dictionaries/ranks/index.cshtml'
                });
    }
]);
appModule.run(["$rootScope", "$state", 'i18nService', '$uibModalStack', function ($rootScope, $state, i18nService, $uibModalStack) {
    $rootScope.$state = $state;
    $rootScope.$on('$stateChangeSuccess', function () {
        $uibModalStack.dismissAll();
    });

    //Set Ui-Grid language
    i18nService.setCurrentLang("zh-cn");

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