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

    this.getProjectUsers = function (projectId) {
        return $http.get('/Project/Users/' + projectId)
        .then(function(response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response.data;
        })
        .catch(function(error){
            $scope.status = '[UserService.getProjectUsers] Unable to load data: ' + error.data.message;
            console.log($scope.status);
        })
    };

    this.assignProjectUser = function (projectId, userId) {
        return $http.get('/Project/Users/'+projectId + "?userId=" + userId)
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response;
        })
        .catch(function (error) {
            $scope.status = '[UserService.assignProjectUser] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }

    this.getMeetingUsers = function (meetingId) {
        return $http.get('/Meeting/Users/' + meetingId)
        .then(function(resposne) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response.data;
        })
        .catch(function(error){
            $scope.status = '[UserService.getMeetingUsers] Unable to load data: ' + error.data.message;
            console.log($scope.status);
        })
    };

    this.assignMeetingUser = function (meetingId, userId) {
        return $http.get('/Meeting/Users/'+meetingId + "?userId=" + userId)
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response;
        })
        .catch(function (error) {
            $scope.status = '[UserService.assignMeetingUser] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }

    this.getTaskUsers = function (taskId, projectId) {
        return $http.get('/Task/Users/' + taskId + "?projectId=" + projectId)
        .then(function(resposne) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response.data;
        })
        .catch(function(error){
            $scope.status = '[UserService.getTaskUsers] Unable to load data: ' + error.data.message;
            console.log($scope.status);
        })
    };

    this.assignTaskUser = function (taskId, projectId, userId) {
        return $http.get('/Task/Users/'+taskId + "?projectId=" + projectId + "&userId=" + userId)
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response;
        })
        .catch(function (error) {
            $scope.status = '[UserService.assignTaskUser] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }

}]);