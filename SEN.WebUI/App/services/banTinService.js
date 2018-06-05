(function () {
    'use strict';

    var app = angular.module('venApp');

    app.service('banTinService', function ($http) {
        this.getDanhSachBanTin = function (thanhVienId) {
            return $http.get("/BanTin/ListBanTin?thanhVienId=" + thanhVienId);
        }

        this.dangTin = function (banTin) {
            return $http.post("/BanTin/DangTin", banTin);
        }

        this.xoaTin = function (banTin) {
            return $http.delete("/BanTin/XoaTin?banTinId=" + banTin.BanTinId)
        }

        this.getTuKhoaChiTiet = function(tuKhoaId) {
            return $http.get("/BanTin/GetTuKhoaChiTiet?tuKhoaId=" + tuKhoaId);
        }

        this.getTopTuKhoa = function () {
            return $http.get("/BanTin/GetTopTuKhoa");
        }

        this.getTuKhoaByThanhVien = function (thanhVienId) {
            return $http.get("/BanTin/GetTuKhoaByThanhVienId" + thanhVienId);
        }
    });
})();
