angular.module('taskVlopperApp').factory('ProjectService', ['$http', function ($http) {

    var ProjectService = {};

    ProjectService.getProjects = function () {
        return $http.get('/Project');
    };

    ProjectService.getProject = function (projectId) {
        if (projectId !== undefined || !isNaN(projectId)) {
            return $http.get('/Project/Details/' + projectId);
        }
        else {
            $scope.status = "[ProjectService.getProject] ProjectID is invalid!";
            console.log($scope.status);
        }
    };

    ProjectService.createProject = function (model) {
        // TODO: handle invalid model
        console.log(model);
        model = JSON.stringify(model);
        return $http({
            method: 'POST',
            url: '/Project/Create',
            accept: 'application/json',
            data: model
        })
    };

    ProjectService.updateProject = function (model) {
        // TODO: handle invalid model
        return $http({
            method: 'POST',
            url: '/Project/Edit/' + model.ID,
            accept: 'application/json',
            data: model
        });
    }

    ProjectService.deleteProject = function (projectId) {
        if (projectId !== undefined || !isNaN(projectId))
            return $http.post('/Project/Delete/' + projectId);
            
        else {
            $scope.status = "[ProjectService.deleteProject] ProjectID is invalid!";
            console.log($scope.status);
        }
    };

    return ProjectService;

}]);