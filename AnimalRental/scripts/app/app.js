/*
    Main AngularJS function - module
*/

(function (ng) {
    'use strict';

    var myApp = ng.module('myApp', ['ngRoute', 'ngResource', 'ui.bootstrap', 'angularFileUpload']);   //[] - sukuria nauja moduli,  jei nera- tiesiog grazins jau egzistuojanti

    // We want to disable caching
    myApp.run(function ($rootScope, $templateCache) {
        $rootScope.$on('$routeChangeStart', function (event, next, current) {
            if (typeof (current) !== 'undefined') {
                $templateCache.remove(current.templateUrl);
            }
        });

    });

}(angular));

