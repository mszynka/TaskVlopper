/// <reference path="services/task.service.js" />

app.controller('TaskController', function ($scope, $state, $stateParams, TaskService) {
    
    Pace.on("done",
        function () {
            $('#modelEndDate').parent().datetimepicker({
                format: "MM/DD/YYYY",
                useCurrent: false
            });
            $("#modelStartDate").parent().on("dp.change", function (e) {
                $('#modelEndDate').parent().data("DateTimePicker").minDate(e.date);
            });
            $("#modelEndDate").parent().on("dp.change", function (e) {
                $('#modelStartDate').parent().data("DateTimePicker").maxDate(e.date);
            });
        });

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
        })
    };

    $scope.taskHandler.createTask = function () {
        TaskService.create($scope.model, $scope.currentProjectId).then(function (response) {
            $state.go('task/list', { projectId: $scope.currentProjectId });
        })
    };

    $scope.taskHandler.initEditor = function () {
        $scope.taskHandler.getTask($scope.currentTaskId);
    };

    $scope.taskHandler.editTask = function () {
        var temp_model = $scope.model;
        delete temp_model['$$hashkey'];
        TaskService.update(temp_model, $scope.currentProjectId).then(function (response) {
            $state.go('task/list', { projectId: $scope.currentProjectId });
        });
    };

    $scope.taskHandler.deleteTask= function () {
        TaskService.delete($scope.currentTaskId, $scope.currentProjectId).then(function (response) {
            $scope.model = null;
            $state.go('task/list', { projectId: $scope.currentProjectId });
        });
    }
});