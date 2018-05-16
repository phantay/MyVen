var app = angular.module('venApp');

(function (app) {
    'use strict';

    app.controller('banTinCtrl', function ($scope, $location, banTinService, thanhVienService, akFileUploaderService) {
        $scope.dsBanTin = [];
        $scope.TuKhoa;
        $scope.imageFile;
        $scope.thanhVien = {
            Id: "",
            FirstName: "",
            LastName: ""
        };

        thanhVienService.getThanhVien().then(function (response) {
            $scope.thanhVien.Id = response.data.ThanhVienId;
            $scope.thanhVien.FirstName = response.data.FirstName;
            $scope.thanhVien.LastName = response.data.LastName;

            //
            banTinService.getDanhSachBanTin($scope.thanhVien.Id).then(function (response) {
                $scope.dsBanTin = response.data;
            });
        });

        $scope.dangTin = function () {
            // Dang Tin
            var model = {
                NoiDung: $scope.noiDungBanTin,
                NoiDungTuKhoa: $scope.noiDungTuKhoa,
                attachment: $scope.imageFile
            };
            akFileUploaderService.saveModel(model, "/BanTin/DangTin").then(function (data) {
                banTinService.getDanhSachBanTin($scope.thanhVien.Id).then(function (response) {
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
                fuzzy = Math.floor(delta / day) + ' ngày trước';
            }
            else if (delta < month) {
                fuzzy = Math.floor(delta / week) + ' tuần trước';
            }
            else if (delta < year) {
                fuzzy = Math.floor(delta / month) + ' tháng trước';
            }
            else {
                fuzzy = Math.floor(delta / year) + 'năm trước';
            }
            return fuzzy;
        }
    });
})(app);