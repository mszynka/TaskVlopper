angular.module('taskVlopperApp').controller('DashboardController', function ($scope, ProjectService) {
    
    $scope.today = function() {
        $scope.StartDate = new Date();
    };
    $scope.today();

    $scope.status = {
        opened: false
    };

    $scope.open = function ($event) {
        $scope.status.opened = true;
    };

    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    var afterTomorrow = new Date();
    afterTomorrow.setDate(tomorrow.getDate() + 2);
    $scope.events =
    [
        {
            date: tomorrow,
            status: 'full'
        },
        {
            date: afterTomorrow,
            status: 'partially'
        }
    ];

    getProjects();
    function getProjects() {
        ProjectService.getProjects()
        .success(function (response) {
            $scope.projects = response.Projects;
        })
        .error(function (error) {
            $scope.status = '[DashboardController.getProjects] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }

    $scope.handleProjectCreate = function() {
        ProjectService.createProject($scope.model)
        .success(function (response) {
            console.log(response.StatusDescription);
            getProjects();
        })
        .error(function (error) {
            $scope.status = '[DashboardController.handleProjectCreate] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    };

    $scope.handleProjectDelete = function(projectId){
        ProjectService.deleteProject(projectId)
        .success(function (response) {
            console.log(response.StatusDescription);
            getProjects();
        })
        .error(function (error) {
            $scope.status = '[DashboardController.handleProjectDelete] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }

});