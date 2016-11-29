angular.module('adminServiceModule', []).
 factory('adminSvc', function ($http, $q, $filter) {

     var svc = {

         getUsers: getUsers,
         getUsersRoles: getUsersRoles,
         //getRoles: getRoles,
         //addUserRoles: addUserRoles,
         //removeUserRoles: removeUserRoles

     };

     return svc;

     function getUsers() {

         // get posts form backend
       return  $http.get('/Admin/getUsers')
                   .then(function (result) {

                       return result.data;

                   }, function (error) {

                       return error;
                   });
         
     };


     function getUsersRoles(userName) {

         // get posts form backend
         return $http.get('/Admin/getUsersInRoles?userName=' + userName + '')
                   .then(function (result) {

                       return result.data;

                   }, function (error) {

                       return error;
                   });

     };


 })