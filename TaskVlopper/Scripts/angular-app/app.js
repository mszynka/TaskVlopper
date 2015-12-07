var app = angular.module('TaskVlopperApp', []);

app.filter("aspDate", function () {
    return function (item) {
        if (item != null) {
            return new Date(parseInt(item.substr(6)));
        }
        return "";
    };
});