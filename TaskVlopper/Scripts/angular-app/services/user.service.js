app.service('UserService', ['$http', function ($http) {

    this.getManage = function () {
        return $http.get('/Manage')
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response.data;
        })
        .catch(function (error) {
            $scope.status = '[UserService.getAll] Unable to load data: ' + error.data.message;
            console.log($scope.status);
        });
    };

    this.changePassword = function (model) {
        var newmodel = JSON.stringify(model);
        return $http({
            method: 'POST',
            url: '/Manage/ChangePassword',
            accept: 'application/json',
            data: newmodel
        })
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            return response;
        })
            .catch(function (error) {
                $scope.status = '[UserService.changePassword] Unable to load data: ' + error.message;
                console.log($scope.status);
            });
    };
}]);