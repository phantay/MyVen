var app = angular.module('venApp');

(function (app) {
    'use strict';

    app.controller('binhLuanCtrl', function ($scope, $location, $window, binhLuanService) {
        $scope.thanhVienId;
        $scope.banTinId;
        $scope.dsBinhLuan = null;
        $scope.binhLuanMoi;
        $scope.anXemThem = true;

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

            var input = $window.document.getElementById('inlineComment_' + bt.BanTinId);
            if (input) {
                input.focus();
            }
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
                $scope.dsBinhLuan = response.data;

                if ($scope.dsBinhLuan.length == 5) {
                    $scope.anXemThem = false;
                }
            });
        }

        $scope.loadMoreBinhLuan = function () {
            var minBinhLuanId = $scope.dsBinhLuan[$scope.dsBinhLuan.length - 1].BinhLuanId;
            binhLuanService.getMoreBinhLuan($scope.banTinId, minBinhLuanId).then(function (response) {
                $scope.dsBinhLuan = [].concat($scope.dsBinhLuan, response.data);

                if ($scope.dsBinhLuan.length >= 5 && response.data.length < 5) {
                    $scope.anXemThem = true;
                }
            });
        }
    });
})(app);