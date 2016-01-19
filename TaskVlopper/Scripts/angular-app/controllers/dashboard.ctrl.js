app.controller('DashboardController', function ($scope, UserService) {

    // Datepicker section
    // Disable weekend selection
    $scope.disabled = function (date, mode) {
        return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
    };

    $scope.dashboardHandler = {}
    $scope.users = []

    $scope.open = function ($event, element) {
        $event.preventDefault();
        $event.stopPropagation();
        if (element == 1)
            $scope.status.opened1 = true;
        if (element == 2)
            $scope.status.opened2 = true;
    };

    $scope.dateOptions = {
        formatYear: 'yyyy',
        startingDay: 1
    };

    $scope.format = 'dd/MM/yyyy';

    $scope.status = {
        opened1: false,
        opened2: false
    };

    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    var afterTomorrow = new Date();
    afterTomorrow.setDate(tomorrow.getDate() + 2);
    $scope.events =
      [
        {
            date: tomorrow,
            status: 'full'
        },
        {
            date: afterTomorrow,
            status: 'partially'
        }
      ];

    $scope.getDayClass = function (date, mode) {
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                if (dayToCheck === currentDay) {
                    return $scope.events[i].status;
                }
            }
        }

        return '';
    };

    $scope.$on('$stateChangeStart',
        function (event, toState, toParams, fromState, fromParams) {
            if (toState != fromState && toParams != fromParams) {
                Pace.restart();
                $scope.users = null;
            }
        });

    $scope.dashboardHandler.getAllUsers = function () {
        return UserService.getCurrentUser().then(function (currentUser) {
            $scope.currentUser = currentUser;
            UserService.getAllUsers().then(function (response) {
                angular.forEach(response, function (response) {
                    if (response.Email == currentUser) {
                        response.isSelectable = false;
                        response.isSelected = true;
                        response.isCurrentUser = true;
                    }
                    else {
                        response.isSelectable = true;
                    }
                    response.isOwner = false;
                    response.isDirty = false;
                })
                $scope.users = response;
            });
        });
    };

    $scope.dashboardHandler.triggerUserClick = function (user) {
        user.isSelected = !user.isSelected;
        user.isDirty = true;
    };
});