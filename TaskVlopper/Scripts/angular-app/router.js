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
});