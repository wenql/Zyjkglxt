(function () {
    appModule.controller('main.views.constitutions.subjects.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.constitution', 'subjectId', 'groupId',
        function ($scope, $uibModalInstance, constitutionService, subjectId, groupId) {
            var vm = this;
            vm.saving = false;
            vm.subject = null;
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            vm.save = function () {
                vm.saving = true;
                constitutionService.createOrUpdateConstitutionSubject(vm.subject).then(function () {
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
                constitutionService.getConstitutionSubjectForEdit({
                    id: subjectId
                }).then(function (result) {
                    vm.subject = result.data;
                    if (vm.subject.id == null && groupId > 0)
                        vm.subject.groupId = groupId;
                });
            }

            init();
        }
    ]);
})();