/// <reference path="services/worklog.service.js" />

app.controller('WorklogController', function ($scope, $rootScope, $interval, $state, $stateParams, WorklogService) {

    $scope.currentTaskId = $stateParams.taskId;
    $scope.currentProjectId = $stateParams.projectId;
    $scope.currentWorklogId = $stateParams.worklogId;
    $rootScope.currentView = "worklog";
    $scope.worklog = [];

    $scope.worklogHandler = {};
    $scope.worklogHandler.getWorklogs = function () {
        WorklogService.getAll($scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            if ($scope.worklogs == undefined || !angular.equals($scope.worklogs, response))
                $scope.worklogs = response;
        })
    };

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

    if ($state.current.name == "worklog/list") {
        $scope.worklogHandler.getWorklogs();
        $scope.interval = $interval(function () { $('body').css('ng-scope pace-done'); $scope.worklogHandler.getWorklogs() }, 10000);
    }
    else if ($state.current.name == "worklog/edit") {
        $scope.worklogHandler.getWorklog($scope.currentWorklogId);
    }

    $scope.$on("$destroy", function () {
        if (angular.isDefined($scope.interval)) {
            $interval.cancel($scope.interval);
            $scope.interval = 0;
        }
    });

});