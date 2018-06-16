var app = angular.module('venApp');

(function (app) {
    'use strict';
    app.controller('tuKhoaByTvCtrl', function ($scope, $routeParams, banTinService, thanhVienService) {
        $scope.thanhVien.Id = $routeParams.id;
        $scope.tuKhoas = {};

        banTinService.getTuKhoaByThanhVien($scope.thanhVien.Id).then(function (response) {
            $scope.tuKhoas = response.data;
        });
    });
})(app);