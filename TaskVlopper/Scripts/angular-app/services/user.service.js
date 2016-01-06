app.service('UserService', ['$http', function ($http) {

    this.getAll = function () {
        return $http.get('/') //TODO
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            //return response.data.Projects;
        })
        .catch(function (error) {
            $scope.status = '[UserService.getAll] Unable to load data: ' + error.data.message;
            console.log($scope.status);
        });
    };

    this.get = function () {
        return $http.get('/') //TODO
        .then(function (response) {
            if (response.data.HttpCode != undefined) {
                console.log(response.data.HttpCode + " " + response.data.Message);
            }
            //return response.data.Project;
        })
        .catch(function (error) {
            $scope.status = '[UserService.get] Unable to load data: ' + error.message;
            console.log($scope.status);
        });
    };

    //this.create = function (model) {
    //    var newmodel = JSON.stringify(model);
    //    return $http({
    //        method: 'POST',
    //        url: '/Project/Create',
    //        accept: 'application/json',
    //        data: newmodel
    //    })
    //    .then(function (response) {
    //        if (response.data.HttpCode != undefined) {
    //            console.log(response.data.HttpCode + " " + response.data.Message);
    //        }
    //        return response;
    //    })
    //    .catch(function (error) {
    //        $scope.status = '[ProjectService.create] Unable to load data: ' + error.message;
    //        console.log($scope.status);
    //    });
    //};

    //this.update = function (model) {
    //    return $http({
    //        method: 'POST',
    //        url: '/Project/Edit/' + model.ID,
    //        accept: 'application/json',
    //        data: model
    //    })
    //    .then(function (response) {
    //        if (response.data.HttpCode != undefined) {
    //            console.log(response.data.HttpCode + " " + response.data.Message);
    //        }
    //        return response;
    //    })
    //    .catch(function (error) {
    //        $scope.status = '[ProjectService.update] Unable to load data: ' + error.message;
    //        console.log($scope.status);
    //    });
    //}

    //this.delete = function (projectId) {
    //    if (projectId !== undefined || !isNaN(projectId))
    //        return $http.post('/Project/Delete/' + projectId)
    //        .then(function (response) {
    //            if (response.data.HttpCode != undefined) {
    //                console.log(response.data.HttpCode + " " + response.data.Message);
    //            }
    //            return response;
    //        })
    //        .catch(function (error) {
    //            $scope.status = '[ProjectService.delete] Unable to load data: ' + error.message;
    //            console.log($scope.status);
    //        });
    //
    //    else {
    //        $scope.status = "[ProjectService.delete] ProjectID is invalid!";
    //        console.log($scope.status);
    //        return null;
    //    }
    //};

}]);