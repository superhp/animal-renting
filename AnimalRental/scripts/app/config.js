/*
    Here we define AngularJS routes
*/

(function (ng) {

    'use strict';

    ng.module('myApp').config([
        '$routeProvider', '$locationProvider',
		 function ($routeProvider, $locationProvider) {
		     $locationProvider.html5Mode(true);

		     $routeProvider
                 .when('/', { templateUrl: '/home' })
                 .when('/home', { templateUrl: '/home' })
                 .when('/animals', { templateUrl: '/animals', controller: 'animalGetController' })
                 .when('/login', { templateUrl: '/user/login' })
                 .when('/register', { templateUrl: '/user/register' })
                 .when('/details/:id', { templateUrl: '/details', controller: 'animalDetailsController' })
                 .when('/publish', { templateUrl: '/publish', controller: 'animalPublishController' })
                 .when('/edit/:id', { templateUrl: '/edit', controller: 'animalEditController' })
                 .otherwise({ redirectTo: '/oi' });
		 }
    ]);

}(angular));