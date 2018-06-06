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

        thanhVienService.getThanhVien().then(function (response) {
            if (response.data.StatusCode == 403) {
                alert("Đã hết session, vui lòng đăng nhập lại!");
                window.location = "/Home/DangNhap";
                return;
            }
            else if (response.data.StatusCode == 500) {
                alert("Đã có lỗi xảy ra, vui lòng thử lại sau!<br>Thông điệp lỗi: " + response.data.Message);
                return;
            }

            banTinService.getTuKhoaByThanhVien(response.data.ThanhVien.ThanhVienId).then(function (response) {
                $scope.tuKhoabythanhvien = response.data;
            });
        });
    });
})(app);