app.service('WorklogService', ['$http', function ($http) {

    this.getAll = function (projectId, taskId) {
        return $http.get('/Worklog?projectId=' + projectId + '&taskId=' + taskId)
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response.data.Worklog;
        })
        .catch(function (error) {
            console.log('[WorklogService.getAll] Unable to load data: ' + error.data.message);
        });
    };

    this.get = function (worklogId, projectId, taskId) {
        if (worklogId === undefined || isNaN(worklogId)) {
            console.log("[WorklogService.get] worklogId is invalid!");
            return null;
        }
        if (taskId === undefined || isNaN(taskId)) {
            console.log("[WorklogService.get] TaskID is invalid!");
            return null;
        }
        if (projectId === undefined || isNaN(projectId)) {
            console.log("[WorklogService.get] ProjectID is invalid!");
            return null;
        }
        return $http.get('/Worklog/Details/' + worklogId + "?projectId=" + projectId + '&taskId=' + taskId)
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response.data.Worklog;
        })
        .catch(function (error) {
            console.log('[WorklogService.get] Unable to load data: ' + error.message);
        });
    };

    this.create = function (model, projectId, taskId) {
        var newmodel = JSON.stringify(model);
        return $http({
            method: 'POST',
            url: '/Worklog/Create?projectId=' + projectId + '&taskId=' + taskId,
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
            $scope.status = '[WorklogService.create] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    };

    this.update = function (model, projectId, taskId) {
        return $http({
            method: 'POST',
            url: '/Worklog/Edit/' + model.ID + "?projectId=" + projectId + '&taskId=' + taskId,
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
            $scope.status = '[WorklogService.update] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }

    this.delete = function (worklogId, projectId, taskId) {
        if (worklogId === undefined || isNaN(worklogId)) {
            console.log("[WorklogService.delete] worklogId is invalid!");
            return null;
        }
        if (taskId === undefined || isNaN(taskId)) {
            console.log("[WorklogService.delete] TaskID is invalid!");
            return null;
        }
        if (projectId === undefined || isNaN(projectId)) {
            console.log("[WorklogService.delete] ProjectID is invalid!");
            return null;
        }
        return $http.post('/Worklog/Delete/' + worklogId + "?projectId=" + projectId + '&taskId=' + taskId)
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response;
        })
        .catch(function (error) {
            $scope.status = '[WorklogService.delete] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    };
}]);