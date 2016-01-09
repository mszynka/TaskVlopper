app.service('MeetingService', ['$http', function ($http) {

    this.getAllForCurrentUser = function () {
        return $http.get('/Meeting')
            .then(function (response) {
                if (response.data.HttpCode != undefined) {
                    console.log(response.data.HttpCode + " " + response.data.Message);
                }
                return response.data.Meeting;
            })
            .catch(function (error) {
                console.log('[MeetingService.getAllForCurrentUser] Unable to load data: ' + error.data.message);
            });
    };

    this.getAll = function (projectId, taskId) {
        var taskData = "";
        if (taskId != undefined && taskId != null) {
            taskData = '&taskId=' + taskId;
        }

        return $http.get('/Meeting?projectId=' + projectId + taskData)
            .then(function (response) {
                if (response.data.HttpCode != undefined) {
                    console.log(response.data.HttpCode + " " + response.data.Message);
                }
                return response.data.Meeting;
            })
            .catch(function (error) {
                console.log('[MeetingService.getAll] Unable to load data: ' + error.data.message);
            });
    };

    this.get = function (meetingId, projectId, taskId) {
        if (meetingId === undefined || isNaN(meetingId)) {
            console.log("[MeetingService.get] MeetingID is invalid!");
            return null;
        }
        if (projectId === undefined || isNaN(projectId)) {
            console.log("[MeetingService.get] ProjectID is invalid!");
            return null;
        }
        var taskData = "";
        if (taskId != undefined && taskId != null) {
            taskData = '&taskId=' + taskId;
        }

        return $http.get('/Meeting/Details/' + meetingId + "?projectId=" + projectId + taskData)
            .then(function (response) {
                if (response.data.HttpCode != undefined) {
                    console.log(response.data.HttpCode + " " + response.data.Message);
                }
                return response.data.Meeting;
            })
            .catch(function (error) {
                console.log('[MeetingService.get] Unable to load data: ' + error.message);
            });
    };

    this.create = function (model, projectId, taskId) {
        var newmodel = JSON.stringify(model);
        var taskData = "";
        if (taskId != undefined && taskId != null) {
            taskData = '&taskId=' + taskId;
        }

        return $http({
            method: 'POST',
            url: '/Meeting/Create?projectId=' + projectId + taskData,
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
                $scope.status = '[MeetingService.create] Unable to load data: ' + error.message;
                console.log($scope.status);
            });
    };

    this.update = function (model, projectId, taskId) {
        var taskData = "";
        if (taskId != undefined && taskId != null) {
            taskData = '&taskId=' + taskId;
        }

        return $http({
            method: 'POST',
            url: '/Meeting/Edit/' + model.ID + "?projectId=" + projectId + taskData,
            accept: 'application/json',
            data: model
        })
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response;
        })
        .catch(function (error) {
            $scope.status = '[MeetingService.update] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    };

    this.delete = function (meetingId, projectId, taskId) {
        if (meetingId === undefined || isNaN(meetingId)) {
            console.log("[MeetingService.delete] MeetingID is invalid!");
            return null;
        }
        if (projectId === undefined || isNaN(projectId)) {
            console.log("[MeetingService.delete] ProjectID is invalid!");
            return null;
        }
        var taskData = "";
        if (taskId != undefined && taskId != null) {
            taskData = '&taskId=' + taskId;
        }

        return $http.post('/Meeting/Delete/' + meetingId + "?projectId=" + projectId + taskData)
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response;
        })
        .catch(function (error) {
            $scope.status = '[MeetingService.delete] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    };

    this.getUsers = function (meetingId) {
        if (meetingId !== undefined || !isNaN(meetingId)) {
            return $http.get('/Meeting/Users/' + meetingId)
            .then(function (response) {
                if (response.data.HttpCode != undefined) {
                    console.log(response.data.HttpCode + " " + response.data.Message);
                }
                return response.data;
            })
            .catch(function (error) {
                $scope.status = '[MeetingService.getUsers] Unable to load data: ' + error.message;
                console.log($scope.status);
            });
        }
        else {
            $scope.status = "[MeetingService.getUsers] MeetingID is invalid!";
            console.log($scope.status);
            return null;
        }
    }

    this.bindUser = function (meetingId, userId) {
        if (meetingId !== undefined || !isNaN(meetingId)) {
            return $http.post('/Meeting/Users/' + meetingId + "?userId=" + userId)
            .then(function (response) {
                if (response.data.HttpCode != undefined) {
                    console.log(response.data.HttpCode + " " + response.data.Message);
                }
                return response;
            })
            .catch(function (error) {
                console.log('[MeetingService.bindUser] Unable to load data: ' + error.message);
            });
        }
        else {
            console.log("[MeetingService.bindUser] MeetingID is invalid!");
            return null;
        }
    }

}]);