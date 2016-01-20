/// <reference path="services/meeting.service.js" />
/// <reference path="services/user.service.js" />

app.controller('MeetingController', function ($scope, $rootScope, $filter, $state, $stateParams,
    MeetingService,
    TaskService,
    ProjectService,
    UserService) {

    $scope.currentTaskId = $stateParams.taskId;
    $scope.currentProjectId = $stateParams.projectId;

    $rootScope.currentProjectId = $stateParams.projectId;
    $rootScope.projectView = false;
    $rootScope.taskView = false;
    $rootScope.meetingView = true;

    $scope.currentMeetingId = $stateParams.meetingId;
    $scope.meetings = [];

    $scope.meetingHandler = {};
    $scope.meetingHandler.getMeetings = function () {
        MeetingService.getAllWithStats($scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $scope.meetings = response;
        })
    };

    $scope.meetingHandler.getMeeting = function (meetingId) {
        MeetingService.get(meetingId, $scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $scope.model = response;
            $scope.model.DateAndTime = new Date(parseInt($scope.model.DateAndTime.split("(")[1]));
        })
    };

    $scope.meetingHandler.createMeeting = function () {
        console.log($scope.model.TaskID);
        MeetingService.create($scope.model, $scope.currentProjectId, $scope.model.TaskID)
            .then(function (meeting) {
                $scope.currentMeetingId = meeting.data.ID;
                $scope.meetingHandler.bindUsersToMeeting();
            })
            .then(function () {
                $state.go('meeting/list', { projectId: $scope.currentProjectId, taskId: $scope.currentTaskId });
            })
    };

    $scope.meetingHandler.editMeeting = function () {
        var temp_model = $scope.model;
        delete temp_model['$$hashkey'];
        $scope.meetingHandler.bindUsersToMeeting();
        MeetingService.update(temp_model, $scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $state.go('meeting/list', { projectId: $scope.currentProjectId, taskId: $scope.currentTaskId });
        });
    };

    $scope.meetingHandler.deleteMeeting = function () {
        MeetingService.delete($scope.currentMeetingId, $scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $scope.model = null;
            $state.go('meeting/list', { projectId: $scope.currentProjectId, taskId: $scope.currentTaskId });
        });
    }

    $scope.meetingHandler.getUsers = function (meetingId) {
        UserService.getAllUsersWithSelectors().then(function (allUsers) {
            $scope.users = allUsers;
            MeetingService.getUsers(meetingId).then(function (meetingUsers) {
                angular.forEach(meetingUsers, function (meetingUser) {
                    angular.forEach($scope.users, function (user) {
                        if (meetingUser.Email === user.Email) {
                            user.isSelected = true;
                        }
                    });
                });
                ProjectService.getUsers($scope.currentProjectId).then(function (projectUsers) {
                    angular.forEach($scope.users, function (user) {
                        angular.forEach(projectUsers.Users, function (projectUser) {
                            if (projectUser.Email === user.Email) {
                                user.isProjectUser = true;
                            }
                        })
                    });
                    $scope.users = $filter('filter')($scope.users, { isProjectUser: 'true' });
                })
            })
        })
    };

    $scope.meetingHandler.getTasks = function (projectId) {
        TaskService.getAll(projectId).then(function (response) {
            $scope.tasks = response.Tasks;
            if ($scope.model == undefined)
                $scope.model = [];
            if ($scope.model.TaskID == undefined)
                $scope.model.TaskID = null;
        })
    };

    $scope.meetingHandler.bindUsersToMeeting = function () {
        angular.forEach($scope.users, function (user) {
            if (user.isDirty && user.isSelectable) {
                if (user.isSelected)
                    MeetingService.bindUser($scope.currentMeetingId, user.Email);
                else if (!user.isSelected)
                    MeetingService.unbindUser($scope.currentMeetingId, user.Email);
            }
        })
    };

    $scope.sortByDate = function (element) {
        return new Date(parseInt(element.Meeting.DateAndTime.substr(6)));
    }

    if ($state.current.name == "meeting/list") {
        $scope.meetingHandler.getMeetings();
    }
    else if ($state.current.name == "meeting/edit" || $state.current.name == "meeting/view") {
        $scope.tasks = [];
        $scope.meetingHandler.getMeeting($scope.currentMeetingId);
        $scope.meetingHandler.getUsers($scope.currentMeetingId);
        $scope.meetingHandler.getTasks($scope.currentProjectId);
    }
    else if ($state.current.name == "meeting/create") {
        $scope.meetingHandler.getTasks($scope.currentProjectId);
        UserService.getAllUsersWithSelectors().then(function (allUsers) {
            $scope.users = allUsers;
            ProjectService.getUsers($scope.currentProjectId).then(function (projectUsers) {
                angular.forEach($scope.users, function (user) {
                    angular.forEach(projectUsers.Users, function (projectUser) {
                        if (projectUser.Email === user.Email) {
                            user.isProjectUser = true;
                        }
                    })
                });
                $scope.users = $filter('filter')($scope.users, { isProjectUser: 'true' });
            })
        })
    }
});