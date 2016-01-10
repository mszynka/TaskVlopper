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
            $http.get('/Account/CurrentUser').then(function (currentUser) {
                angular.forEach(response.data.Users, function (response) {
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
            })
            return response.data.Users;
            
        })
        .catch(function (error) {
            $scope.status = '[UserService.getAllUsersWithSelectors] Unable to load data: ' + error.data.message;
            console.log($scope.status);
        });
    };

}]);