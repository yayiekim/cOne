angular.module('consulatationServiceModule', []).
 factory('consultationSvc', function ($http, $q) {



     var svc = {

         submitInitialConsultation: submitInitialConsultation,
         submitConsultation: submitConsultation,
         getConsultation: getConsultation,
         deleteConsultation: deleteConsultation,
         getConsultationChildren: getConsultationChildren,
         getLab: getLab,
         addLab: addLab,
         editLab: editLab,
         deleteLab: deleteLab
     };

     return svc;

     function submitInitialConsultation(consultation) {
         return $http({
             method: 'POST',
             url: '/Consultation/AddConsultation',
             data: $.param({ consultaion: consultation }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         }).success(function (data) {


         });
     };

     function submitConsultation(consultation) {

         return $http({
             method: 'POST',
             url: '/Consultation/AddConsultation',
             data: $.param({ consultaion: consultation }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         });
     };

     function deleteConsultation(consultationId) {

         $http({
             method: 'GET',
             url: '/Consultation/DeleteConsultation?id=' + consultationId + ''

         }).success(function (data) {

         });
     };


     function getConsultation(patientId) {

         return $http({

             method: 'GET',
             url: '/Consultation/getConsultaions?PatientId=' + patientId + ''

         })

     };

     function getConsultationChildren(consultationId) {
         var deferred = $q.defer();
         // get posts form backend
         $http.get('/Consultation/getConsultaionDetail?ConsultationId=' + consultationId + '')
           .then(function (result) {

               deferred.resolve(result.data);
           }, function (error) {

               deferred.reject(error);
           });


         return deferred.promise;

     };

     function getLab(consultationId, recordCategoryName) {
         var deferred = $q.defer();
         // get posts form backend
         $http.get('/Consultation/GetLab?ConsultationId=' + consultationId + '&RecordCategory=' + recordCategoryName + '')
           .then(function (result) {

               deferred.resolve(result.data);
           }, function (error) {

               deferred.reject(error);
           });


         return deferred.promise;

     };



     function addLab(labs) {
         var deferred = $q.defer();
         return $http({
             method: 'POST',
             url: '/Consultation/AddLab',
             data: $.param({ labs: labs }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         });

         return deferred.promise;

     };


     function editLab(labs) {


         return $http({
             method: 'POST',
             url: '/Consultation/EditLab',
             data: $.param({ labs: labs }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         });

         return deferred.promise;
     };

     function deleteLab(categoryName, consultationId) {

         console.log(categoryName);
         console.log(consultationId);


         var deferred = $q.defer();
         // get posts form backend
         $http.get('/Consultation/DeleteLab?ConsultationId=' + consultationId + '&CategoryName=' + categoryName + '')
           .then(function (result) {

               deferred.resolve(result.data);
           }, function (error) {

               deferred.reject(error);
           });

         return deferred.promise;

     };

 })