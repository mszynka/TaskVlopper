/// <reference path="services/meeting.service.js" />

app.controller('MeetingController', function ($scope, $timeout, $state, $stateParams, MeetingService) {

    $scope.currentTaskId = $stateParams.taskId;
    $scope.currentProjectId = $stateParams.projectId;
    $scope.currentMeetingId = $stateParams.meetingId;
    $scope.meetings = [];

    $scope.meetingHandler = {};
    $scope.meetingHandler.getMeetings = function () {
        MeetingService.getAllWithStats($scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $scope.meetings = response;
        })
    };

    if ($state.current.name == "meeting/list") {
        $scope.meetingHandler.getMeetings();
    }

    $scope.meetingHandler.getMeeting = function (meetingId) {
        MeetingService.get(meetingId, $scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $scope.model = response;
            $scope.model.DateAndTime = new Date(parseInt($scope.model.DateAndTime.split("(")[1]));
        })
    };

    $scope.meetingHandler.createMeeting = function () {
        Pace.restart();
        MeetingService.create($scope.model, $scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            MeetingService.getAll($scope.currentProjectId, $scope.currentTaskId).then(function (response) {
                angular.forEach(response, function (meeting) {
                    if (meeting.Title == $scope.model.Title
                        && meeting.Description == $scope.model.Description
                        && meeting.ProjectId == $scope.model.ProjectId) {
                        $scope.currentMeetingId = meeting.ID;
                        $scope.meetingHandler.bindUsersToMeeting();
                    }
                })
            })
            .then(function () {
                $state.go('meeting/list', { projectId: $scope.currentProjectId, taskId: $scope.currentTaskId });
            })
        })
    };

    $scope.meetingHandler.editMeeting = function () {
        Pace.restart();
        var temp_model = $scope.model;
        delete temp_model['$$hashkey'];
        $scope.meetingHandler.bindUsersToMeeting();
        MeetingService.update(temp_model, $scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $state.go('meeting/list', { projectId: $scope.currentProjectId, taskId: $scope.currentTaskId });
        });
    };

    $scope.meetingHandler.deleteMeeting = function () {
        Pace.restart();
        MeetingService.delete($scope.currentMeetingId, $scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $scope.model = null;
            $state.go('meeting/list', { projectId: $scope.currentProjectId, taskId: $scope.currentTaskId });
        });
    }

    $scope.meetingHandler.getUsers = function (meetingId) {
        $timeout(function () {
            if ($scope.users != null) {
                MeetingService.getUsers(meetingId)
                .then(function (response) {
                    angular.forEach(response, function (pUser) {
                        angular.forEach($scope.users, function (user) {
                            if (pUser === user.Email) {
                                user.isSelected = true;
                            }
                        });
                    });
                })
            }
            else {
                $timeout(function () {
                    MeetingService.getUsers(meetingId)
                    .then(function (response) {
                        angular.forEach(response, function (pUser) {
                            angular.forEach($scope.users, function (user) {
                                if (pUser === user.Email) {
                                    user.isSelected = true;
                                }
                            });
                        });
                    })
                }, 50);
            }
        }, 50);
    };

    $scope.meetingHandler.bindUsersToMeeting = function () {
        angular.forEach($scope.users, function (user) {
            if (user.isDirty && user.isSelectable)
                MeetingService.bindUser($scope.currentMeetingId, user.Email);
        })
    };

    $scope.sortByDate = function (element) {
        return new Date(parseInt(element.Meeting.DateAndTime.substr(6)));
    }

    if ($state.current.name == "meeting/edit" || $state.current.name == "meeting/view") {
        $scope.meetingHandler.getMeeting($scope.currentMeetingId);
        $scope.meetingHandler.getUsers($scope.currentMeetingId);
    }
});