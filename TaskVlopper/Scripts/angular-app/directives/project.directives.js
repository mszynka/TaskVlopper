app
    .directive('tvProjectViewer', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/project/viewer.html"
        }
    })
    .directive('tvProjectModeleditor', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/project/modeleditor.html"
        }
    });