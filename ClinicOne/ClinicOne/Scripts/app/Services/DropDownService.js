angular.module('dropDownServiceModule', []).
 factory('dropDownSvc', function ($http, $q) {


     var svc = {

         getRecordTypes: getRecordTypes,
         getDiagnosis: getDiagnosis,
         getMedications: getMedications,
         getRecordsCategories: getRecordsCategories,
         getLabRecordTypes: getLabRecordTypes
     };

     return svc;

     function getRecordTypes(classId) {

         // create deferred object using $q
         var deferred = $q.defer();

         // get posts form backend
         $http.get('/RecordTypes/getRecordTypes?classId=' + classId + '')
                   .then(function (result) {

                       deferred.resolve(result.data);
                   }, function (error) {

                       deferred.reject(error);
                   });


         return deferred.promise;

     };

     function getDiagnosis() {

         // create deferred object using $q
         var deferred = $q.defer();

         // get posts form backend
         $http.get('/Diagnosis/getDiagnosis')
                   .then(function (result) {

                       deferred.resolve(result.data);
                   }, function (error) {

                       deferred.reject(error);
                   });


         return deferred.promise;

     };

     function getMedications() {

         // create deferred object using $q
         var deferred = $q.defer();

         // get posts form backend
         $http.get('/Medications/getMedications')
                   .then(function (result) {

                       deferred.resolve(result.data);
                   }, function (error) {

                       deferred.reject(error);
                   });


         return deferred.promise;

     };

     function getRecordsCategories(classId) {

         // create deferred object using $q
         var deferred = $q.defer();

         // get posts form backend
         $http.get('/RecordTypes/getRecordCategories?ClassId=' + classId + '')
                   .then(function (result) {

                       deferred.resolve(result.data);
                   }, function (error) {

                       deferred.reject(error);
                   });


         return deferred.promise;

     };


     function getLabRecordTypes(categoryId) {

         // create deferred object using $q
         var deferred = $q.defer();

         // get posts form backend
         $http.get('/RecordTypes/getLabRecordTypes?CategoryId=' + categoryId + '')
                   .then(function (result) {

                       deferred.resolve(result.data);
                   }, function (error) {

                       deferred.reject(error);
                   });


         return deferred.promise;

     };

 })