/*
    Details' page controller:
        for getting the details by id
        for writing and geting comments
        for showing modal and changing animal rent status
*/

(function (ng) {
    'use strict';

    ng.module('myApp').controller('animalDetailsController', [
        '$scope', '$modal', '$resource', '$location', '$routeParams',
        function ($scope, $modal, $resource, $location, $routeParams) {

            // Get animal details part
            $scope.animal = $resource('/umbraco/api/animalsapi/getanimal/' + $routeParams.id).get(function() {
                ($resource('/umbraco/api/usersapi/getuserbyid/' + $scope.animal.UserId).get(function(data) {
                    $scope.userName = data.userName;
                }));
            });

            // Function for rent status change
            $scope.rentng = function(rentTask) {
                var modalInstance = $modal.open({
                    templateUrl: 'myModalContent.html',
                    controller: 'modalInstanceCtrl',
                    size: 'sm',
                    resolve: {
                        id: function() {
                            return $routeParams.id;
                        },
                        rTask: function() {
                            return rentTask;
                        }
                    }
                });
                modalInstance.result.then(function(newStatus) {
                    $scope.animal.Status = newStatus;
                });
            };

            // part for sending and getting comments
            $scope.model = {};
            $scope.sendMessage = function() {
                var data = {
                    AnimalId: $routeParams.id,
                    Author: $scope.model.Author,
                    Message: $scope.model.Message,
                    PublishDate: Date.now()
                };

                $resource('/umbraco/api/commentsapi/addcomment').save(data, function() {
                    $scope.animal.Comments = $resource('/umbraco/api/commentsapi/getcommentsbyanimal/' + $routeParams.id).query();
                    $scope.model.Author = "";
                    $scope.model.Message = "";
                });
            };
        }
    ]);


    ng.module('myApp').controller('modalInstanceCtrl', [
        '$scope', '$resource', '$modalInstance', 'id', 'rTask',
        function($scope, $resource, $modalInstance, id, rTask) {

            $scope.modalMessage = rTask === 'rentanimal' ? 'Do you really want to rent this animal?' : 'Do you really want to return this animal?';

            $scope.ok = function() {
                var newStatus = 0;
                if (rTask === 'rentanimal') {
                    newStatus = 1;
                };
                $resource('/umbraco/api/animalsapi/putstatus', null, { 'update': { method: 'PUT' } }).update({ AnimalId: id, Status: newStatus });

                $modalInstance.close(newStatus);
            };

            $scope.cancel = function() {
                $modalInstance.dismiss('cancel');
            };
        }
    ]);

}(angular));