(function () {
    appModule.controller('main.views.administration.roles.index', [
        '$scope', '$uibModal', '$templateCache', 'abp.services.app.role',
        function ($scope, $uibModal, $templateCache, roleService) {
            var vm = this;
            vm.roles = [];
            roleService.getAll({}).then(function (result) {
                vm.roles = result.data.items;
            });

    
        }
    ]);
})();