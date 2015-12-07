angular.module('TaskVlopperApp').controller('DashboardController', function ($scope, ProjectService) {
    
    getProjects();
    function getProjects() {
        ProjectService.getProjects().success(function (response) {
            $scope.projects = response.Projects;
            console.log($scope.projects);
        })
        .error(function (error) {
            $scope.status = 'Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }

    $scope.hasProjects = function() {
        return $scope.projects.length > 0 ? false : true;
    }

});