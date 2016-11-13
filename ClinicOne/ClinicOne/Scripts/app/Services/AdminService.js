angular.module('adminServiceModule', []).
 factory('adminSvc', function ($http, $q, $filter) {

     var svc = {

         getUsers: getusers,
         getUsersRoles: getUsersRoles,
         getRoles: getRoles,
         addUserRoles: addUserRoles,
         removeUserRoles: removeUserRoles

     };

     return svc;

     function getUsers(userName, Roles) {

         // get posts form backend
         $http.get('/Admin/getUsers')
                   .then(function (result) {

                       return result.data;

                   }, function (error) {

                       return error;
                   });



     };


     function getUserRoles(userName) {

         // get posts form backend
         $http.get('/Admin/getUserRoles')
                   .then(function (result) {

                       return result.data;

                   }, function (error) {

                       return error;
                   });



     };


 })