/*
    Edit page controller
*/

(function (ng) {
    'use strict';

    ng.module('myApp').controller('animalEditController', [
        '$scope', '$upload', '$resource', '$location', '$routeParams',
        function ($scope, $upload, $resource, $location, $routeParams) {

            // Fill the form
            $scope.model = $resource('/umbraco/api/animalsapi/getanimal/' + $routeParams.id).get();

            // List of species for drop-down selection
            $scope.species = $resource('/umbraco/api/animalspeciesapi/get').query();

            // Get the uploadded file instance
            var ifNewFile = false;
            $scope.onFileSelect = function ($files) {
                $scope.file = $files[0];
                ifNewFile = true;
            };

            // Submit the edited model
            $scope.submit = function () {
                if ($scope.articleForm.$valid) {
                    if (ifNewFile) {
                        // image has been changed
                        $upload.upload({
                            url: '/umbraco/api/fileuploadapi/upload',
                            method: "POST",
                            file: $scope.file,
                            progress: function () { }
                        }).then(function (data) {
                            // file is uploaded successfully
                            $scope.model.PhotoUrl = data.data.fileUrl;
                            $resource('/umbraco/api/animalsapi/put', null, { 'update': { method: 'PUT' } }).update($scope.model, function () {
                                $location.url('/animals/');
                            });
                        });
                    } else {
                        // image has not been changed
                        $resource('/umbraco/api/animalsapi/put', null, { 'update': { method: 'PUT' } }).update($scope.model, function () {
                            $location.url('/animals/');
                        });
                    }
                } else {
                    $scope.articleForm.$dirty = true;
                }
            };
        }
    ]);
}(angular));