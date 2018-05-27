'use strict';
var app = angular.module('venApp');

app.controller('trangChuCtrl', function ($scope, $location, thanhVienService, banTinService) {
    $scope.dsTuKhoa = [];
    $scope.thanhVien = {};

    thanhVienService.getThanhVien().then(function (response) {
        $scope.thanhVien.Id = response.data.ThanhVienId;
        $scope.thanhVien.FirstName = response.data.FirstName;
        $scope.thanhVien.LastName = response.data.LastName;
    });

    banTinService.getTopTuKhoa().then(function (response) {
        $scope.dsTuKhoa = response.data;
    });
});
