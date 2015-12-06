angular.module("GamesApp")
    .controller("GamesController", ["$scope", "$gameService", function ($scope, $gameService) {
        $scope.showGames = function (data) {
            $scope.games = data;
        }

        $scope.getAll = function () {
            $gameService.getAll($scope.showGames);
        }

        $scope.delete = function (game) {
            $gameService.destroy(game.GameID, function (data) {
                $scope.games = $scope.games.filter(function (m) {
                    return m.GameID !== game.GameID;
                });
            });
        }

        $gameService.getAll($scope.showGames);
    }])
    .controller("CreateGameController", ["$scope", "$gameService", "$location", function ($scope, $gameService, $location) {
        $scope.game = {};
        $scope.title = "Create game";

        $scope.save = function () {
            $gameService.create($scope.game, function (data) {
                $location.path("/");
            });
        }
    }])
    .controller("EditGameController", ["$scope", "$gameService", "$location", "$routeParams", function ($scope, $gameService, $location, $routeParams) {
        $scope.title = "Edit game";
        $scope.showMovie = function (data) {
            $scope.game = data;
        }

        $scope.save = function () {
            $gameService.update($scope.game, function (data) {
                $location.path("/");
            });
        }

        $gameService.getByID($routeParams.id, $scope.showMovie);
    }])
    .controller("GameDetailsController", ["$scope", "$gameService", "$location", "$routeParams", function ($scope, $gameService, $location, $routeParams) {
        $scope.showMovie = function (data) {
            $scope.game = data;
        }

        $scope.delete = function (game) {
            $gameService.destroy(game.GameID, function (data) {
                $location.path("/");
            });
        }

        $gameService.getByID($routeParams.id, $scope.showMovie);
    }]);