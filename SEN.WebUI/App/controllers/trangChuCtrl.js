'use strict';
var app = angular.module('venApp');

app.controller('trangChuCtrl', function ($scope, $location, thanhVienService, banTinService) {
    $scope.dsTuKhoa = [];
    $scope.dsTuKhoaByThanhVien = [];
    $scope.thanhVien = {};

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

        $scope.thanhVien.Id = response.data.ThanhVien.ThanhVienId;
        $scope.thanhVien.FirstName = response.data.ThanhVien.FirstName;
        $scope.thanhVien.LastName = response.data.ThanhVien.LastName;

        banTinService.getTuKhoaByThanhVien($scope.thanhVien.Id).then(function (response) {
            $scope.dsTuKhoaByThanhVien = response.data;
        });
    });

    banTinService.getTopTuKhoa().then(function (response) {
        $scope.dsTuKhoa = response.data;
    });
});
