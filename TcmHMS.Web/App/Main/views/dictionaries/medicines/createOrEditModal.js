(function () {
    appModule.controller('main.views.dictionaries.medicines.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.medicine', 'medicineId',
        function ($scope, $uibModalInstance, medicineService, medicineId) {
            var vm = this;
            vm.saving = false;
            vm.medicine = null;
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            vm.save = function () {
                vm.saving = true;
                medicineService.createOrUpdateMedicine(vm.medicine).then(function () {
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
                medicineService.getMedicineForEdit({
                    id: medicineId
                }).then(function (result) {
                    vm.medicine = result.data;
                });
            }

            init();
        }
    ]);
})();