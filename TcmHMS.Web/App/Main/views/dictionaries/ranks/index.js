(function () {
    appModule.controller('main.views.dictionaries.ranks.index', [
        '$scope', '$uibModal', '$templateCache', 'abp.services.app.rank', '$timeout',
        function ($scope, $uibModal, $templateCache, rankService, $timeout) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                mLayout.initAjax();
            });
            vm.rowHeight = 50;
            vm.loading = false;
            vm.permissions = {
                create: abp.auth.hasPermission('Pages.Dictionaries.Ranks.Create'),
                edit: abp.auth.hasPermission('Pages.Dictionaries.Ranks.Edit'),
                'delete': abp.auth.hasPermission('Pages.Dictionaries.Ranks.Delete')
            };
            vm.requestParams = {
                keyword: ''
            };

            vm.getRanks = function () {
                vm.loading = true;
                rankService.getRanks(vm.requestParams).then(function (result) {
                    $scope.data = result.data.items;
                }).finally(function () {
                    vm.loading = false;
                });
            };

            $scope.sortableOptions = {
                // 数据有变化
                update: function (e, ui) {
                    $timeout(function () {
                        var rankIds = [];
                        for (var i = 0; i < $scope.data.length; i++) {
                            rankIds.push($scope.data[i].id);
                        }
                        vm.loading = true;
                        rankService.updateSortable(rankIds).then(function (result) {
                            abp.notify.success('排序成功');
                        }).finally(function () {
                            vm.loading = false;
                        });
                    });
                },
                stop: function (e, ui) {
                }
            }

            vm.deleteRank = function (rank) {
                abp.message.confirm(
                    '确认删除职称：' + rank.displayName + '?',
                    function (isConfirmed) {
                        if (isConfirmed) {
                            rankService.deleteRank({
                                id: rank.id
                            }).then(function () {
                                vm.getRanks();
                                abp.notify.success('删除成功');
                            });
                        }
                    }
                );
            };

            vm.editRank = function (rank) {
                openCreateOrEditRankModal(rank.id);
            };

            vm.createRank = function () {
                openCreateOrEditRankModal(null);
            };

            function openCreateOrEditRankModal(rankId) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/main/views/dictionaries/ranks/createOrEditModal.cshtml',
                    controller: 'main.views.dictionaries.ranks.createOrEditModal as vm',
                    backdrop: 'static',
                    resolve: {
                        rankId: function () {
                            return rankId;
                        }
                    }
                });
                modalInstance.result.then(function (result) {
                    vm.getRanks();
                });
            }

            vm.getRanks();
        }
    ]);
})();