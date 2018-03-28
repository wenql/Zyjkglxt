(function () {
    appModule.controller('main.views.dictionaries.medicines.index',
        [
            '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.medicine',
            function ($scope, $uibModal, $stateParams, uiGridConstants, medicineService) {
                var vm = this;
                $scope.$on('$viewContentLoaded',
                    function () {
                        mLayout.initAjax();
                    });

                vm.loading = false;
                vm.rowHeight = 50;
                vm.permissions = {
                    create: abp.auth.hasPermission('Pages.Dictionaries.Medicines.Create'),
                    edit: abp.auth.hasPermission('Pages.Dictionaries.Medicines.Edit'),
                    'delete': abp.auth.hasPermission('Pages.Dictionaries.Medicines.Delete')
                };

                vm.requestParams = {
                    keyword: '',
                    skipCount: 0,
                    maxResultCount: app.consts.grid.defaultPageSize,
                    sorting: 'displayName asc'
                };

                vm.medicineGridOptions = {
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
                            name: '名称',
                            enableColumnMenu: false,
                            field: 'displayName',
                            minWidth: 120
                        },
                        {
                            name: '拼音',
                            enableColumnMenu: false,
                            field: 'pinyin',
                            minWidth: 160
                        },
                        {
                            name: '来源',
                            enableColumnMenu: false,
                            field: 'source'
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
                            '      <li><a href="javascript:;" ng-if="grid.appScope.permissions.edit" ng-click="grid.appScope.editMedicine(row.entity)">修改</a></li>' +
                            '      <li><a href="javascript:;" ng-if="grid.appScope.permissions.delete" ng-click="grid.appScope.deleteMedicine(row.entity)">删除</a></li>' +
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

                            vm.getMedicines();
                        });
                        gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                            vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                            vm.requestParams.maxResultCount = pageSize;

                            vm.getMedicines();
                        });
                    },
                    data: []
                };

                if (!vm.permissions.edit && !vm.permissions.delete) {
                    vm.medicineGridOptions.columnDefs.pop();
                }

                vm.getTableHeight = function () {
                    return {
                        height: (vm.medicineGridOptions.data.length * vm.rowHeight + 100) + "px"
                    };
                };

                vm.getMedicines = function () {
                    vm.loading = true;
                    var params = $.extend({ filter: vm.filterText }, vm.requestParams);
                    params.department = params.department === 0 ? '' : params.department;
                    medicineService.getMedicines(params)
                        .then(function (result) {
                            vm.medicineGridOptions.totalItems = result.data.totalCount;
                            vm.medicineGridOptions.data = result.data.items;
                        }).finally(function () {
                            vm.loading = false;
                        });
                };

                vm.editMedicine = function (medicine) {
                    openCreateOrEditMedicineModal(medicine.id);
                };

                vm.createMedicine = function () {
                    openCreateOrEditMedicineModal(null);
                };

                vm.deleteMedicine = function (medicine) {
                    abp.message.confirm(
                        '确认删除药品：' + medicine.displayName + '?',
                        function (isConfirmed) {
                            if (isConfirmed) {
                                medicineService.deleteMedicine({
                                    id: medicine.id
                                }).then(function () {
                                    vm.getMedicines();
                                    abp.notify.success('删除成功');
                                });
                            }
                        }
                    );
                };

                function openCreateOrEditMedicineModal(medicineId) {
                    var modalInstance = $uibModal.open({
                        templateUrl: '~/App/main/views/dictionaries/medicines/createOrEditModal.cshtml',
                        controller: 'main.views.dictionaries.medicines.createOrEditModal as vm',
                        backdrop: 'static',
                        resolve: {
                            medicineId: function () {
                                return medicineId;
                            }
                        }
                    });

                    modalInstance.result.then(function (result) {
                        vm.getMedicines();
                    });
                }

                vm.getMedicines();
            }
        ]);
})();