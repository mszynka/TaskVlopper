app.controller('DashboardController', function ($scope) {

    //// Datepicker section
    //function datepickers() {
    //$scope.today = function () {
    //    $scope.StartDate = new Date();
    //};
    //$scope.today();
    //$scope.status = {
    //    opened: false
    //};
    //$scope.open = function ($event) {
    //    $scope.status.opened = true;
    //};
    //var tomorrow = new Date();
    //tomorrow.setDate(tomorrow.getDate() + 1);
    //var afterTomorrow = new Date();
    //afterTomorrow.setDate(tomorrow.getDate() + 2);
    //$scope.events =
    //[
    //    {
    //        date: tomorrow,
    //        status: 'full'
    //    },
    //    {
    //        date: afterTomorrow,
    //        status: 'partially'
    //    }
    //];
    //}

    $scope.$on('$stateChangeStart',
        function (event, toState, toParams, fromState, fromParams) {
            Pace.restart();
        });
});