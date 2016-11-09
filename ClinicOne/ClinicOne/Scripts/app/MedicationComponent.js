var medicationsComponent = angular.module('medicationsComponent', ['smart-table', 'ui.select']);

medicationsComponent.component('medications', {
    templateUrl: '/Static/MedicationTemplate.html',
    bindings: {
        medications: '=',
        selectedConsultationId: '='
    },
    controller: myComponentCtrl
});


myComponentCtrl.$inject = ['dropDownSvc', 'consultationSvc'];

function myComponentCtrl(dropDownSvc, consultationSvc) {

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
  
        Id:'',
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
        $('#ConsultationMedicationModal').modal('toggle');

    };

    $ctrl.showDialogModalEdit = function (medication) {
        $ctrl.mode = 'edit';
        $ctrl.medication = medication;
        $('#ConsultationMedicationModal').modal('toggle');

    };

    $ctrl.showDeleteModal = function (row) {
        $ctrl.selectedMedication = row;
        $('#deleteModalMedication').modal('toggle');

    };


    $ctrl.deleteMedication = function (row) {


        consultationSvc.deleteMedication(row.Id).then(function (data) {

            var index = $ctrl.medications.indexOf(data);
            $ctrl.medications.splice(index, 1);


        });

     

    };

    $ctrl.updateMedication = function () {

        $ctrl.medication.ConsultationId = $ctrl.selectedConsultationId;

        if ($ctrl.mode == 'add') {

            consultationSvc.addMedication($ctrl.medication).then(function (data) {


                $ctrl.medications.push(data);

            });

        }
        else {

            consultationSvc.editMedication($ctrl.medication).then(function (data) {

            });

        };



        $ctrl.clearMedication();

        $('#ConsultationMedicationModal').modal('toggle');

    };



    $ctrl.clearMedication = function () {

        $ctrl.medication = {
            Id: '',
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
