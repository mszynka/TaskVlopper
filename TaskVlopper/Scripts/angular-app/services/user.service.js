app.service('UserService', ['$http', function ($http) {

    this.getManage = function () {
        return $http.get('/Manage')
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response.data;
        })
        .catch(function (error) {
            $scope.status = '[UserService.getManage] Unable to load data: ' + error.data.message;
            console.log($scope.status);
        });
    };

    this.changePassword = function (model) {
        var newmodel = JSON.stringify(model);
        return $http({
            method: 'POST',
            url: '/Manage/ChangePassword',
            accept: 'application/json',
            data: newmodel
        })
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response;
        })
        .catch(function (error) {
            $scope.status = '[UserService.changePassword] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    };

    this.getAllUsers = function () {
        return $http.get('/Account/Users')
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response.data.Users;
        })
        .catch(function (error) {
            $scope.status = '[UserService.getAllUsers] Unable to load data: ' + error.data.message;
            console.log($scope.status);
        });
    };

    this.getCurrentUser = function () {
        return $http.get('/Account/CurrentUser')
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response.data;
        })
        .catch(function (error) {
            $scope.status = '[UserService.getAllUsers] Unable to load data: ' + error.data.message;
            console.log($scope.status);
        });
    };

    this.getAllUsersWithSelectors = function () {
        return $http.get('/Account/Users')
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return $http.get('/Account/CurrentUser').then(function (currentUser) {
                angular.forEach(response.data.Users, function (user) {
                    if (user.Email == currentUser.data) {
                        user.isSelectable = false;
                        user.isSelected = true;
                        user.isCurrentUser = true;
                    }
                    else {
                        user.isSelectable = true;
                    }
                    user.isOwner = false;
                    user.isDirty = false;
                })
                return response;
            })
        })
        .then(function (response) {
            return response.data.Users;
        })
        .catch(function (error) {
            $scope.status = '[UserService.getAllUsersWithSelectors] Unable to load data: ' + error.data.message;
            console.log($scope.status);
        });
    };

}]);