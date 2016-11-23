var userRolesComponent = angular.module('userRolesComponent', []);

userRolesComponent.component('userRoles', {
    templateUrl: '/wwwroot/src/UserRolesTemplate.html',
    controller: myComponentCtrl
});


myComponentCtrl.$inject = ['adminSvc'];

function myComponentCtrl(adminSvc) {

    var $ctrl = this;

    $ctrl.users = [];
    $ctrl.userRoles = [];
    $ctrl.roles = [];


    adminSvc.getUsers().then(function (data) {

        $ctrl.users = data;

    });

 
    $ctrl.getUserRoles = function (row) {
                

        adminSvc.getUsersRoles(row.Username).then(function (data) {

            console.log(data);

            $ctrl.userRoles = data;

        });
    };


};
