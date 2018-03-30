(function () {
    appModule.controller('main.views.dictionaries.diseases.index',
        [
            '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.disease',
            function ($scope, $uibModal, $stateParams, uiGridConstants, diseaseService) {
                var vm = this;
                $scope.$on('$viewContentLoaded',
                    function () {
                        mLayout.initAjax();
                    });

                vm.loading = false;
                vm.rowHeight = 50;
                vm.permissions = {
                    create: abp.auth.hasPermission('Pages.Dictionaries.Diseases.Create'),
                    edit: abp.auth.hasPermission('Pages.Dictionaries.Diseases.Edit'),
                    'delete': abp.auth.hasPermission('Pages.Dictionaries.Diseases.Delete')
                };

                vm.requestParams = {
                    department: $stateParams.departmentId === '' ? parseInt(0) : parseInt($stateParams.departmentId),
                    keyword: '',
                    skipCount: 0,
                    maxResultCount: app.consts.grid.defaultPageSize,
                    sorting: 'creationTime asc'
                };

                vm.diseaseGridOptions = {
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
                            enableSorting: false,
                            minWidth: 160
                        },
                        {
                            name: '科室',
                            enableColumnMenu: false,
                            field: 'department.displayName',
                            enableSorting: false,
                            minWidth: 160
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
                            name: '描述',
                            enableColumnMenu: false,
                            field: 'symptom'
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
                            '      <li><a href="javascript:;" ng-if="grid.appScope.permissions.edit" ng-click="grid.appScope.editDisease(row.entity)">修改</a></li>' +
                            '      <li><a href="javascript:;" ng-if="grid.appScope.permissions.delete" ng-click="grid.appScope.deleteDisease(row.entity)">删除</a></li>' +
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

                            vm.getDiseases();
                        });
                        gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                            vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                            vm.requestParams.maxResultCount = pageSize;

                            vm.getDiseases();
                        });
                    },
                    data: []
                };

                if (!vm.permissions.edit && !vm.permissions.delete) {
                    vm.diseaseGridOptions.columnDefs.pop();
                }

                vm.getTableHeight = function () {
                    return {
                        height: (vm.diseaseGridOptions.data.length * vm.rowHeight + 100) + "px"
                    };
                };

                vm.getDiseases = function () {
                    vm.loading = true;
                    var params = $.extend({ filter: vm.filterText }, vm.requestParams);
                    params.department = params.department === 0 ? '' : params.department;
                    diseaseService.getDiseases(params)
                        .then(function (result) {
                            vm.diseaseGridOptions.totalItems = result.data.totalCount;
                            vm.diseaseGridOptions.data = result.data.items;
                        }).finally(function () {
                            vm.loading = false;
                        });
                };

                vm.editDisease = function (disease) {
                    openCreateOrEditDiseaseModal(disease.id);
                };

                vm.createDisease = function () {
                    openCreateOrEditDiseaseModal(null);
                };

                vm.deleteDisease = function (disease) {
                    abp.message.confirm(
                        '确认删除病症：' + disease.displayName + '?',
                        function (isConfirmed) {
                            if (isConfirmed) {
                                diseaseService.deleteDisease({
                                    id: disease.id
                                }).then(function () {
                                    vm.getDiseases();
                                    abp.notify.success('删除成功');
                                });
                            }
                        }
                    );
                };

                function openCreateOrEditDiseaseModal(diseaseId) {
                    var modalInstance = $uibModal.open({
                        templateUrl: '~/App/main/views/dictionaries/diseases/createOrEditModal.cshtml',
                        controller: 'main.views.dictionaries.diseases.createOrEditModal as vm',
                        backdrop: 'static',
                        resolve: {
                            diseaseId: function () {
                                return diseaseId;
                            },
                            departmentId: function () {
                                return vm.requestParams.department;
                            }
                        }
                    });

                    modalInstance.result.then(function (result) {
                        vm.getDiseases();
                    });
                }

                vm.getDiseases();
            }
        ]);
})();