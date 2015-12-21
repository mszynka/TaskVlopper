/// <reference path="services/project.service.js" />

app.controller('ProjectController', function ($scope, $state, $stateParams, ProjectService) {

    Pace.on("done",
        function () {
            $('#modelDeadline').parent().datetimepicker({
                format: "MM/DD/YYYY",
                useCurrent: false
            });
            $("#modelStartDate").parent().on("dp.change", function (e) {
                $('#modelDeadline').parent().data("DateTimePicker").minDate(e.date);
            });
            $("#modelDeadline").parent().on("dp.change", function (e) {
                $('#modelStartDate').parent().data("DateTimePicker").maxDate(e.date);
            });
        });
    

    $scope.currentProjectId = $stateParams.projectId;
    $scope.projectHandler = {};

    $scope.projectHandler.getProjects = function () {
        ProjectService.getAll().then(function (response) {
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
        ProjectService.create($scope.model).then(function (response) {
            $state.go('project/list');
        })
    };

    $scope.projectHandler.initEditor = function () {
        $scope.projectHandler.getProject($scope.currentProjectId);
    };

    $scope.projectHandler.editProject = function () {
        var temp_model = $scope.model;
        delete temp_model['$$hashkey'];
        ProjectService.update(temp_model).then(function (response) {
            $state.go('project/list');
        });
    };

    $scope.projectHandler.deleteProject = function () {
        ProjectService.delete($scope.currentProjectId).then(function (response) {
            $scope.model = null;
            $state.go('project/list');
        });
    }
});