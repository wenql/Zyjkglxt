(function () {
    appModule.controller('main.views.dictionaries.ranks.index', [
        '$scope', '$uibModal', '$templateCache', 'abp.services.app.rank',
        function ($scope, $uibModal, $templateCache, rankService) {
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
                    //vm.rankGridOptions.data = result.data.items;
                    $scope.data = result.data.items;
                }).finally(function () {
                    vm.loading = false;
                });
            };

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