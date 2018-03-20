﻿(function () {
    appModule.controller('main.views.administration.roles.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.role', 'roleId',
        function ($scope, $uibModalInstance, roleService, roleId) {
            var vm = this;

            vm.saving = false;
            vm.role = null;
            vm.permissionEditData = null;

            vm.save = function () {
                vm.saving = true;
                roleService.createOrUpdateRole({
                    role: vm.role,
                    grantedPermissionNames: vm.permissionEditData.grantedPermissionNames
                }).then(function () {
                    abp.notify.info('保存成功');
                    $uibModalInstance.close();
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            function init() {
                roleService.getRoleForEdit({
                    id: roleId
                }).then(function (result) {
                    vm.role = result.data.role;
                    vm.permissionEditData = {
                        permissions: result.data.permissions,
                        grantedPermissionNames: result.data.grantedPermissionNames
                    };
                });
            }

            init();
        }
    ]);
})();