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
            $state.go('task/list?projectId=' + $scope.currentProjectId);
        });
    };

    $scope.taskHandler.deleteTask= function () {
        TaskService.delete($scope.currenctTaskId, $scope.currentProjectId).then(function (response) {
            $scope.model = null;
            $state.go('task/list?projectId=' + $scope.currentProjectId);
        });
    }
});