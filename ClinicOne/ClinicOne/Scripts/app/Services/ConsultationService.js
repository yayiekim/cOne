angular.module('consulatationServiceModule', []).
 factory('consultationSvc', function ($http, $q, $filter) {

     var svc = {
         
         addConsultation: addConsultation,
         editConsultation: editConsultation,
         getConsultation: getConsultation,
         deleteConsultation: deleteConsultation,
         getConsultationChildren: getConsultationChildren,
         getLab: getLab,
         addLab: addLab,
         editLab: editLab,
         deleteLab: deleteLab,
         addRecord: addRecord,
         editRecord: editRecord,
         deleteRecord: deleteRecord,
         addDiagnosis: addDiagnosis,
         editDiagnosis: editDiagnosis,
         deleteDiagnosis: deleteDiagnosis,
         addMedication: addMedication,
         editMedication: editMedication,
         deleteMedication: deleteMedication,
         updateCommon: updateCommon
     };

     return svc;

     function updateCommon(consultation) {
         
         return $http({
             method: 'POST',
             url: '/Common/updateCommon',
             data: $.param({ cmn: consultation }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         }).then(function (result) {

             return result;

         }, function (error) {

             return error;
         });
     };

     
     function addConsultation(consultation) {


         consultation.TransactionDate = $filter('date')(consultation.TransactionDate, "yyyy-MM-dd HH:mm:ss");

         return $http({
             method: 'POST',
             url: '/Consultation/AddConsultation',
             data: $.param({ consultation: consultation }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         }).then(function (result) {
             
             consultation.Id = result.data.Id;
             consultation.TransactionDate = result.data.TransactionDate

             return consultation;

         }, function (error) {

             return error;
         });
     };

     function editConsultation(consultation) {

         consultation.TransactionDate = $filter('date')(consultation.TransactionDate, "yyyy-MM-dd HH:mm:ss");

         return $http({
             method: 'POST',
             url: '/Consultation/EditConsultation',
             data: $.param({ consultation: consultation }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         }).then(function (result) {
 

             consultation.TransactionDate = new Date(ToJavaScriptDate(result.data));
             return consultation;

         }, function (error) {

             return error;

         });

     };

     function deleteConsultation(consultation) {

        return $http({
             method: 'GET',
             url: '/Consultation/DeleteConsultation?id=' + consultation.Id + ''

         }).then(function (result) {

             return result.data;

         }, function (error) {

             return error;
         });


     };


     function getConsultation(patientId) {

         return $http({

             method: 'GET',
             url: '/Consultation/getConsultaions?PatientId=' + patientId + ''

         });

     };

     function getConsultationChildren(consultationId) {

         var deferred = $q.defer();

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

         $http.get('/Consultation/GetLab?ConsultationId=' + consultationId + '&RecordCategory=' + recordCategoryName + '')
           .then(function (result) {

               deferred.resolve(result.data);
           }, function (error) {

               deferred.reject(error);
           });


         return deferred.promise;

     };

     //Lab

     function addLab(labs) {

         return $http({
             method: 'POST',
             url: '/Consultation/AddLab',
             data: $.param({ labs: labs }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         }).then(function (result) {

             return result.data;

         }, function (error) {

             return error;
         });

     };


     function editLab(labs) {


         return $http({
             method: 'POST',
             url: '/Consultation/EditLab',
             data: $.param({ labs: labs }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         }).then(function (result) {

             return result.data;

         }, function (error) {

             return error;

         });



     };

     function deleteLab(categoryName, consultationId) {

         return $http.get('/Consultation/DeleteLab?ConsultationId=' + consultationId + '&CategoryName=' + categoryName + '')
            .then(function (result) {

                return result.data;

            }, function (error) {

                return error;
            });


     };



     //Record
     function addRecord(record) {

         return $http({
             method: 'POST',
             url: '/Consultation/AddRecord',
             data: $.param({ record: record }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         }).then(function (result) {
             record.Id = result.data;
             return record;

         }, function (error) {


         });





     };



     function editRecord(record) {

         return $http({
             method: 'POST',
             url: '/Consultation/EditRecord',
             data: $.param({ record: record }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         }).then(function (result) {

             return result.data;

         }, function (error) {

             return error;
         });
     };

     function deleteRecord(id) {

         return $http.get('/Consultation/DeleteRecord?id=' + id + '')
            .then(function (result) {

                return id;

            }, function (error) {

                return error;
            });


     };



     //Diagnosis

     function addDiagnosis(diagnosis) {


         return $http({
             method: 'POST',
             url: '/Consultation/AddDiagnosis',
             data: $.param({ diagnosis: diagnosis }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         }).then(function (result) {
             diagnosis.Id = result.data;
             return diagnosis;

         }, function (error) {


         });


     };

     function editDiagnosis(diagnosis) {
         
         return $http({
             method: 'POST',
             url: '/Consultation/EditDiagnosis',
             data: $.param({ diagnosis: diagnosis }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         }).then(function (result) {

             return result.data;

         }, function (error) {

             return error;
         });

     };

     function deleteDiagnosis(id) {

         return $http.get('/Consultation/DeleteDiagnosis?id=' + id + '')
            .then(function (result) {

                return id;

            }, function (error) {

                return error;
            });

     };



     //Medications

     function addMedication(medication) {

        
         return $http({
             method: 'POST',
             url: '/Consultation/AddPrescribeMed',
             data: $.param({ medication: medication }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         }).then(function (result) {
             medication.Id = result.data;
             return medication;

         }, function (error) {


         });


     };

     function editMedication(medication) {

         
         return $http({
             method: 'POST',
             url: '/Consultation/EditPrescribeMed',
             data: $.param({ medication: medication }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

         }).then(function (result) {

             return result.data;

         }, function (error) {

             return error;
         });


     };

     function deleteMedication(id) {

        return $http.get('/Consultation/DeletePrescribeMed?id=' + id + '')
        .then(function (result) {

            return id;

        }, function (error) {

            return error;
        });


     };




 })