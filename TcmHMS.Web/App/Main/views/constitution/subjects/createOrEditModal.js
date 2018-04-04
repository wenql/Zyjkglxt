(function () {
    appModule.controller('main.views.constitutions.subjects.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.app.constitution', 'subjectId', 'groupId',
        function ($scope, $uibModalInstance, constitutionService, subjectId, groupId) {
            var vm = this;
            vm.saving = false;
            vm.loading = false;
            vm.subject = null;
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
            vm.save = function () {
                if (vm.subject.options.length > 0) {
                    vm.saving = true;
                    constitutionService.createOrUpdateConstitutionSubject(vm.subject).then(function() {
                        abp.notify.info('保存成功');
                        $uibModalInstance.close();
                    }).finally(function() {
                        vm.saving = false;
                    });
                } else {
                    abp.message.warn('请添加选项');
                }
            };

            vm.create = function () {
                vm.subject.options.push(addRow());
            };

            function addRow() {
                return {
                    id: 0,
                    subjectId: subjectId,
                    displayName: '',
                    displayOrder: 0,
                    score: 0
                };
            }

            vm.remove = function (index) {
                //abp.message.confirm(
                //    '确认删除?',
                //    function (isConfirmed) {
                //        if (isConfirmed) {
                vm.subject.options.splice(index, 1);
                //        }
                //    }
                //);
            }

            $scope.sortableOptions = {
                update: function (e, ui) { },
                stop: function (e, ui) { }
            }

            function init() {
                constitutionService.getConstitutionSubjectForEdit({
                    id: subjectId
                }).then(function (result) {
                    vm.subject = result.data;
                    vm.subject.specifyGebder = vm.subject.specifyGebder.toString();

                    if (vm.subject.options.length==0) {
                        vm.subject.options = [];
                        vm.subject.options.push(addRow());
                    }

                    if (vm.subject.id == null && groupId > 0) {
                        vm.subject.groupId = groupId;
                    }

                }).finally(function () {
                    vm.loading = false;
                });
            }

            init();
        }
    ]);
})();