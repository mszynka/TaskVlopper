app.controller('ProjectController', function ($scope, $stateParams, ProjectService) {

    $scope.currentProjectId = $stateParams.projectId;
    $scope.projectHandler = {};
    $scope.projectHandler.getProjects = function () {
        ProjectService.getAll().then(function (response) {
            $scope.projects = response;
        })
    }
    $scope.projectHandler.getProjects();

    $scope.projectHandler.getProject = function (projectId) {
        ProjectService.get(projectId).then(function (response) {
            $scope.model = response;
        })
    };

    $scope.projectHandler.createProject = function () {
        ProjectService.create($scope.model).then(function(response){
            $scope.projectHandler.getProjects();
        })
    };

    $scope.projectHandler.initEditor = function () {
        $scope.projectHandler.getProject($scope.currentProjectId);
    }

    $scope.projectHandler.editProject = function () {
        var temp_model = $scope.model;
        delete temp_model['$$hashkey'];
        ProjectService.update(temp_model).then(function(response){
            $scope.projectHandler.getProjects();
        });
    };

    $scope.projectHandler.deleteProject = function () {
        ProjectService.delete($scope.currentProjectId).then(function (response) {
            $scope.model = null;
            $scope.projectHandler.getProjects();
        });
    }
});