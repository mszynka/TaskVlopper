angular.module('taskVlopperApp').controller('DashboardController', function ($scope, ProjectService) {
    
    getProjects();
    function getProjects() {
        ProjectService.getProjects().success(function (response) {
            $scope.projects = response.Projects;
        })
        .error(function (error) {
            $scope.status = 'Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }

    $scope.handleProjectDelete = function(projectId){
        ProjectService.deleteProject(projectId).success(function (response) {
            console.log(response.StatusDescription);
            getProjects();
        })
        .error(function (error) {
            $scope.status = 'Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }

});