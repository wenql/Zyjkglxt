(function () {
    appModule.controller('main.views.administration.users.index',
        [
            '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.user',
            function ($scope, $uibModal, $stateParams, uiGridConstants, userService) {
                var vm = this;
                $scope.$on('$viewContentLoaded',
                    function () {
                        mLayout.initAjax();
                    });

                vm.loading = false;
                vm.rowHeight = 50;
                vm.currentUserId = abp.session.userId;
                vm.permissions = {
                    create: abp.auth.hasPermission('Pages.Administration.Users.Create'),
                    edit: abp.auth.hasPermission('Pages.Administration.Users.Edit'),
                    changePermissions: abp.auth.hasPermission('Pages.Administration.Users.ChangePermissions'),
                    impersonation: abp.auth.hasPermission('Pages.Administration.Users.Impersonation'),
                    'delete': abp.auth.hasPermission('Pages.Administration.Users.Delete'),
                    roles: abp.auth.hasPermission('Pages.Administration.Roles')
                };

                vm.requestParams = {
                    Filter: '',
                    permission: '',
                    role: parseInt(0),
                    skipCount: 0,
                    maxResultCount: app.consts.grid.defaultPageSize,
                    sorting: 'creationTime asc'
                };

                vm.userGridOptions = {
                    enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
                    enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
                    paginationPageSizes: app.consts.grid.defaultPageSizes,
                    paginationPageSize: app.consts.grid.defaultPageSize,
                    useExternalPagination: true,
                    useExternalSorting: true,
                    rowHeight: vm.rowHeight,
                    paginationTemplate: "/app/main/directives/paginationTemplate.cshtml",
                    appScopeProvider: vm,
                    rowTemplate: '<div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'text-muted\': !row.entity.isActive }"  ui-grid-cell></div>',
                    columnDefs: [
                        {
                            name: '用户名',
                            enableColumnMenu: false,
                            field: 'userName',
                            minWidth: 140
                        },
                        {
                            name: '姓名',
                            enableColumnMenu: false,
                            field: 'name',
                            minWidth: 120
                        },
                        {
                            name: '角色',
                            enableColumnMenu: false,
                            field: 'getRoleNames()',
                            enableSorting: false,
                            minWidth: 160
                        },
                        {
                            name: '邮箱地址',
                            enableColumnMenu: false,
                            field: 'emailAddress',
                            minWidth: 200
                        },
                        {
                            name: '激活',
                            enableColumnMenu: false,
                            field: 'isActive',
                            cellTemplate:
                            '<div class=\"ui-grid-cell-contents\">' +
                            '  <span class="m-badge m-badge--focus m-badge--wide" ng-show="row.entity.isActive">是</span>' +
                            '  <span class="m-badge m-badge--danger m-badge--wide" ng-show="!row.entity.isActive">否</span>' +
                            '</div>',
                            minWidth: 80
                        },
                        {
                            name: '上次登录时间',
                            enableColumnMenu: false,
                            field: 'lastLoginTime',
                            cellFilter: 'momentFormat: \'YYYY/MM/DD HH:mm:ss\'',
                            minWidth: 100
                        },
                        {
                            name: '创建时间',
                            enableColumnMenu: false,
                            field: 'creationTime',
                            cellFilter: 'momentFormat: \'YYYY/MM/DD\'',
                            minWidth: 100
                        },
                        {
                            name: '操作',
                            enableSorting: false,
                            enableColumnMenu: false,
                            width: 150,
                            cellTemplate:
                            '<div class=\"ui-grid-cell-contents\">' +
                            '  <div class="btn-group dropdown" uib-dropdown="" dropdown-append-to-body>' +
                            '    <button class="btn btn-xs btn-primary blue" uib-dropdown-toggle="" aria-haspopup="true" aria-expanded="false"><i class="fa fa-cog"></i>操作</button>' +
                            '    <ul uib-dropdown-menu>' +
                            '      <li><a href="javascript:;" ng-if="grid.appScope.permissions.impersonation && row.entity.id != grid.appScope.currentUserId" ng-click="grid.appScope.impersonate(row.entity)">切换至此用户</a></li>' +
                            '      <li><a href="javascript:;" ng-if="grid.appScope.permissions.edit" ng-click="grid.appScope.editUser(row.entity)">修改</a></li>' +
                            '      <li><a href="javascript:;" ng-if="grid.appScope.permissions.changePermissions" ng-click="grid.appScope.editPermissions(row.entity)">权限</a></li>' +
                            '      <li><a href="javascript:;" ng-if="!row.entity.isActive" ng-click="grid.appScope.unlockUser(row.entity)">解锁</a></li>' +
                            '      <li><a href="javascript:;" ng-if="grid.appScope.permissions.delete" ng-click="grid.appScope.deleteUser(row.entity)">删除</a></li>' +
                            '    </ul>' +
                            '  </div>' +
                            '</div>'
                        }
                    ],
                    onRegisterApi: function (gridApi) {
                        $scope.gridApi = gridApi;
                        $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                            if (!sortColumns.length || !sortColumns[0].field) {
                                vm.requestParams.sorting = null;
                            } else {
                                vm.requestParams.sorting = sortColumns[0].field + ' ' + sortColumns[0].sort.direction;
                            }

                            vm.getUsers();
                        });
                        gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                            vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                            vm.requestParams.maxResultCount = pageSize;

                            vm.getUsers();
                        });
                    },
                    data: []
                };

                vm.getTableHeight = function () {
                    return {
                        height: (vm.userGridOptions.data.length * vm.rowHeight + 100) + "px"
                    };
                };

                vm.getUsers = function () {
                    vm.loading = true;
                    var params = $.extend({ filter: vm.filterText }, vm.requestParams);
                    params.role = params.role === 0 ? '' : params.role;
                    userService.getUsers($.extend({ filter: vm.filterText }, vm.requestParams))
                        .then(function (result) {
                            vm.userGridOptions.totalItems = result.data.totalCount;
                            vm.userGridOptions.data = addRoleNamesField(result.data.items);
                        }).finally(function () {
                            vm.loading = false;
                        });
                };

                function addRoleNamesField(users) {
                    for (var i = 0; i < users.length; i++) {
                        var user = users[i];
                        user.getRoleNames = function () {
                            var roleNames = '';
                            for (var j = 0; j < this.roles.length; j++) {
                                if (roleNames.length) {
                                    roleNames = roleNames + ', ';
                                }
                                roleNames = roleNames + this.roles[j].roleName;
                            };

                            return roleNames;
                        }
                    }

                    return users;
                }

                vm.editUser = function (user) {
                    openCreateOrEditUserModal(user.id);
                };

                vm.createUser = function () {
                    openCreateOrEditUserModal(null);
                };

                vm.deleteUser = function (user) {
                    if (user.userName == app.consts.userManagement.defaultAdminUserName) {
                        abp.message.warn('不能删除系统用户');
                        return;
                    }

                    abp.message.confirm(
                        '确认删除用户：' + user.userName + '?',
                        function (isConfirmed) {
                            if (isConfirmed) {
                                userService.deleteUser({
                                    id: user.id
                                }).then(function () {
                                    vm.getUsers();
                                    abp.notify.success('删除成功');
                                });
                            }
                        }
                    );
                };

                vm.unlockUser = function (user) {
                    userService.unlockUser({
                        id: user.id
                    })
                        .then(function () {
                            abp.notify.success('操作成功');
                        });
                };

                function openCreateOrEditUserModal(userId) {
                    var modalInstance = $uibModal.open({
                        templateUrl: '~/App/main/views/administration/users/createOrEditModal.cshtml',
                        controller: 'main.views.administration.users.createOrEditModal as vm',
                        backdrop: 'static',
                        resolve: {
                            userId: function () {
                                return userId;
                            }
                        }
                    });

                    modalInstance.result.then(function (result) {
                        vm.getUsers();
                    });
                }

                vm.editPermissions = function (user) {
                    $uibModal.open({
                        templateUrl: '~/App/main/views/administration/users/permissionsModal.cshtml',
                        controller: 'main.views.administration.users.permissionsModal as vm',
                        backdrop: 'static',
                        resolve: {
                            user: function () {
                                return user;
                            }
                        }
                    });
                };

                vm.getUsers();
            }
        ]);
})();