(function () {
    'use strict';

    var app = angular.module('venApp');

    app.controller('banTinCtrl', function ($scope, $location, banTinService, thanhVienService, binhLuanService, akFileUploaderService) {
        $scope.dsBanTin = [];
        $scope.dsBinhLuan = [];
        $scope.imageFile;
        $scope.binhLuanMoi;

        thanhVienService.getThanhVien().then(function (response) {
            console.log(response);
            $scope.FirstName = response.data.FirstName;
            $scope.LastName = response.data.LastName;
        });

        banTinService.getDanhSachBanTin().then(function (response) {
            $scope.dsBanTin = response.data;
        });

        //mo modal
        $scope.post = {
            name: "",
            anh: ""
        };
        $scope.modalData = { BanTinId: "", NoiDung: "", TenNguoiDang: "" };
        $scope.open_modal = function (bt) {
            this.modalData.BanTinId = bt.BanTinId;
            this.modalData.NoiDung = bt.NoiDung;
            this.modalData.TenNguoiDang = bt.BanTinId;

            binhLuanService.getDanhSanhBinhLuan($scope, bt.BanTinId, 0);

            //alert('mo modal ' + this.modalData.BanTinId + " " + this.modalData.NoiDung + " " + this.modalData.TenNguoiDang);
            $('#link-open-modal').trigger('click');
            //this.post.name = bt.BanTinId;
            //this.post.anh = bt.BanTinId;

            //$('#link-modal').trigger('click');
            //setTimeout(function () {

            //    $('.post-scroll-modal').slimscroll({
            //        size: '5px',
            //        height: $('.modal-gallery').height() - 15
            //    });
            //}, 250);
        };
        //het modal

        $scope.noiDungBanTin = "";
        $scope.noiDungBinhLuan = "";

        $scope.dangTin = function () {
            // Dang Tin
            var model = {
                NoiDung: $scope.noiDungBanTin,
                attachment: $scope.imageFile
            };

            akFileUploaderService.saveModel(model, "/BanTin/DangTin").then(function (data) {
                banTinService.getDanhSachBanTin().then(function (response) {
                    $scope.dsBanTin = response.data;
                });
            });
        };

        $scope.xoaTin = function (bantin) {
            banTinService.xoaTin(bantin).then(function (response) {
                var btIndex = $scope.dsBanTin.indexOf(bantin);
                $scope.dsBanTin.splice(btIndex, 1);
            });
        }

        $scope.chuyenGio = function (date) {            
            if (!date)
                return null;

            date = new Date(date);

            var currentDate = new Date();
            var delta = Math.abs(currentDate - date) / 1000;
            var minute = 60,
                hour = minute * 60,
                day = hour * 24,
                week = day * 7,
                month = day * 30,
                year = month * 12;
            var fuzzy;

            if (delta < 30) {
                fuzzy = 'vừa xong';
            }
            else if (delta < minute) {
                fuzzy = delta + ' giây trước';
            }
            else if (delta < hour) {
                fuzzy = Math.floor(delta / minute) + ' phút trước';
            }
            else if (delta < day) {
                fuzzy = Math.floor(delta / hour) + ' giờ trước';
            }
            else if (delta < week) {
                fuzzy = Math.floor(delta / day) + ' ngay trước';
            }
            else if (delta < month) {
                fuzzy = Math.floor(delta / week) + ' tuan trước';
            }
            else if (delta < year) {
                fuzzy = Math.floor(delta / month) + ' thang trước';
            }
            else {
                fuzzy = Math.floor(delta / year) + ' nam trước';
            }
            return fuzzy;
        }

        $scope.dangBinhLuan = function () {
            if (!$scope.modalData.BanTinId || !$scope.binhLuanMoi)
                return;
            binhLuanService.dangBinhLuan($scope.modalData.BanTinId, $scope.binhLuanMoi).then(function (binhLuan) {
                var pageSize = $scope.dsBinhLuan.length + 1;
                binhLuanService.getDanhSanhBinhLuan($scope, $scope.modalData.BanTinId, pageSize);
            });            
        }

        $scope.loadMoreBinhLuan = function () {
            var pageSize = $scope.dsBinhLuan.length + 5;
            binhLuanService.getDanhSanhBinhLuan($scope, $scope.modalData.BanTinId, pageSize);
        }

    });
})();

