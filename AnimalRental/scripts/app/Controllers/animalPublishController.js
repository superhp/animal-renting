/*
    Publish page controller
*/

(function (ng) {
    'use strict';

    ng.module('myApp').controller('animalPublishController', [
        '$scope', '$upload', '$resource', '$location',
        function ($scope, $upload, $resource, $location) {
            $scope.model = {};

            // List of species for drop-down selection
            $scope.species = $resource('/umbraco/api/animalspeciesapi/get').query();

            // Get the uploadded file instance
            var ifNewFile = false;
            $scope.onFileSelect = function ($files) {
                $scope.file = $files[0];
                ifNewFile = true;
            };

            // Submit the model
            $scope.submit = function () {
                if ($scope.articleForm.$valid && ifNewFile) {
                    $upload.upload({
                        url: '/umbraco/api/fileuploadapi/upload',
                        method: "POST",
                        file: $scope.file,
                        progress: function () { }
                    }).then(function (data) {
                        // file is uploaded successfully
                        $scope.model.PhotoUrl = data.data.fileUrl;
                        $resource('/umbraco/api/animalsapi/post').save($scope.model, function () {
                            $location.url('/animals/');
                        });
                    });
                } else {
                    $scope.articleForm.$dirty = true;
                }
            };
        }
    ]);
}(angular));