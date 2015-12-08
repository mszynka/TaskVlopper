var app = angular.module('taskVlopperApp', ['ui.bootstrap', 'ui.bootstrap.datepicker']);

app.filter("aspDate", function () {
    return function (item) {
        if (item != null) {
            return new Date(parseInt(item.substr(6)));
        }
        return "";
    };
});
