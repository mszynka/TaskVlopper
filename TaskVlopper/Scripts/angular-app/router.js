/// <reference path="controllers/project.ctrl.js" />
/// <reference path="controllers/task.ctrl.js" />
/// <reference path="services/project.service.js" />
/// <reference path="services/task.service.js" />

app.config(function ($stateProvider, $urlRouterProvider) {
    
    $urlRouterProvider.otherwise("/project/list");
    $urlRouterProvider.when('', '/project/list');
    var urlPrepend = "/static/templates/";
    
    $stateProvider
        .state("index", {
            url: "",
            onEnter: function ($state) {
                $state.go('project/list');
            }
        })
        .state('project/list', {
            url: "/project/list",
            templateUrl: urlPrepend + "project/list.html",
            controller: "ProjectController as controller"
        })
        .state('project/view', {
            url: "/project/view/:projectId",
            templateUrl: urlPrepend + "project/viewer.html",
            controller: "ProjectController as controller"
        })
        .state('project/create', {
            url: "/project/create",
            templateUrl: urlPrepend + "project/create.html",
            controller: "ProjectController as controller"
        })
        .state('project/edit', {
            url: "/project/edit/:projectId",
            templateUrl: urlPrepend + "project/edit.html",
            controller: "ProjectController as controller"
        })
        .state('project/delete', {
            url: "/project/delete/:projectId",
            templateUrl: urlPrepend + "project/delete.html",
            controller: "ProjectController as controller"
        })
    
        .state('task/list', {
            url: "/task/list?projectId",
            templateUrl: urlPrepend + "task/list.html",
            controller: "TaskController as controller"
        })
        .state('task/create', {
            url: "/task/create?projectId",
            templateUrl: urlPrepend + "task/create.html",
            controller: "TaskController as controller"
        })
        .state('task/edit', {
            url: "/task/edit/:taskId?projectId",
            templateUrl: urlPrepend + "task/edit.html",
            controller: "TaskController as controller"
        })
        .state('task/delete', {
            url: "/task/delete/:taskId?projectId",
            templateUrl: urlPrepend + "task/delete.html",
            controller: "TaskController as controller"
        })

        .state('meeting/list', {
            url: "/meeting/list?projectId&taskId",
            templateUrl: urlPrepend + "meeting/list.html",
            controller: "MeetingController as controller"
        })
        .state('meeting/create', {
            url: "/meeting/create?projectId&taskId",
            templateUrl: urlPrepend + "meeting/create.html",
            controller: "MeetingController as controller"
        })
        .state('meeting/edit', {
            url: "/meeting/edit/:meetingId?projectId&taskId",
            templateUrl: urlPrepend + "meeting/edit.html",
            controller: "MeetingController as controller"
        })
        .state('meeting/view', {
            url: "/meeting/view/:meetingId?projectId&taskId",
            templateUrl: urlPrepend + "meeting/viewer.html",
            controller: "MeetingController as controller"
        })
        .state('meeting/delete', {
            url: "/meeting/delete/:meetingId?projectId",
            templateUrl: urlPrepend + "meeting/delete.html",
            controller: "MeetingController as controller"
        })

        .state('worklog/list', {
            url: "/worklog/list?projectId&taskId",
            templateUrl: urlPrepend + "worklog/list.html",
            controller: "WorklogController as controller"
        })
        .state('worklog/create', {
            url: "/worklog/create?projectId&taskId",
            templateUrl: urlPrepend + "worklog/create.html",
            controller: "WorklogController as controller"
        })
        .state('worklog/edit', {
            url: "/worklog/edit/:worklogId?projectId&taskId",
            templateUrl: urlPrepend + "worklog/edit.html",
            controller: "WorklogController as controller"
        })
        .state('worklog/delete', {
            url: "/worklog/delete/:worklogId?projectId&taskId",
            templateUrl: urlPrepend + "worklog/delete.html",
            controller: "WorklogController as controller"
        })
        
        .state('user/manage', {
            url: "/user/manage",
            templateUrl: urlPrepend + "user/manage.html",
            controller: "UserController as controller"
        })
        .state('user/manage/changepassword', {
            url: "/user/manage/changepassword",
            templateUrl: urlPrepend + "user/changepassword.html",
            controller: "UserController as controller"
        })
});