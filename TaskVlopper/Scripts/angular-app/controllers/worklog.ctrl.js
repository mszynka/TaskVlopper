/// <reference path="services/worklog.service.js" />

app.controller('WorklogController', function ($scope, $state, $stateParams, WorklogService) {

    $scope.currentTaskId = $stateParams.taskId;
    $scope.currentProjectId = $stateParams.projectId;
    $scope.currentWorklogId = $stateParams.worklogId;
    $scope.worklog = [];

    $scope.worklogHandler = {};
    $scope.worklogHandler.getWorklogs = function () {
        WorklogService.getAll($scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $scope.worklogs = response;
        })
    };
    $scope.worklogHandler.getWorklogs();

    $scope.worklogHandler.getWorklog = function (worklogId) {
        WorklogService.get(worklogId, $scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $scope.model = response;
            $scope.model.Date = new Date(parseInt($scope.model.Date.split("(")[1]));
        })
    };

    $scope.worklogHandler.createWorklog = function () {
        WorklogService.create($scope.model, $scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $state.go('worklog/list', { projectId: $scope.currentProjectId, taskId: $scope.currentTaskId });
        })
    };

    $scope.worklogHandler.initEditor = function () {
        $scope.worklogHandler.getWorklog($scope.currentWorklogId);
    };

    $scope.worklogHandler.editWorklog = function () {
        var temp_model = $scope.model;
        delete temp_model['$$hashkey'];
        WorklogService.update(temp_model, $scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $state.go('worklog/list', { projectId: $scope.currentProjectId, taskId: $scope.currentTaskId });
        });
    };

    $scope.worklogHandler.deleteWorklog = function () {
        WorklogService.delete($scope.currentWorklogId, $scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $scope.model = null;
            $state.go('worklog/list', { projectId: $scope.currentProjectId, taskId: $scope.currentTaskId });
        });
    }
});