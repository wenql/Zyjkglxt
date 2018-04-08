(function () {
    appModule.controller('main.views.constitutions.suggests.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.constitution', 'groupId',
        function ($scope, $uibModalInstance, constitutionService, groupId) {
            var vm = this;
            vm.saving = false;
            vm.loading = false;
            vm.suggest = null;
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
            vm.save = function () {
                vm.saving = true;
                constitutionService.createOrUpdateConstitutionSuggest(vm.suggest).then(function () {
                    abp.notify.info('保存成功');
                    $uibModalInstance.close();
                }).finally(function () {
                    vm.saving = false;
                });
            };

            function init() {
                constitutionService.getConstitutionSuggestForEdit({
                    id: groupId
                }).then(function (result) {
                    vm.suggest = result.data;
                }).finally(function () {
                    vm.loading = false;
                });
            }

            init();
        }
    ]);
})();