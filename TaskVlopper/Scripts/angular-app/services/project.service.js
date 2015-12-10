angular.module('taskVlopperApp')
    .service('ProjectService', ['$http', function ($http) {

        this.getAll = function () {
            return $http.get('/Project');
        };

        this.get = function (projectId) {
            if (projectId !== undefined || !isNaN(projectId)) {
                return $http.get('/Project/Details/' + projectId);
            }
            else {
                $scope.status = "[ProjectService.get] ProjectID is invalid!";
                console.log($scope.status);
            }
        };

        this.create = function (model) {
            model = JSON.stringify(model);
            return $http({
                method: 'POST',
                url: '/Project/Create',
                accept: 'application/json',
                data: model
            })
        };

        this.update = function (model) {
            return $http({
                method: 'POST',
                url: '/Project/Edit/' + model.ID,
                accept: 'application/json',
                data: model
            });
        }

        this.delete = function (projectId) {
            if (projectId !== undefined || !isNaN(projectId))
                return $http.post('/Project/Delete/' + projectId);
            
            else {
                $scope.status = "[ProjectService.delete] ProjectID is invalid!";
                console.log($scope.status);
            }
        };

    }]);