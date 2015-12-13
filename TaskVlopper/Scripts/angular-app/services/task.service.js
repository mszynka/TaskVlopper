app.service('TaskService', ['$http', function($http) {
        
        this.getAll = function(projectId) {
            return $http.get('/Task?projectId=' + projectId);
        }

    }
]);