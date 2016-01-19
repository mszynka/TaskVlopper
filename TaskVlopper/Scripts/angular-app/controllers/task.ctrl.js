/// <reference path="services/task.service.js" />

app.controller('TaskController', function ($scope, $rootScope, $interval, $state, $stateParams, TaskService, UserService) {

    $scope.currentTaskId = $stateParams.taskId;
    $scope.currentProjectId = $stateParams.projectId;

    $rootScope.currentProjectId = $stateParams.projectId;
    $rootScope.projectView = false;
    $rootScope.taskView = true;
    $rootScope.meetingView = false;

    $scope.tasks = [];
    $scope.taskStatus = [];

    $scope.taskHandler = {};
    $scope.taskHandler.getTasks = function () {
        TaskService.getAllWithStats($scope.currentProjectId).then(function (response) {
            if ($scope.tasks == undefined || !angular.equals($scope.tasks, response.Tasks))
                $scope.tasks = response.Tasks;
            $scope.taskStatus = response.Statuses;
            angular.forEach($scope.tasks, function (task) {
                task.Task.statusName = $scope.taskStatus[task.Task.Status];
            })
        })
    };

    $scope.taskHandler.getStatuses = function () {
        TaskService.getAllWithStats($scope.currentProjectId).then(function (response) {
            $scope.taskStatus = response.Statuses;
        })
    };
    
    $scope.taskHandler.getTask = function (taskId) {
        TaskService.get(taskId, $scope.currentProjectId).then(function (response) {
            $scope.model = response;
            if ($scope.model.StartDate != undefined)
                $scope.model.StartDate = new Date(parseInt($scope.model.StartDate.split("(")[1]));
            if ($scope.model.EndDate != undefined)
                $scope.model.EndDate = new Date(parseInt($scope.model.EndDate.split("(")[1]));
            if ($scope.model.Status == undefined)
                $scope.model.Status = 0;
        })
    };

    $scope.taskHandler.createTask = function () {
        $scope.taskHandler.bindUsersToTask();
        TaskService.create($scope.model, $scope.currentProjectId).then(function (response) {
            $state.go('task/list', { projectId: $scope.currentProjectId });
        })
    };

    $scope.taskHandler.editTask = function () {
        var temp_model = $scope.model;
        delete temp_model['$$hashkey'];
        $scope.taskHandler.bindUsersToTask();
        TaskService.update(temp_model, $scope.currentProjectId).then(function (response) {
            $state.go('task/list', { projectId: $scope.currentProjectId });
        });
    };

    $scope.taskHandler.deleteTask = function () {
        TaskService.delete($scope.currentTaskId, $scope.currentProjectId).then(function (response) {
            $scope.model = null;
            $state.go('task/list', { projectId: $scope.currentProjectId });
        });
    }

    $scope.taskHandler.getUsers = function (taskId) {
        UserService.getAllUsersWithSelectors().then(function (allUsers) {
            $scope.users = allUsers;
            TaskService.getUsers(taskId, $scope.currentProjectId).then(function (taskUsers) {
                angular.forEach(taskUsers.Users, function (taskUser) {
                    angular.forEach($scope.users, function (user) {
                        if (taskUser.Email === user.Email) {
                            user.isSelected = true;
                        }
                    });
                });
            })
        })
    };

    $scope.taskHandler.bindUsersToTask = function () {
        angular.forEach($scope.users, function (user) {
            if (user.isDirty && user.isSelectable) {
                if (user.isSelected)
                    TaskService.bindUser($scope.currentTaskId, $scope.currentProjectId, user.Email);
                else if (!user.isSelected)
                    TaskService.unbindUser($scope.currentTaskId, $scope.currentProjectId, user.Email);
            }
        })
    };

    if ($state.current.name == "task/list") {
        $scope.interval = $interval(function () { $('body').css('ng-scope pace-done'); $scope.taskHandler.getTasks() }, 10000);
        $scope.taskHandler.getTasks();
    }
    else if ($state.current.name == "task/edit") {
        $scope.taskHandler.getTask($scope.currentTaskId);
        $scope.taskHandler.getUsers($scope.currentTaskId);
        $scope.taskHandler.getStatuses();
    }
    else if ($state.current.name == "task/create") {
        $scope.taskHandler.getStatuses();
        UserService.getAllUsersWithSelectors().then(function (allUsers) {
            $scope.users = allUsers;
        })
    }

    $scope.$on("$destroy", function () {
        if (angular.isDefined($scope.interval)) {
            $interval.cancel($scope.interval);
            $scope.interval = 0;
        }
    });

});