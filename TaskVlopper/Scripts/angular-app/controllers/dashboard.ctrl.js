angular.module('taskVlopperApp').controller('DashboardController', function ($scope, ProjectService) {

    // Datepicker section
    $scope.today = function () {
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

    $scope.status.modelEditorStatus = {
        create: false,
        edit: false
    };

    $scope.handlers = {};
    // Project handlers
    $scope.handlers.getProjects = function () {
        ProjectService.getProjects()
            .success(function (response) {
                if (response.HttpCode != undefined) {
                    console.log(response.HttpCode + " " + response.Message);
                }
                else if (response) {
                    console.log(response);
                }
                $scope.projects = response.Projects;
            })
        .error(function (error) {
            $scope.status = '[ProjectService.getProjects] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }
    $scope.handlers.getProjects();

    $scope.handlers.getProject = function (projectId) {
        ProjectService.getProject(projectId)
        .success(function (response) {
            if (response.HttpCode != undefined) {
                console.log(response.HttpCode + " " + response.Message);
            }
            $scope.model = response.Projects;
        })
        .error(function (error) {
            $scope.status = '[ProjectService.getProject] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    };

    $scope.handlers.createProject = function () {
        ProjectService.createProject($scope.model)
        .success(function (response) {
            if (response.HttpCode != undefined) {
                console.log(response.HttpCode + " " + response.Message);
            }
            console.log(response);
        })
        .error(function (error) {
            $scope.status = '[ProjecService.createProject] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    };

    $scope.handlers.editProject = function (project) {
        $scope.model = project;

        $scope.handlers.openModelEditor();
    };

    $scope.handlers.deleteProject = function (projectId) {
        ProjectService.deleteProject(projectId)
        .success(function (response) {
            if (response.HttpCode != undefined) {
                console.log(response.HttpCode + " " + response.Message);
            }
        })
        .error(function (error) {
            $scope.status = '[ProjectService.deleteProject] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }

    // Handlers
    $scope.handlers.openModelEditor = function () {
        $scope.openModelEditor = true;
    };

    $scope.handlers.closeModelEditor = function () {
        $scope.openModelEditor = false;
    };

    // Action handlers
    $scope.handlers.action = {};
    $scope.handlers.action.formSubmit = function () {
        if ($scope.status.modelEditorStatus.edit) {

        }
        else if ($scope.status.modelEditorStatus.create) {
            $scope.status.modelEditorStatus.create = false;
            $scope.handlers.createProject();
        }
        else {
            throw new Error("[handlers.action.formSubmit] Cannot specify ModelEditorStatus");
        }
        $scope.handlers.closeModelEditor();
        $scope.handlers.getProjects();
    };

    $scope.handlers.action.formCancel = function () {
        $scope.model = null;
        $scope.handlers.closeModelEditor();
    };

    $scope.handlers.action.addProject = function () {
        $scope.handlers.openModelEditor();
        $scope.model = null;
        $scope.status.modelEditorStatus.create = true;
    };

    $scope.handlers.action.editProject = function () {

    };

    $scope.handlers.action.deleteProject = function (projectId) {
        $scope.handlers.deleteProject(projectId);
        $scope.model = null;
        $scope.handlers.getProjects();
    }

});