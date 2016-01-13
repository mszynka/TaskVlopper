/// <reference path="services/task.service.js" />

app.controller('TaskController', function ($scope, $state, $stateParams, TaskService) {

    $scope.currentTaskId = $stateParams.taskId;
    $scope.currentProjectId = $stateParams.projectId;
    $scope.tasks = [];

    $scope.taskHandler = {};
    $scope.taskHandler.getTasks = function () {
        TaskService.getAll($scope.currentProjectId).then(function (response) {
            $scope.tasks = response;
        })
    };
    $scope.taskHandler.getTasks();

    $scope.taskHandler.getTask = function (taskId) {
        TaskService.get(taskId, $scope.currentProjectId).then(function (response) {
            $scope.model = response;
            if ($scope.model.StartDate != undefined)
                $scope.model.StartDate = new Date(parseInt($scope.model.StartDate.split("(")[1]));
            if ($scope.model.EndDate != undefined)
                $scope.model.EndDate = new Date(parseInt($scope.model.EndDate.split("(")[1]));
        })
    };

    $scope.taskHandler.createTask = function () {
        Pace.restart();
        TaskService.create($scope.model, $scope.currentProjectId).then(function (response) {
            $state.go('task/list', { projectId: $scope.currentProjectId });
        })
    };

    $scope.taskHandler.initEditor = function () {
        $scope.taskHandler.getTask($scope.currentTaskId);
    };

    $scope.taskHandler.editTask = function () {
        Pace.restart();
        var temp_model = $scope.model;
        delete temp_model['$$hashkey'];
        TaskService.update(temp_model, $scope.currentProjectId).then(function (response) {
            $state.go('task/list', { projectId: $scope.currentProjectId });
        });
    };

    $scope.taskHandler.deleteTask = function () {
        Pace.restart();
        TaskService.delete($scope.currentTaskId, $scope.currentProjectId).then(function (response) {
            $scope.model = null;
            $state.go('task/list', { projectId: $scope.currentProjectId });
        });
    }
});