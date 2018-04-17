(function () {
    'use strict';

    var app = angular.module('venApp');

    app.service('binhLuanService', function ($http) {
        this.dangBinhLuan = function (banTinId, binhLuan) {
            return $http.post("/BanTin/DangBinhLuan", { "banTinId": banTinId, "binhLuan": binhLuan});
        }
        this.getDanhSanhBinhLuan = function (banTinId) {
            return $http.get("/BanTin/GetDanhSachBinhLuan?banTinId=" + banTinId);
        }
    });
})();
