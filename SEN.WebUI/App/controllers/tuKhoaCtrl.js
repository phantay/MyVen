var app = angular.module('venApp');

(function (app) {
    'use strict';
    app.controller('tuKhoaCtrl', function ($scope, $routeParams, banTinService, thanhVienService) {
        $scope.tuKhoaId = $routeParams.id;
        $scope.tuKhoas = {};

        banTinService.getTuKhoaChiTiet($scope.tuKhoaId).then(function (response) {
            $scope.tuKhoas = response.data;
        });
    });
})(app);