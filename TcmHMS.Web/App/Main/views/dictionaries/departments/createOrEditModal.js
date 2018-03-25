(function () {
    appModule.controller('main.views.dictionaries.departments.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.department', 'departmentId',
        function ($scope, $uibModalInstance, departmentService, departmentId) {
            var vm = this;
            vm.saving = false;
            vm.department = null;

            vm.save = function () {
                vm.saving = true;
                departmentService.createOrUpdateDepartment(vm.department).then(function () {
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
                departmentService.getDepartmentForEdit({
                    id: departmentId
                }).then(function (result) {
                    vm.department = result.data;
                });
            }

            init();
        }
    ]);
})();