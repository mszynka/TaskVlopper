app.controller('ProjectController', function ($scope, $stateParams, ProjectService) {

    $scope.currentProjectId = $stateParams.projectId;
    $scope.projectHandler = {};
    $scope.projectHandler.getProjects = function () {
        ProjectService.getAll()
            .success(function (response) {
                if (response.HttpCode != undefined) {
                    console.log(response.HttpCode + " " + response.Message);
                }
                $scope.projects = response.Projects;
            })
        .error(function (error) {
            $scope.status = '[ProjectService.getAll] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }
    $scope.projectHandler.getProjects();

    $scope.projectHandler.getProject = function (projectId) {
        ProjectService.get(projectId)
        .success(function (response) {
            if (response.HttpCode != undefined) {
                console.log(response.HttpCode + " " + response.Message);
            }
            $scope.model = response.Project;
        })
        .error(function (error) {
            $scope.status = '[ProjectService.get] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    };

    $scope.projectHandler.createProject = function () {
        ProjectService.create($scope.model)
        .success(function (response) {
            if (response.HttpCode != undefined) {
                console.log(response.HttpCode + " " + response.Message);
            }
            $scope.projectHandler.getProjects();
        })
        .error(function (error) {
            $scope.status = '[ProjecService.create] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    };

    $scope.projectHandler.initEditor = function () {
        $scope.projectHandler.getProject($scope.currentProjectId);
    }

    $scope.projectHandler.editProject = function () {
        var temp_model = $scope.model;
        delete temp_model['$$hashkey'];
        console.log(temp_model);
        ProjectService.update(temp_model)
        .success(function (response) {
            if (response.HttpCode != undefined) {
                console.log(response.HttpCode + " " + response.Message);
            }
            $scope.projectHandler.getProjects();
        })
        .error(function (error) {
            $scope.status = '[ProjectService.deleteProject] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    };

    $scope.projectHandler.deleteProject = function () {
        ProjectService.delete($scope.currentProjectId)
        .success(function (response) {
            if (response.HttpCode != undefined) {
                console.log(response.HttpCode + " " + response.Message);
            }
            $scope.model = null;
            $scope.projectHandler.getProjects();
        })
        .error(function (error) {
            $scope.status = '[ProjectService.deleteProject] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }
});

app
    .directive('tvProjectViewer', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/project/viewer.html"
        }
    })
    .directive('tvProjectModeleditor', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/project/modeleditor.html"
        }
    });