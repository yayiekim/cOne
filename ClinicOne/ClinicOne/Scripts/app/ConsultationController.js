var consultationController = angular.module('consultationController', ['consultationListComponent', 'consulatationServiceModule']);

consultationController.controller('consultationCtrl', function ($scope, $http, consultationSvc, $timeout) {

    $scope.disableButton = true;
    $scope.showAddButtons = false;
    $scope.consultations;
    $scope.selectedConsultaion;
    $scope.selectedPatient;
    $scope.records =[];
    $scope.diagnosisList =[];
    $scope.medications = [];
    $scope.labSummaries = [];
    $scope.medications = [];

    var initializing = true;
    
    $scope.$watch('selectedPatient', function () {


        if (initializing) {
            $timeout(function () { initializing = false; });
        } else {

         $scope.clear()
            $scope.disableButton = true;



            consultationSvc.getConsultation($scope.selectedPatient.Id).then(function (data) {

                angular.forEach(data.data, function (resdata) {


                    resdata.TransactionDate = new Date(ToJavaScriptDate(resdata.TransactionDate))
                  


                });


               $scope.consultations = data.data;
            });


        }
        
    });


    $scope.$watch('selectedConsultaion', function () {

        if (initializing) {
            $timeout(function () { initializing = false; });
        } else {
            
            consultationSvc.getConsultationChildren($scope.selectedConsultaion.Id).then(function (data) {
                                
                $scope.records = data.RecordList;
             
                $scope.diagnosisList = data.DiagnosisList;

                $scope.labSummaries = data.LabModelList;

                $scope.medications = data.PrescribeMedicationList;

                if ($scope.selectedConsultaion != null) {
                    $scope.disableButton = false;
                } else {
                    $scope.disableButton = true;
                }

               
            });

        }

    });




    $scope.clear = function ()
    {

        $scope.records = [];

        $scope.diagnosisList = [];

        $scope.labSummaries = [];

        $scope.medications = [];

    };

    $scope.showPatients = function ()
    {
        $('#searchPatientModal').modal('toggle');

    }

  

    $scope.submit = function ()
    {

        var records = [{ConsultationId : "",
            RecordType: '4f1e64ea-2b98-e611-9d7a-a0d37a3ad21a',
            RecordValue : 1}];


        $scope.consultation = {

            PatientId: $scope.admittedPatient.Id,
            RecordList: records

        };


        consultationSvc.submitInitialConsultation($scope.consultation);

    }


    $scope.newConsultation = function (patientId)
    {

        
        $http({
            method: 'GET',
            url: '/Consultation/NewConsultation?patientId=' + patientId + ''
        }).success(function (data) {

            $scope.diagnosisTypeCategoryList = data;

        }, function errorCallBack(data) {

        });

    }




});