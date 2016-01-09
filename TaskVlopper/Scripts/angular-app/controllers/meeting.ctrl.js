/// <reference path="services/meeting.service.js" />

app.controller('MeetingController', function ($scope, $state, $stateParams, MeetingService) {

    $scope.currentTaskId = $stateParams.taskId;
    $scope.currentProjectId = $stateParams.projectId;
    $scope.currentMeetingId = $stateParams.meetingId;
    $scope.meetings = [];

    $scope.meetingHandler = {};
    $scope.meetingHandler.getMeetings = function () {
        MeetingService.getAll($scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $scope.meetings = response;
        })
    };
    $scope.meetingHandler.getMeetings();

    $scope.meetingHandler.getMeeting = function (meetingId) {
        MeetingService.get(meetingId, $scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $scope.model = response;
            $scope.model.DateAndTime = new Date(parseInt($scope.model.DateAndTime.split("(")[1]));
        })
    };

    $scope.meetingHandler.createMeeting = function () {
        Pace.restart();
        MeetingService.create($scope.model, $scope.currentProjectId, $scope.currentTaskId).then(function (response) {
            $state.go('meeting/list', { projectId: $scope.currentProjectId, taskId: $scope.currentTaskId });
        })
    };

    $scope.meetingHandler.initEditor = function () {
        $scope.meetingHandler.getMeeting($scope.currentMeetingId);
    };

    $scope.meetingHandler.editMeeting = function () {
        Pace.restart();
        var temp_model = $scope.model;
        delete temp_model['$$hashkey'];
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
});