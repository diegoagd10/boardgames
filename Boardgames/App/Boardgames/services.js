angular.module("GamesApp")
    .factory("$gameService", ["$http", function ($http) {
        var gameService = {};

        gameService.url = "/api/Games";

        gameService.getAll = function (callback) {
            $http.get(this.url).success(callback);
        }

        gameService.getByID = function (ID, callback) {
            var url = gameService.url + "/" + ID;
            $http.get(url).success(callback);
        }

        gameService.create = function (movie, callback) {
            $http.post(this.url, movie).success(callback);
        }

        gameService.update = function (movie, callback) {
            $http.put(this.url, movie).success(callback);
        }

        gameService.destroy = function (ID, callback) {
            var url = gameService.url + "/" + ID;
            $http.delete(url, ID).success(callback);
        }
        return gameService;
    }]);