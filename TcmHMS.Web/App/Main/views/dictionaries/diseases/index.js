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
                    create: abp.auth.hasPermission('Pages.Administration.Diseases.Create'),
                    edit: abp.auth.hasPermission('Pages.Administration.Diseases.Edit'),
                    'delete': abp.auth.hasPermission('Pages.Administration.Diseases.Delete'),
                };

                vm.requestParams = {
                    department: '',
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

                vm.getTableHeight = function () {
                    return {
                        height: (vm.diseaseGridOptions.data.length * vm.rowHeight + 100) + "px"
                    };
                };

                vm.getDiseases = function () {
                    vm.loading = true;
                    diseaseService.getDiseases($.extend({ filter: vm.filterText }, vm.requestParams))
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
                    if (disease.diseaseName == app.consts.diseaseManagement.defaultAdminDiseaseName) {
                        abp.message.warn('不能删除系统用户');
                        return;
                    }

                    abp.message.confirm(
                        '确认删除用户：' + disease.diseaseName + '?',
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
                        templateUrl: '~/App/main/views/administration/diseases/createOrEditModal.cshtml',
                        controller: 'main.views.administration.diseases.createOrEditModal as vm',
                        backdrop: 'static',
                        resolve: {
                            diseaseId: function () {
                                return diseaseId;
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