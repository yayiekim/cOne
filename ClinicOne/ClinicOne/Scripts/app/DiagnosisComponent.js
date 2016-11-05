var diagnosisComponent = angular.module('diagnosisComponent', ['smart-table', 'dropDownServiceModule', 'ui.select']);

diagnosisComponent.component('diagnosis', {
    templateUrl: '/Static/DiagnosisTemplate.html',
    bindings: {
        diagnosisList: '='
    },
    controller: myComponentCtrl
});


myComponentCtrl.$inject = ['dropDownSvc'];

function myComponentCtrl(dropDownSvc) {

    var $ctrl = this;

    $ctrl.selectedDiagnosis;
    $ctrl.mode;
       

    $ctrl.diagnosisListDropDown = [];
    $ctrl.diagnosisListDisplay = [].concat($ctrl.diagnosisList);


    dropDownSvc.getDiagnosis().then(function (data) {

        $ctrl.diagnosisListDropDown = data;

    });

    

    $ctrl.diagnosis = {
        ConsultationId: '',
        Remarks: '',
        Diagnosis: '',
    };


    $ctrl.showDialogModalAdd = function () {
        $ctrl.mode = 'add';
        $ctrl.clearDiagnosis();
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

        var index = $ctrl.diagnosisList.indexOf(row);

        $ctrl.diagnosisList.splice(index, 1);



    };

    $ctrl.updateDiagnosis = function () {


        $ctrl.diagnosisList = [];
        if ($ctrl.mode == 'add') {

            $ctrl.diagnosisList.push($ctrl.diagnosis);
        }
        else {



        }


        $ctrl.clearDiagnosis();

        $('#ConsultationDiagnosisModal').modal('toggle');

    };



    $ctrl.clearDiagnosis = function () {

        $ctrl.diagnosis = {

            ConsultationId: '',
            Remarks: '',
            Diagnosis: '',
        };

    };




};
