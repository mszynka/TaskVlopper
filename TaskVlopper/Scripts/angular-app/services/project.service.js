angular.module('TaskVlopperApp').factory('ProjectService', ['$http', function ($http) {
    
    var ProjectService = {};
    
    ProjectService.getProjects = function() {
        return $http.get('/Project');
    };

    return ProjectService;

}]);