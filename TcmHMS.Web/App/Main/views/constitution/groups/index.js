(function () {
    appModule.controller('main.views.constitutions.groups.index', [
        '$scope', '$uibModal', '$templateCache', 'abp.services.app.constitution', 'uiGridConstants', '$state',
        function ($scope, $uibModal, $templateCache, constitutionService, uiGridConstants, $state) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                mLayout.initAjax();
            });

            vm.rowHeight = 50;
            vm.loading = false;
            vm.permissions = {
                subject: abp.auth.hasPermission('Pages.Constitutions.Subjects'),
                suggest: abp.auth.hasPermission('Pages.Constitutions.Suggests')
            };
            vm.constitutionGridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
                appScopeProvider: vm,
                showGridFooter: true,
                rowHeight: vm.rowHeight,
                columnDefs: [
                    {
                        name: '名称',
                        enableColumnMenu: false,
                        field: 'groupName'
                    },
                    {
                        name: '操作',
                        enableSorting: false,
                        enableColumnMenu: false,
                        width: 150,
                        //cellClass:'text-center',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <div class="btn-group dropdown" uib-dropdown="" dropdown-append-to-body>' +
                        '    <button class="btn btn-xs btn-primary blue" uib-dropdown-toggle="" aria-haspopup="true" aria-expanded="false"><i class="fa fa-cog"></i>操作</button>' +
                        '    <ul uib-dropdown-menu>' +
                        '      <li><a ng-if="grid.appScope.permissions.subject" href=\"javascript:;\" ng-click="grid.appScope.subjects(row.entity)">体质问卷</a></li>' +
                        '      <li><a ng-if="grid.appScope.permissions.suggest" href=\"javascript:;\" ng-click="grid.appScope.suggests(row.entity)">健康建议</a></li>' +
                        '    </ul>' +
                        '  </div>' +
                        '</div>'
                    }
                ],
                data: []
            };

            if (!vm.permissions.subject && !vm.permissions.suggest) {
                vm.constitutionGridOptions.columnDefs.pop();
            }

            vm.getTableHeight = function () {
                return {
                    height: (vm.constitutionGridOptions.data.length * vm.rowHeight + 80) + "px"
                };
            };

            vm.subjects = function (group) {
                $state.go('constitutionSubjects', { groupId: group.groupId });
            }

            vm.suggests = function (group) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/main/views/constitution/suggests/createOrEditModal.cshtml',
                    controller: 'main.views.constitutions.suggests.createOrEditModal as vm',
                    backdrop: 'static',
                    resolve: {
                        groupId: function () {
                            return group.groupId;
                        }
                    }
                });

                modalInstance.result.then(function (result) {});
            }

            vm.getConstitutionGroups = function () {
                vm.loading = true;
                constitutionService.getConstitutionGroups({}).then(function (result) {
                    vm.constitutionGridOptions.data = result.data.items;
                }).finally(function () {
                    vm.loading = false;
                });
            };

            vm.getConstitutionGroups();
        }
    ]);
})();