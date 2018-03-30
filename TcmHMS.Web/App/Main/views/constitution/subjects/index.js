(function () {
    appModule.controller('main.views.constitutions.subjects.index',
        [
            '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.constitution',
            function ($scope, $uibModal, $stateParams, uiGridConstants, constitutionService) {
                var vm = this;
                $scope.$on('$viewContentLoaded',
                    function () {
                        mLayout.initAjax();
                    });

                vm.loading = false;
                vm.rowHeight = 50;
                vm.permissions = {
                    create: abp.auth.hasPermission('Pages.Constitutions.Subjects.Create'),
                    edit: abp.auth.hasPermission('Pages.Constitutions.Subjects.Edit'),
                    'delete': abp.auth.hasPermission('Pages.Constitutions.Subjects.Delete')
                };

                vm.requestParams = {
                    group: $stateParams.groupId === '' ? parseInt(0) : parseInt($stateParams.groupId),
                    keyword: '',
                    skipCount: 0,
                    maxResultCount: app.consts.grid.defaultPageSize,
                    sorting: 'creationTime asc'
                };

                vm.subjectGridOptions = {
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
                            name: '标题',
                            enableColumnMenu: false,
                            field: 'title',
                            minWidth: 120
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
                            '      <li><a href="javascript:;" ng-if="grid.appScope.permissions.edit" ng-click="grid.appScope.editSubject(row.entity)">修改</a></li>' +
                            '      <li><a href="javascript:;" ng-if="grid.appScope.permissions.delete" ng-click="grid.appScope.deleteSubject(row.entity)">删除</a></li>' +
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

                            vm.getSubjects();
                        });
                        gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                            vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                            vm.requestParams.maxResultCount = pageSize;

                            vm.getSubjects();
                        });
                    },
                    data: []
                };

                if (!vm.permissions.edit && !vm.permissions.delete) {
                    vm.subjectGridOptions.columnDefs.pop();
                }

                vm.getTableHeight = function () {
                    return {
                        height: (vm.subjectGridOptions.data.length * vm.rowHeight + 100) + "px"
                    };
                };

                vm.getSubjects = function () {
                    vm.loading = true;
                    var params = $.extend({ filter: vm.filterText }, vm.requestParams);
                    params.group = params.group === 0 ? '' : params.group;
                    constitutionService.getConstitutionSubjects(params)
                        .then(function (result) {
                            vm.subjectGridOptions.totalItems = result.data.totalCount;
                            vm.subjectGridOptions.data = result.data.items;
                        }).finally(function () {
                            vm.loading = false;
                        });
                };

                vm.editSubject = function (subject) {
                    openCreateOrEditSubjectsModal(subject.id);
                };

                vm.createSubject = function () {
                    openCreateOrEditSubjectsModal(null);
                };

                vm.deleteSubject = function (subject) {
                    abp.message.confirm(
                        '确认删除问卷：' + subject.title + '?',
                        function (isConfirmed) {
                            if (isConfirmed) {
                                constitutionService.deleteConstitutionSubjects({
                                    id: subject.id
                                }).then(function () {
                                    vm.getSubjects();
                                    abp.notify.success('删除成功');
                                });
                            }
                        }
                    );
                };

                function openCreateOrEditSubjectsModal(subjectId) {
                    var modalInstance = $uibModal.open({
                        templateUrl: '~/App/main/views/constitution/subjects/createOrEditModal.cshtml',
                        controller: 'main.views.constitutions.subjects.createOrEditModal as vm',
                        backdrop: 'static',
                        resolve: {
                            subjectId: function () {
                                return subjectId;
                            },
                            groupId: function () {
                                return vm.requestParams.group;
                            }
                        }
                    });

                    modalInstance.result.then(function (result) {
                        vm.getSubjects();
                    });
                }

                vm.getSubjects();
            }
        ]);
})();