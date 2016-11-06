angular.module('consulatationServiceModule', []).
 factory('consultationSvc', function ($http, $q) {


     var svc = {

         submitInitialConsultation: submitInitialConsultation,
         submitConsultation: submitConsultation,
         getConsultation: getConsultation,
         deleteConsultation: deleteConsultation,
         getConsultationChildren: getConsultationChildren,
         getLab: getLab,
         addLab: addLab
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

                 // create deferred object using $q
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

         // create deferred object using $q
         var deferred = $q.defer();

         // get posts form backend
         $http.get('/Consultation/GetLab?ConsultaionId=' + consultationId + '&RecordCategory=' + recordCategoryName + '')
           .then(function (result) {

               deferred.resolve(result.data);
           }, function (error) {

               deferred.reject(error);
           });


         return deferred.promise;

     };



     function addLab(lab) {

         return $http({
             method: 'POST',
             url: '/Consultation/AddLab',
             data: $.param({ labs: lab }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         });
     };





 })