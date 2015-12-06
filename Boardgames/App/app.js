angular.module("GamesApp", [
    "ngRoute"
])
    .config(["$routeProvider", function ($routeProvider) {
        $routeProvider
            .otherwise({
                redirectTo: '/'
            });
    }]);