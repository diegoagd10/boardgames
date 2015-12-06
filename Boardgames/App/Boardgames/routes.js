angular.module("GamesApp")
    .config(["$routeProvider", function ($routeProvider) {
        $routeProvider
            .when("/", {
                controller: "GamesController",
                templateUrl: "/App/Boardgames/views/gamesCollection.html"
            })
            .when("/GamesAdmin/Register", {
                controller: "CreateGameController",
                templateUrl: "/App/Boardgames/views/register.html"
            })
            .when("/GamesAdmin/Edit/:id", {
                controller: "EditGameController",
                templateUrl: "/App/Boardgames/views/register.html"
            })
            .when("/GamesAdmin/Details/:id", {
                controller: "GameDetailsController",
                templateUrl: "/App/Boardgames/views/details.html"
            });
    }]);