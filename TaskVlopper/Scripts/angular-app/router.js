app.config(function ($stateProvider, $urlRouterProvider) {
    
    $urlRouterProvider.otherwise("/project/list");
    var urlPrepend = "/static/templates/";
    
    $stateProvider
        .state("index", {
            url: "",
            onEnter: function () {
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
            url: "/project/edit",
            templateUrl: urlPrepend + "project/edit.html",
            controller: "ProjectController as controller"
        })
        .state('project/delete', {
            url: "/project/delete",
            templateUrl: urlPrepend + "project/delete.html",
            controller: "ProjectController as controller"
        })
    
});