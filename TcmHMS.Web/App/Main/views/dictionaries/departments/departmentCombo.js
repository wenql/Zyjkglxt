(function () {
    appModule.directive('departmentCombo', ['$timeout', 'abp.services.app.department',
        function ($timeout, departmentService) {
            return {
                restrict: 'E',
                replace: true,
                templateUrl: '/App/Main/views/dictionaries/departments/departmentCombo.cshtml',
                scope: {
                    selectedDepartment: '=?'
                },
                link: function ($scope, element, attrs) {
                    $scope.departments = [];
                    $scope.emptyText = attrs.emptyText || '';

                    departmentService.getDepartments({}).then(function (result) {
                        $scope.departments = result.data.items;
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