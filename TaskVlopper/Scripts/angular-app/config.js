angular.module('taskVlopperApp', [])
    .config(["$httpProvider",
        function ($httpProvider) {

            var httpStatusCodeInterceptorFactory = function ($q) {

                function onSuccess(response) {
                    if ("success_condition") {
                        return response.data;
                    }
                    else {
                        //Show your global error dialog 
                        $q.reject(response.data);//Very important to reject the error 
                    }
                };

                function onError(response) {
                    //Show your global error dialog 
                    $q.reject(response);//Very important to reject the error 
                };
            }
        }]);