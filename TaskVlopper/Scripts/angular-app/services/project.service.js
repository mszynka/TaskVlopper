angular.module('taskVlopperApp').factory('ProjectService', ['$http', function ($http) {
    
    var ProjectService = {};
    
    ProjectService.getProjects = function() {
        return $http.get('/Project');
    };

    ProjectService.createProject = function (model) {
        model = JSON.stringify(model);
        return $http({
            method: 'POST',
            url: '/Project/Create',
            accept: 'application/json',
            data: model
        });
    };

    ProjectService.deleteProject = function (projectId) {
        if (projectId !== undefined || !isNaN(projectId))
            return $http.post('/Project/Delete/' + projectId)
        else
            throw new Error("[ProjectService.deleteProject] ProjectID is invalid!");
    };

    return ProjectService;

}]);