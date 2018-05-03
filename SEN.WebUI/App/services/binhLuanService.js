(function () {
    'use strict';

    var app = angular.module('venApp');

    app.service('binhLuanService', function ($http) {
        this.dangBinhLuan = function (banTinId, binhLuan) {
            return $http.post("/BanTin/DangBinhLuan", { "banTinId": banTinId, "binhLuan": binhLuan});
        }

        this.getDanhSanhBinhLuan = function ($scope, banTinId, pageSize) {
            $http.get("/BanTin/GetDanhSachBinhLuan?banTinId=" + banTinId + "&pageSize=" + pageSize).then(function (result) {
                $scope.dsBinhLuan = result.data;
            });
        }
    });
})();
