app
    .directive('tvMeetingViewer', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/meeting/viewer.html"
        }
    })
    .directive('tvMeetingModeleditor', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/meeting/modeleditor.html"
        }
    });