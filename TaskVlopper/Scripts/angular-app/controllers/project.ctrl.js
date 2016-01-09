/// <reference path="services/project.service.js" />
/// <reference path="services/task.service.js" />
/// <reference path="services/meeting.service.js" />
/// <reference path="services/worklog.service.js" />

app.controller('ProjectController', function ($scope, $state, $stateParams,
    ProjectService,
    TaskService,
    MeetingService,
    WorklogService) {

    Pace.on("done",
        function () {
            $('#modelDeadline').parent().datetimepicker({
                format: "MM/DD/YYYY",
                useCurrent: false
            });
            if($("#modelStartDate") != [])
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
        ProjectService.getAll().then(function (response) {
            angular.forEach(response, function (project) {
                TaskService.getAll(project.ID).then(function (response) {
                    if (response != undefined)
                        project.taskCount = response.length;
                    else
                        project.taskCount = 0;
                    
                });
                MeetingService.getAll(project.ID, null).then(function (response) {
                    if (response != undefined)
                        project.futureMeetingCount = response.length;
                    else
                        project.futureMeetingCount = 0;
                });
            })
            $scope.projects = response;
        })
    };
    $scope.projectHandler.getProjects();

    $scope.projectHandler.getProject = function (projectId) {
        ProjectService.get(projectId).then(function (response) {
            $scope.model = response;
            if($scope.model.StartDate != undefined)
                $scope.model.StartDate = new Date(parseInt($scope.model.StartDate.split("(")[1]));
            if($scope.model.Deadline != undefined)
                $scope.model.Deadline = new Date(parseInt($scope.model.Deadline.split("(")[1]));
        })
    };

    $scope.projectHandler.createProject = function () {
        Pace.start();
        ProjectService.create($scope.model).then(function (response) {
            $state.go('project/list');
        })
    };

    $scope.projectHandler.initEditor = function () {
        $scope.projectHandler.getProject($scope.currentProjectId);
    };

    $scope.projectHandler.editProject = function () {
        Pace.start();
        var temp_model = $scope.model;
        delete temp_model['$$hashkey'];
        ProjectService.update(temp_model).then(function (response) {
            $state.go('project/list');
        });
    };

    $scope.projectHandler.deleteProject = function () {
        Pace.start();
        ProjectService.delete($scope.currentProjectId).then(function (response) {
            $scope.model = null;
            $state.go('project/list');
        });
    };

});