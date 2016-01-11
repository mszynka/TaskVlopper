/// <reference path="services/project.service.js" />
/// <reference path="services/task.service.js" />
/// <reference path="services/meeting.service.js" />
/// <reference path="services/worklog.service.js" />
/// <reference path="services/user.service.js" />

app.controller('ProjectController', function ($scope, $timeout, $filter, $state, $stateParams,
    ProjectService,
    TaskService,
    MeetingService,
    WorklogService,
    UserService) {

    Pace.on("done",
        function () {
            $('#modelDeadline').parent().datetimepicker({
                format: "MM/DD/YYYY",
                useCurrent: false
            });
            if ($("#modelStartDate") != [])
                $("#modelStartDate").parent().on("dp.change", function (e) {
                    $('#modelDeadline').parent().data("DateTimePicker").minDate(e.date);
                });
            if ($("#modelDeadline") != [])
                $("#modelDeadline").parent().on("dp.change", function (e) {
                    $('#modelStartDate').parent().data("DateTimePicker").maxDate(e.date);
                });
        });

    $scope.currentProjectId = $stateParams.projectId;
    $scope.projectHandler = {};

    $scope.projectHandler.getProjects = function () {
        ProjectService.getAllWithStats().then(function (response) {
            $scope.projects = response;
        })
    };

    if ($state.current.name == "project/list") {
        $scope.projectHandler.getProjects();
    }

    $scope.projectHandler.getProject = function (projectId) {
        ProjectService.get(projectId).then(function (response) {
            $scope.model = response;
            if ($scope.model.StartDate != undefined)
                $scope.model.StartDate = new Date(parseInt($scope.model.StartDate.split("(")[1]));
            if ($scope.model.Deadline != undefined)
                $scope.model.Deadline = new Date(parseInt($scope.model.Deadline.split("(")[1]));
        })
    };

    $scope.projectHandler.createProject = function () {
        Pace.restart();
        ProjectService.create($scope.model)
            .then(function (response) {
                $scope.currentProjectId = response.data.ID;
                $scope.projectHandler.bindUsersToProject();
            })
            .then(function () {
                $state.go('project/list');
            })
    };

    $scope.projectHandler.editProject = function () {
        Pace.restart();
        var temp_model = $scope.model;
        delete temp_model['$$hashkey'];
        $scope.projectHandler.bindUsersToProject();
        ProjectService.update(temp_model).then(function (response) {
            $state.go('project/list');
        });
    };

    $scope.projectHandler.deleteProject = function () {
        Pace.restart();
        ProjectService.delete($scope.currentProjectId).then(function (response) {
            $scope.model = null;
            $state.go('project/list');
        });
    };

    $scope.projectHandler.getUsers = function (projectId) {
        UserService.getAllUsersWithSelectors().then(function (allUsers) {
            $scope.users = allUsers;
            ProjectService.getUsers(projectId).then(function (projectUsers) {
                angular.forEach(projectUsers.Users, function (projectUser) {
                    angular.forEach($scope.users, function (user) {
                        if (projectUser.Email === user.Email) {
                            user.isSelected = true;
                        }
                    });
                });
            })
        })
    };

    $scope.projectHandler.bindUsersToProject = function () {
        angular.forEach($scope.users, function (user) {
            if (user.isDirty && user.isSelectable) {
                if (user.isSelected)
                    ProjectService.bindUser($scope.currentProjectId, user.Email);
                else if (!user.isSelected)
                    ProjectService.unbindUser($scope.currentProjectId, user.Email);
            }
        })
    };

    if ($state.current.name == "project/edit") {
        $scope.projectHandler.getProject($scope.currentProjectId);
        $scope.projectHandler.getUsers($scope.currentProjectId);
    }

});