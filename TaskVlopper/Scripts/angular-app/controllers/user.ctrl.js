/// <reference path="services/user.service.js" />

app.controller('UserController', function ($scope, $state, $stateParams, UserService) {

    $scope.userHandler = {};

    $scope.userHandler.manageInit = function () {
        UserService.getManage().then(function (response) {
            $scope.model = response;
        });
    };

    $scope.userHandler.changePassword = function () {
        UserService.changePassword($scope.model).then(function (response) {
            // Passed
        });
    };

});