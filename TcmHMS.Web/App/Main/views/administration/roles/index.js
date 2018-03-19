(function () {
    appModule.controller('main.views.administration.roles.index', [
        '$scope', '$uibModal', '$templateCache', 'abp.services.app.role', 'uiGridConstants',
        function ($scope, $uibModal, $templateCache, roleService, uiGridConstants) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                mLayout.initAjax();
            });

            vm.loading = false;
            vm.requestParams = {
                permission: ''
            };

            vm.roleGridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
                appScopeProvider: vm,
                showGridFooter: true,

                columnDefs: [
                    {
                        name: '操作',
                        enableSorting: false,
                        enableColumnMenu: false,
                        width: 120,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <div class="btn-group dropdown" uib-dropdown="" dropdown-append-to-body>' +
                        '    <button class="btn btn-xs btn-primary blue" uib-dropdown-toggle="" aria-haspopup="true" aria-expanded="false"><i class="fa fa-cog"></i>操作<span class="caret"></span></button>' +
                        '    <ul uib-dropdown-menu>' +
                        '      <li><a ng-if="grid.appScope.permissions.edit" ng-click="grid.appScope.editRole(row.entity)">编辑</a></li>' +
                        '      <li><a ng-if="!row.entity.isStatic && grid.appScope.permissions.delete" ng-click="grid.appScope.deleteRole(row.entity)">删除</a></li>' +
                        '    </ul>' +
                        '  </div>' +
                        '</div>'
                    },
                    {
                        name: '名称',
                        enableColumnMenu: false,
                        field: 'displayName',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  {{COL_FIELD CUSTOM_FILTERS}} &nbsp;' +
                        '  <span ng-show="row.entity.isStatic" class="label label-info" uib-popover="1" popover-placement="bottom" popover-trigger="\'mouseenter click\'">系统</span>&nbsp;' +
                        '  <span ng-show="row.entity.isDefault" class="label label-default" uib-popover="2" popover-placement="bottom" popover-trigger="\'mouseenter click\'">默认</span>' +
                        '</div>'
                    },
                    {
                        name: '创建日期',
                        enableColumnMenu: false,
                        field: 'creationTime',
                        cellFilter: 'momentFormat: \'YYYY-MM-DD HH:mm:ss\''
                    }
                ],
                data: []
            };
            vm.getRoles = function () {
                vm.loading = true;
                roleService.getRoles(vm.requestParams).then(function (result) {
                    vm.roleGridOptions.data = result.data.items;
                }).finally(function () {
                    vm.loading = false;
                });
            };
            vm.getRoles();
        }
    ]);
})();