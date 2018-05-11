var app = angular.module('venApp');

(function (app) {
    'use strict';

    app.controller('binhLuanCtrl', function ($scope, $location, binhLuanService) {
        $scope.thanhVienId;
        $scope.banTinId;
        $scope.dsBinhLuan = [];
        $scope.binhLuanMoi;

        $scope.Init = function (banTinId, thanhVienId) {
            $scope.banTinId = banTinId;
            $scope.thanhVienId = thanhVienId;
        }

        //mo modal
        $scope.modalData = { BanTinId: "", NoiDung: "", TenNguoiDang: "" };
        $scope.open_modal = function (bt) {
            this.modalData.BanTinId = bt.BanTinId;
            this.modalData.NoiDung = bt.NoiDung;
            this.modalData.TenNguoiDang = bt.ThanhVienId;
            $scope.getTopBinhLuanMoiNhat();
        };

        $scope.dangBinhLuan = function () {
            if (!$scope.banTinId || !$scope.thanhVienId || !$scope.binhLuanMoi) {
                return;
            }
            binhLuanService.dangBinhLuan($scope.banTinId, $scope.thanhVienId, $scope.binhLuanMoi).then(function (response) {
                if (response.data.success != undefined && response.data.success == true) {
                    $scope.getTopBinhLuanMoiNhat();
                }
            });
            $scope.binhLuanMoi = '';
        }

        $scope.getTopBinhLuanMoiNhat = function () {
            binhLuanService.getTopBinhLuanMoiNhat($scope.banTinId).then(function (response) {
                console.log(response);
                $scope.dsBinhLuan = response.data;
            });
        }

        //
        $scope.loadMoreBinhLuan = function () {
            var minBinhLuanId = $scope.dsBinhLuan[$scope.dsBinhLuan.length - 1].BinhLuanId;
            binhLuanService.getMoreBinhLuan($scope.banTinId, minBinhLuanId).then(function (response) {
                console.log(response);
                $scope.dsBinhLuan = [].concat($scope.dsBinhLuan, response.data);
            });
        }
    });
})(app);