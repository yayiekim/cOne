var diagnosisComponent = angular.module('diagnosisComponent', ['ui.select']);

diagnosisComponent.component('diagnosis', {
    templateUrl: '/Static/DiagnosisTemplate.html',
    bindings: {
        diagnosisList: '=',
        selectedConsultationId: '=',
        disableButton: '='

    },
    controller: myComponentCtrl
});


myComponentCtrl.$inject = ['dropDownSvc', 'consultationSvc'];

function myComponentCtrl(dropDownSvc, consultationSvc) {

    var $ctrl = this;

    $ctrl.selectedDiagnosis;
    $ctrl.mode;
       
    $ctrl.diagnosisList = [];

    $ctrl.diagnosisListDropDown = [];
    $ctrl.diagnosisListDisplay = [].concat($ctrl.diagnosisList);


    dropDownSvc.getDiagnosis().then(function (data) {

        $ctrl.diagnosisListDropDown = data;

    });

    

    $ctrl.diagnosis = {
        Id: '',
        ConsultationId: '',
        Remarks: '',
        Diagnosis: '',
    };


    $ctrl.showDialogModalAdd = function () {
        $ctrl.mode = 'add';

        $('#ConsultationDiagnosisModal').modal('toggle');

    };

    $ctrl.showDialogModalEdit = function (diagnosis) {
        $ctrl.mode = 'edit';
        $ctrl.diagnosis = diagnosis;
        $('#ConsultationDiagnosisModal').modal('toggle');

    };

    $ctrl.showDeleteModal = function (row) {
        $ctrl.selectedDiagnosis = row;
        $('#deleteModalDiagnosis').modal('toggle');

    };


    $ctrl.deleteDiagnosis = function (row) {
        
        consultationSvc.deleteDiagnosis(row.Id).then(function (data) {
            
            var index = $ctrl.diagnosisList.indexOf(data);

            $ctrl.diagnosisList.splice(index, 1);
        


        });

     

    };

    $ctrl.updateDiagnosis = function () {

        $ctrl.diagnosis.ConsultationId = $ctrl.selectedConsultationId;

        if ($ctrl.mode == 'add') {

            consultationSvc.addDiagnosis($ctrl.diagnosis).then(function (data) {

                $ctrl.diagnosisList.push(data);

            });

        }
        else {

            consultationSvc.editDiagnosis($ctrl.diagnosis).then(function (data) {

            });

        }


        $ctrl.clearDiagnosis();

        $('#ConsultationDiagnosisModal').modal('toggle');

    };



    $ctrl.clearDiagnosis = function () {

        $ctrl.diagnosis = {
            Id: '',
            ConsultationId: '',
            Remarks: '',
            Diagnosis: '',
        };

    };




};
