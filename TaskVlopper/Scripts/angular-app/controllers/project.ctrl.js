app.controller('ProjectController', function ($scope, ProjectService) {

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
            $scope.model = response.Projects;
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

    $scope.projectHandler.editProject = function (project) {
        var temp_model = project;
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

    $scope.projectHandler.deleteProject = function (projectId) {
        ProjectService.delete(projectId)
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

    $scope.$on('$stateChangeStart', 
        function(event, toState, toParams, fromState, fromParams){ 
            Pace.restart();
        });
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