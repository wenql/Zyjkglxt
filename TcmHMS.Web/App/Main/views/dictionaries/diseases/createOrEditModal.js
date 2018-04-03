(function () {
    appModule.controller('main.views.dictionaries.diseases.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.disease', 'diseaseId', 'departmentId',
        function ($scope, $uibModalInstance, diseaseService, diseaseId, departmentId) {
            var vm = this;
            vm.saving = false;
            vm.disease = null;
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            vm.save = function () {
                vm.saving = true;
                diseaseService.createOrUpdateDisease(vm.disease).then(function () {
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
                diseaseService.getDiseaseForEdit({
                    id: diseaseId
                }).then(function (result) {
                    vm.disease = result.data;
                    if (vm.disease.id == null && departmentId > 0)
                        vm.disease.departmentId = departmentId;
                });
            }

            init();
        }
    ]);
})();