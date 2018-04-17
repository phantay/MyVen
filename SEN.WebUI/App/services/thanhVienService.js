(function () {
    'use strict';

    var app = angular.module('venApp');

    app.service('thanhVienService', function ($http) {
        this.getThanhVien = function () {
            return $http.post("/ThanhVien/GetThanhVien");
        }
    });
})();
