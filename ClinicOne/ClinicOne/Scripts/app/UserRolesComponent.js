var userRolesComponent = angular.module('userRolesComponent', []);

userRolesComponent.component('userRoles', {
    templateUrl: '/Static/UserRolesTemplate.html',
    controller: myComponentCtrl
});


myComponentCtrl.$inject = ['adminSvc'];

function myComponentCtrl(dropDownSvc, consultationSvc) {

    var $ctrl = this;

    $ctrl.users = [];
    $ctrl.userRoles = [];
    $ctrl.roles = [];


    adminSvc.getUsers().then(function (data) {

        $ctrl.user = data;

    });

    $ctrl.getUserRoles = function ()
    {
        adminSvc.getUserRoles().then(function (data) {

            $ctrl.userRoles = data;

        });

    };



};
