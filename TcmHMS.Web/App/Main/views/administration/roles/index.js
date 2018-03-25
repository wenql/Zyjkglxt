(function () {
    appModule.controller('main.views.administration.roles.index', [
        '$scope', '$uibModal', '$templateCache', 'abp.services.app.role', 'uiGridConstants',
        function ($scope, $uibModal, $templateCache, roleService, uiGridConstants) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                mLayout.initAjax();
            });

            vm.rowHeight = 50;
            vm.loading = false;
            vm.permissions = {
                create: abp.auth.hasPermission('Pages.Administration.Roles.Create'),
                edit: abp.auth.hasPermission('Pages.Administration.Roles.Edit'),
                'delete': abp.auth.hasPermission('Pages.Administration.Roles.Delete')
            };
            vm.requestParams = {
                permission: ''
            };
            vm.roleGridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
                appScopeProvider: vm,
                showGridFooter: true,
                rowHeight: vm.rowHeight,
                columnDefs: [
                    {
                        name: '名称',
                        enableColumnMenu: false,
                        field: 'displayName',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  {{COL_FIELD CUSTOM_FILTERS}} &nbsp;' +
                        '  <span ng-show="row.entity.isStatic" class="m-badge m-badge--brand m-badge--wide ng-tns-c2-0 ng-star-inserted" title="不能删除系统角色.">系统</span>&nbsp;' +
                        '  <span ng-show="row.entity.isDefault" class="m-badge m-badge--metal m-badge--wide ng-tns-c2-0 ng-star-inserted" title="新用户将默认拥有此角色.">默认</span>' +
                        '</div>'
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
                        '      <li><a ng-if="grid.appScope.permissions.edit" href=\"javascript:;\" ng-click="grid.appScope.editRole(row.entity)">编辑</a></li>' +
                        '      <li><a ng-if="!row.entity.isStatic && grid.appScope.permissions.delete" href=\"javascript:;\" ng-click="grid.appScope.deleteRole(row.entity)">删除</a></li>' +
                        '    </ul>' +
                        '  </div>' +
                        '</div>'
                    }
                ],
                data: []
            };

            if (!vm.permissions.edit && !vm.permissions.delete) {
                vm.roleGridOptions.columnDefs.pop();
            }

            vm.getTableHeight = function () {
                return {
                    height: (vm.roleGridOptions.data.length * vm.rowHeight + 80) + "px"
                };
            };

            vm.getRoles = function () {
                vm.loading = true;
                roleService.getRoles(vm.requestParams).then(function (result) {
                    vm.roleGridOptions.data = result.data.items;
                }).finally(function () {
                    vm.loading = false;
                });
            };

            vm.deleteRole = function (role) {
                abp.message.confirm(
                    '确认删除角色：' + role.displayName + '?',
                    function (isConfirmed) {
                        if (isConfirmed) {
                            roleService.deleteRole({
                                id: role.id
                            }).then(function () {
                                vm.getRoles();
                                abp.notify.success('删除成功');
                            });
                        }
                    }
                );
            };

            vm.editRole = function (role) {
                openCreateOrEditRoleModal(role.id);
            };

            vm.createRole = function () {
                openCreateOrEditRoleModal(null);
            };

            function openCreateOrEditRoleModal(roleId) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/main/views/administration/roles/createOrEditModal.cshtml',
                    controller: 'main.views.administration.roles.createOrEditModal as vm',
                    backdrop: 'static',
                    resolve: {
                        roleId: function () {
                            return roleId;
                        }
                    }
                });
                modalInstance.result.then(function (result) {
                    vm.getRoles();
                });
            }

            vm.getRoles();
        }
    ]);
})();