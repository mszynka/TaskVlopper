app
    .directive('tvProjectViewer', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/project/modelviewer.html"
        }
    })
    .directive('tvProjectModeleditor', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/project/modeleditor.html"
        }
    });