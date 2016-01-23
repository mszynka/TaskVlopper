app
    .directive('tvTaskModeleditor', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/task/modeleditor.html"
        }
    })
    .directive('tvTaskModelTableViewer', function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "static/templates/task/modeltableviewer.html"
        }
    })

    .directive('convertToNumber', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                ngModel.$parsers.push(function (val) {
                    return parseInt(val, 10);
                });
                ngModel.$formatters.push(function (val) {
                    return '' + val;
                });
            }
        };
    });