app
    .directive('tvTaskViewer', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/task/viewer.html"
        }
    })
    .directive('tvTaskModeleditor', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/task/modeleditor.html"
        }
    });