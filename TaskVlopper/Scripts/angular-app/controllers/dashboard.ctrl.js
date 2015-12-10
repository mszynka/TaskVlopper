angular.module('taskVlopperApp').controller('DashboardController', function ($scope, ProjectService, TaskService) {

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

    $scope.projectMode = true;
    $scope.taskMode = false;
    $scope.currentProject = [];

    $scope.handlers = {};
    // Project handlers
    $scope.handlers.getProjects = function () {
        ProjectService.getAll()
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
            $scope.status = '[ProjectService.getAll] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }
    $scope.handlers.getProjects();

    $scope.handlers.getProject = function (projectId) {
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

    $scope.handlers.createProject = function () {
        ProjectService.create($scope.model)
        .success(function (response) {
            if (response.HttpCode != undefined) {
                console.log(response.HttpCode + " " + response.Message);
            }
            console.log(response);
            $scope.handlers.getProjects();
        })
        .error(function (error) {
            $scope.status = '[ProjecService.create] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    };

    $scope.handlers.editProject = function (project) {
        $scope.model = project;

        $scope.handlers.openModelEditor();
    };

    $scope.handlers.deleteProject = function (projectId) {
        ProjectService.delete(projectId)
        .success(function (response) {
            if (response.HttpCode != undefined) {
                console.log(response.HttpCode + " " + response.Message);
            }
            $scope.model = null;
            $scope.handlers.getProjects();
        })
        .error(function (error) {
            $scope.status = '[ProjectService.deleteProject] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    }

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
    }

    $scope.handlers.action.taskMode = function (project) {
        $scope.taskMode = true;
        $scope.projectMode = false;
        $scope.currentProject = project;
        TaskService.getAll(project.ID)
        .success(function (response) {
            if (response.HttpCode != undefined) {
                console.log(response.HttpCode + " " + response.Message);
            }
            $scope.tasks = response.Tasks;
        })
        .error(function (error) {
            $scope.status = '[TaskService.getAll] Unable to load data: ' + error.message;
            console.log($scope.status);
            $scope.tasks = [];
        });
    }

    $scope.handlers.action.projectMode = function() {
        $scope.projectMode = true;
        $scope.taskMode = false;
        $scope.currentProject = null;
    }
});