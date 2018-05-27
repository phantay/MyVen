var app = angular.module('venApp');

(function (app) {
    'use strict';
    app.controller('tuKhoaCtrl', function ($scope, $routeParams, banTinService) {
        $scope.tuKhoaId = $routeParams.id;
        $scope.tuKhoa = {};

        banTinService.getTuKhoaChiTiet($scope.tuKhoaId).then(function (response) {
            $scope.tuKhoa = response.data;
        });
    });
})(app);