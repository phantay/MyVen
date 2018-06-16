'use strict';
var app = angular.module('venApp', ["ngRoute", "akFileUploader"]);

app.config(function ($routeProvider, $locationProvider) {
    $routeProvider
    .when("/home", {
        templateUrl: "/app/views/ban-tin.html",
        controller: "banTinCtrl"
    })
    .when("/tukhoa/:id", {
        templateUrl: "/app/views/tu-khoa.html",
        controller: "tuKhoaCtrl"
    })
    .when("/tukhoabythanhvien/:id", {
        templateUrl: "/app/views/tu-khoa-by-thanh-vien.html",
        controller: "tuKhoaByTvCtrl"
    }).otherwise({
        redirectTo: "/home"
    });

    $locationProvider.hashPrefix([""]);
});

app.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
});