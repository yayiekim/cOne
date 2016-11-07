﻿var medicationsComponent = angular.module('medicationsComponent', ['smart-table', 'dropDownServiceModule', 'ui.select']);

medicationsComponent.component('medications', {
    templateUrl: '/Static/MedicationTemplate.html',
    bindings: {
        medications: '='
    },
    controller: myComponentCtrl
});


myComponentCtrl.$inject = ['dropDownSvc'];

function myComponentCtrl(dropDownSvc) {

    var $ctrl = this;

    $ctrl.selectedMedication;
    $ctrl.mode;


    $ctrl.medications = [];

    $ctrl.medicationsListDropDown = [];

    $ctrl.medicationsListDisplay = [].concat($ctrl.medications);


    dropDownSvc.getMedications().then(function (data) {

        $ctrl.medicationsListDropDown = data;

    });


    $ctrl.medication = {
  
        Medication :'',
        ConsultationId :'',
        Quantity: 0,
        Remarks :'',
        Amount : 0,
        Strength :'',
        Volume :'',
        Frequency :'',
        Route :''
    };


    $ctrl.showDialogModalAdd = function () {
        $ctrl.mode = 'add';
        $ctrl.clearMedication();
        $('#ConsultationMedicationModal').modal('toggle');

    };

    $ctrl.showDialogModalEdit = function (medication) {
        $ctrl.mode = 'edit';
        $ctrl.medication = medication;
        $('#ConsultationMedicationModal').modal('toggle');

    };

    $ctrl.showDeleteModal = function (row) {
        $ctrl.selectedMedication = row;
        $('#ConsultationMedicationModal').modal('toggle');

    };


    $ctrl.deleteMedication = function (row) {

        var index = $ctrl.medications.indexOf(row);

        $ctrl.medications.splice(index, 1);



    };

    $ctrl.updateMedication = function () {

        if ($ctrl.mode == 'add') {


            $ctrl.medications.push($ctrl.medication);
        }
        else {



        }


        $ctrl.clearMedication();

        $('#ConsultationMedicationModal').modal('toggle');

    };



    $ctrl.clearMedication = function () {

        $ctrl.medication = {

            Medication: '',
            ConsultationId: '',
            Quantity: 0,
            Remarks: '',
            Amount: 0,
            Strength: '',
            Volume: '',
            Frequency: '',
            Route: ''
        };

    };




};