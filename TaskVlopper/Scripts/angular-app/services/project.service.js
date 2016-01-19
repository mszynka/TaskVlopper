app.service('ProjectService', ['$http', function ($http) {

        this.getAll = function () {
            return $http.get('/Project')
            .then(function (response) {
                if (response.data.HttpCode != undefined) {
                    console.log(response.data.HttpCode + " " + response.data.Message);
                }
                return response.data.Projects;
            })
            .catch(function (error) {
                console.log('[ProjectService.getAll] Unable to load data: ' + error.data.message);
            });
        };

        this.getAllWithStats = function () {
            return $http.get('/Project/GetAllWithStats')
            .then(function (response) {
                if (response.data.HttpCode != undefined) {
                    console.log(response.data.HttpCode + " " + response.data.Message);
                }
                return response.data.Projects;
            })
            .catch(function (error) {
                console.log('[ProjectService.getAllWithStats] Unable to load data: ' + error.data.message);
            });
        };

        this.get = function (projectId) {
            if (projectId !== undefined || !isNaN(projectId)) {
                return $http.get('/Project/Details/' + projectId)
                .then(function (response) {
                    if (response.data.HttpCode != undefined) {
                        console.log(response.data.HttpCode + " " + response.data.Message);
                    }
                    return response.data;
                })
                .catch(function (error) {
                    console.log('[ProjectService.get] Unable to load data: ' + error.message);
                });
            }
            else {
                console.log("[ProjectService.get] ProjectID is invalid!");
                return null;
            }
        };

        this.create = function (model) {
            var newmodel = JSON.stringify(model);
            return $http({
                method: 'POST',
                url: '/Project/Create',
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
                console.log('[ProjectService.create] Unable to load data: ' + error.message);
            });
        };

        this.update = function (model) {
            return $http({
                method: 'POST',
                url: '/Project/Edit/' + model.ID,
                accept: 'application/json',
                data: model
            })
            .then(function (response) {
                if (response.data.HttpCode != undefined) {
                    console.log(response.data.HttpCode + " " + response.data.Message);
                }
                return response;
            })
            .catch(function (error) {
                console.log('[ProjectService.update] Unable to load data: ' + error.message);
            });
        }

        this.delete = function (projectId) {
            if (projectId !== undefined || !isNaN(projectId))
                return $http.post('/Project/Delete/' + projectId)
                .then(function (response) {
                    if (response.data.HttpCode != undefined) {
                        console.log(response.data.HttpCode + " " + response.data.Message);
                    }
                    return response;
                })
                .catch(function (error) {
                    console.log('[ProjectService.delete] Unable to load data: ' + error.message);
                });
            
            else {
                console.log("[ProjectService.delete] ProjectID is invalid!");
                return null;
            }
        };

        this.getUsers = function (projectId) {
            if (projectId !== undefined && !isNaN(projectId)) {
                return $http.get('/Project/Users/' + projectId)
                .then(function (response) {
                    if (response.data.HttpCode != undefined) {
                        console.log(response.data.HttpCode + " " + response.data.Message);
                    }
                    return response.data;
                })
                .catch(function (error) {
                    console.log('[ProjectService.getUsers] Unable to load data: ' + error.message);
                });
            }
            else {
                console.log("[ProjectService.getUsers] ProjectID is invalid!");
                return null;
            }
        }

        this.bindUser = function (projectId, userId) {
            if (projectId !== undefined && !isNaN(projectId)) {
                return $http.post('/Project/Users/' + projectId + "?userId=" + userId)
                .then(function (response) {
                    if (response.data.HttpCode != undefined) {
                        console.log(response.data.HttpCode + " " + response.data.Message);
                    }
                    return response;
                })
                .catch(function (error) {
                    console.log('[ProjectService.bindUser] Unable to load data: ' + error.message);
                });
            }
            else {
                console.log("[ProjectService.bindUser] ProjectID is invalid!");
                return null;
            }
        }

        this.unbindUser = function (projectId, userId) {
            if (projectId !== undefined && !isNaN(projectId)) {
                return $http.post('/Project/UnbindUser/' + projectId + "?userId=" + userId)
                .then(function (response) {
                    if (response.data.HttpCode != undefined) {
                        console.log(response.data.HttpCode + " " + response.data.Message);
                    }
                    return response;
                })
                .catch(function (error) {
                    console.log('[ProjectService.unbindUser] Unable to load data: ' + error.message);
                });
            }
            else {
                console.log("[ProjectService.unbindUser] ProjectID is invalid!");
                return null;
            }
        }

    }]);