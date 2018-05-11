(function () {
    'use strict';

    var app = angular.module('app', []);

    app.controller('dangKyCtrl', function ($scope, dangKyService) {
        $scope.submitText = "Save";
        $scope.submitted = false;
        $scope.message = '';
        $scope.isFormValid = false;

        $scope.ThanhVien = {
            FirstName: '',
            LastName: '',
            Email: '',
            Mobile: '',
            Password: '',
            ConfirmPassword: '',
            NgaySinh: '',
            GioiTinh: ''
        };

        $scope.$watch('f1.$valid', function (newValue) {
            $scope.isFormValid = newValue;
        });

        //lưu dữ liệu
        $scope.SaveData = function (data) {
            if ($scope.submitText == 'Save') {
                $scope.submitted = true;
                $scope.message = '';
                if ($scope.isFormValid) {
                    $scope.submitText = 'Please wait...';
                    $scope.ThanhVien = data;
                    dangKyService.SaveFormData($scope.ThanhVien).then(function (d)){
                        alert(d);
                        if (d == 'Success') {
                            ClearForm();
                        }
                        $scope.submitText = "Save";
                    };
                }
                else {
                    $scope.message = '';
                }
            }
        }
        function ClearForm() {
            $scope.ThanhVien = {};
            $scope.f1.$setPristine();
            $scope.submitted = false;
        }
    });
    app.factory('dangKyService', function ($http, $q) {
        var fac = {};
        fac.SaveFormData = function (data) {
            var defer = $q.defer();
            $http({
                url: '/Home/DangKy',
                method: 'POST',
                data: JSON.stringify(data),
                headers: { 'content-type': 'application/json' }
            }).success(function (d) {
                // Success callback
                defer.resolve(d);
            }).error(function (e) {
                //Failed Callback
                alert('Error!');
                defer.reject(e);
            };
            return defer.promise;
        }
        return fac;
    });
})();   