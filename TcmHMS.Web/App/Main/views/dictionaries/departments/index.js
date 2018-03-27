(function () {
    appModule.controller('main.views.dictionaries.departments.index', [
        '$scope', '$uibModal', '$templateCache', 'abp.services.app.department', 'uiGridConstants', '$state',
        function ($scope, $uibModal, $templateCache, departmentService, uiGridConstants, $state) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                mLayout.initAjax();
            });

            vm.rowHeight = 50;
            vm.loading = false;
            vm.permissions = {
                diseases: abp.auth.hasPermission('Pages.Dictionaries.Diseases'),
                create: abp.auth.hasPermission('Pages.Dictionaries.Departments.Create'),
                edit: abp.auth.hasPermission('Pages.Dictionaries.Departments.Edit'),
                'delete': abp.auth.hasPermission('Pages.Dictionaries.Departments.Delete')
            };
            vm.requestParams = {
                keyword: ''
            };
            vm.departmentGridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
                appScopeProvider: vm,
                showGridFooter: true,
                rowHeight: vm.rowHeight,
                columnDefs: [
                    {
                        name: '名称',
                        enableColumnMenu: false,
                        field: 'displayName'
                    },
                    {
                        name: '代码',
                        enableColumnMenu: false,
                        field: 'code'
                    },
                    {
                        name: '有效',
                        enableColumnMenu: false,
                        field: 'isEnabled',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <span class="m-badge m-badge--focus m-badge--wide" ng-show="row.entity.isEnabled">是</span>' +
                        '  <span class="m-badge m-badge--danger m-badge--wide" ng-show="!row.entity.isEnabled">否</span>' +
                        '</div>',
                        minWidth: 80
                    },
                    {
                        name: '备注',
                        enableColumnMenu: false,
                        field: 'description'
                    },
                    {
                        name: '创建日期',
                        enableColumnMenu: false,
                        field: 'creationTime',
                        cellFilter: 'momentFormat: \'YYYY/MM/DD HH:mm:ss\''
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
                        '      <li><a ng-if="grid.appScope.permissions.diseases" href=\"javascript:;\" ng-click="grid.appScope.disease(row.entity)">病症管理</a></li>' +
                        '      <li><a ng-if="grid.appScope.permissions.edit" href=\"javascript:;\" ng-click="grid.appScope.editDepartment(row.entity)">编辑</a></li>' +
                        '      <li><a ng-if="!row.entity.isStatic && grid.appScope.permissions.delete" href=\"javascript:;\" ng-click="grid.appScope.deleteDepartment(row.entity)">删除</a></li>' +
                        '    </ul>' +
                        '  </div>' +
                        '</div>'
                    }
                ],
                data: []
            };

            if (!vm.permissions.edit && !vm.permissions.delete && !vm.permissions.diseases) {
                vm.departmentGridOptions.columnDefs.pop();
            }

            vm.getTableHeight = function () {
                return {
                    height: (vm.departmentGridOptions.data.length * vm.rowHeight + 80) + "px"
                };
            };

            vm.getDepartments = function () {
                vm.loading = true;
                departmentService.getDepartments(vm.requestParams).then(function (result) {
                    vm.departmentGridOptions.data = result.data.items;
                }).finally(function () {
                    vm.loading = false;
                });
            };

            vm.disease = function (department) {
                $state.go('diseases', { departmentId: department.id });
            }

            vm.deleteDepartment = function (department) {
                abp.message.confirm(
                    '确认删除科室：' + department.displayName + '?',
                    function (isConfirmed) {
                        if (isConfirmed) {
                            departmentService.deleteDepartment({
                                id: department.id
                            }).then(function () {
                                vm.getDepartments();
                                abp.notify.success('删除成功');
                            });
                        }
                    }
                );
            };

            vm.editDepartment = function (department) {
                openCreateOrEditDepartmentModal(department.id);
            };

            vm.createDepartment = function () {
                openCreateOrEditDepartmentModal(null);
            };

            function openCreateOrEditDepartmentModal(departmentId) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/main/views/dictionaries/departments/createOrEditModal.cshtml',
                    controller: 'main.views.dictionaries.departments.createOrEditModal as vm',
                    backdrop: 'static',
                    resolve: {
                        departmentId: function () {
                            return departmentId;
                        }
                    }
                });
                modalInstance.result.then(function (result) {
                    vm.getDepartments();
                });
            }

            vm.getDepartments();
        }
    ]);
})();