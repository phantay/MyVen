(function () {
    'use strict';

    var app = angular.module('venApp');

    app.service('binhLuanService', function ($http) {
        this.dangBinhLuan = function (banTinId, binhLuan) {
            return $http.post("/BanTin/DangBinhLuan", { "banTinId": banTinId, "binhLuan": binhLuan });
        }

        this.getTopBinhLuanMoiNhat = function (banTinId) {
            return $http.get("/BanTin/GetTopBinhLuanMoiNhat?banTinId=" + banTinId);
        }

        this.getBinhLuanById = function (binhLuanId) {
            return $http.get("/BanTin/GetBinhLuanById?binhLuanId=" + binhLuanId);
        }

        this.getMoreBinhLuan = function (banTinId, minBinhLuanId) {
            return $http.get("/BanTin/GetMoreBinhLuan?banTinId=" + banTinId + "&minBinhLuanId=" + minBinhLuanId);
        }
    });
})();