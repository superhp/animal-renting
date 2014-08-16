/*
    Animals list page controller:
*/

(function (ng) {
    'use strict';

    ng.module('myApp').controller('animalGetController', [
        '$scope', '$resource',
        function ($scope, $resource) {

            // Get loged-in user id for enabling edit button
            $scope.currentUser = $resource('/umbraco/api/usersapi/getcurrentuser').get();

            // Get animals
            $scope.animals = $resource('/umbraco/api/animalsapi/get').query(function() {
                for (var i = 0; i < $scope.animals.length; i++) {
                    ($scope.animals[i]).Status = ($scope.animals[i]).Status == 0 ? 'Available' : 'Rented';
                    ($scope.animals[i]).UserId = { id: ($scope.animals[i]).UserId, name: $resource('/umbraco/api/usersapi/getuserbyid/' + ($scope.animals[i]).UserId).get() };
                }
            });

        }
    ]);
}(angular));