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
    
});