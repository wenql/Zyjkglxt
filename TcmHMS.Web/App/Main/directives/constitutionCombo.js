(function () {
    appModule.directive('constitutionCombo', ['$timeout', 'abp.services.app.constitution',
        function ($timeout, onstitutionService) {
            return {
                restrict: 'E',
                replace: true,
                templateUrl: '/App/Main/directives/constitutionCombo.cshtml',
                scope: {
                    selectedGroup: '=?'
                },
                link: function ($scope, element, attrs) {
                    $scope.groups = [];
                    $scope.emptyText = attrs.emptyText || '';

                    onstitutionService.getConstitutionGroups({}).then(function (result) {
                        if (attrs.emptyText)
                            result.data.items.unshift({ groupId: 0, groupName: $scope.emptyText });
                        $scope.groups = result.data.items;

                        //refresh combo
                        $timeout(function () {
                            $(element).selectpicker('refresh');
                        });
                    });
                }
            };
        }
    ]);
})();