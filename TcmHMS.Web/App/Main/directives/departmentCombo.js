(function () {
    appModule.directive('departmentCombo', ['$timeout', 'abp.services.app.department',
        function ($timeout, departmentService) {
            return {
                restrict: 'E',
                replace: true,
                templateUrl: '/App/Main/directives/departmentCombo.cshtml',
                scope: {
                    selectedDepartment: '=?'
                },
                link: function ($scope, element, attrs) {
                    $scope.departments = [];
                    $scope.emptyText = attrs.emptyText || '';

                    departmentService.getDepartments({}).then(function (result) {
                        if (attrs.emptyText)
                            result.data.items.unshift({ id: 0, displayName: $scope.emptyText });
                        $scope.departments = result.data.items;

                        //refresh combo
                        $timeout(function () {
                            //$(element).selectpicker('val', $scope.selectedDepartment);
                            $(element).selectpicker('refresh');
                        }, 500);
                    });
                }
            };
        }
    ]);
})();