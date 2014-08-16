/*
    Carousel (home-page) controller
*/

(function (ng) {
    ng.module('myApp').controller('carouselController', ['$scope', '$resource', function ($scope, $resource) {

        // slide change interval
        $scope.myInterval = 3000;

        var slides = $scope.slides = [];

        // Get last five published animals
        $scope.lastAnimals = $resource('/umbraco/api/animalsapi/getlastfive').query();

    }]);
}(angular));