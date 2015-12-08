angular.module('taskVlopperApp').factory('ProjectService', ['$http', function ($http) {
    
    var ProjectService = {};
    
    ProjectService.getProjects = function() {
        return $http.get('/Project');
    };

    ProjectService.deleteProject = function (projectId) {
        if (projectId !== undefined || !isNaN(projectId))
            return $http.post('/Project/Delete/' + projectId)
        else
            throw new Error("[ProjectService.deleteProject] ProjectID is invalid!");
    };

    return ProjectService;

}]);