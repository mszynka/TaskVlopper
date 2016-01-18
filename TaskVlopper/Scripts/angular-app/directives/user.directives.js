app
    .directive('tvUserListSelector', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/user/list-selector.html"
        }
    })
    .directive('tvUserListViewer', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/user/list-viewer.html"
        }
    });