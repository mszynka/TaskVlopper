app
    .directive('tvMeetingModelTableViewer', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/meeting/modeltableviewer.html"
        }
    })
    .directive('tvMeetingModeleditor', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/meeting/modeleditor.html"
        }
    });