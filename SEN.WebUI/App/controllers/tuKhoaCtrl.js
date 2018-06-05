var app = angular.module('venApp');

(function (app) {
    'use strict';
    app.controller('tuKhoaCtrl', function ($scope, $routeParams, banTinService) {
        $scope.tuKhoaId = $routeParams.id;
        $scope.tuKhoa = {};
        $scope.tuKhoabythanhvien = {};

        banTinService.getTuKhoaChiTiet($scope.tuKhoaId).then(function (response) {
            $scope.tuKhoa = response.data;
        });

        banTinService.getTuKhoaByThanhVien($scope.thanhVienId).then(function (response) {
            $scope.tuKhoabythanhvien = response.data;
        });
    });
})(app);