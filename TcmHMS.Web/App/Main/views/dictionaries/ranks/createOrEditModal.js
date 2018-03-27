(function () {
    appModule.controller('main.views.dictionaries.ranks.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.rank', 'rankId',
        function ($scope, $uibModalInstance, rankService, rankId) {
            var vm = this;
            vm.saving = false;
            vm.rank = null;

            vm.save = function () {
                vm.saving = true;
                rankService.createOrUpdateRank(vm.rank).then(function () {
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
                rankService.getRankForEdit({
                    id: rankId
                }).then(function (result) {
                    vm.rank = result.data;
                });
            }

            init();
        }
    ]);
})();