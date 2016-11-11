var consultationListComponent = angular.module('consultationListComponent', []);

consultationListComponent.component('consultationList', {
    templateUrl: '/Static/ConsultationList.html',
    bindings: {
        myConsultations: '<',
        selectedRow: '=',
        patient: '=',
        disableButtonConsultationList: '='

    },
    controller: MyController
});

MyController.$inject = ['consultationSvc'];



function MyController(consultationSvc) {

    var $ctrl = this;

    $ctrl.consultationListDisplay = [].concat($ctrl.myConsultations);
    $ctrl.mode;

    $ctrl.myConsultation = {

        Id: '',
        PatientId: '',
        TransactionDate : ''
     
    };


    $ctrl.showDialogModalAdd = function () {
        $ctrl.mode = 'add';

        $('#addConsultaionModal').modal('toggle');

    };

    $ctrl.showDialogModalEdit = function (consultation) {
        $ctrl.mode = 'edit';
        $ctrl.myConsultation = consultation;
        $('#addConsultaionModal').modal('toggle');

    };

    $ctrl.showDeleteModal = function (row) {
        $ctrl.myConsultation = row;
        $('#deleteConsultationModal').modal('toggle');

    };

    

    $ctrl.showPrint = function (row) {
        $ctrl.myConsultation = row;
        $('#printConsultationModal').modal('toggle');

    };

    $ctrl.deleteConsultation = function () {

        consultationSvc.deleteConsultation($ctrl.myConsultation).then(function (data) {

            var index = $ctrl.myConsultations.indexOf(data);
            $ctrl.myConsultations.splice(index, 1);
            $ctrl.clear();
        });

    }


    $ctrl.updateConsultation = function () {

        $ctrl.myConsultation.PatientId = $ctrl.patient.Id;

             

        if ($ctrl.mode == 'add') {

            consultationSvc.addConsultation($ctrl.myConsultation).then(function (data) {

                data.TransactionDate = new Date(ToJavaScriptDate(data.TransactionDate));

                $ctrl.myConsultations.push(data);
                $ctrl.selectedRow = data;
            });

        }
        else {

            consultationSvc.editConsultation($ctrl.myConsultation).then(function (data) {
            
                $ctrl.myConsultation.TransactionDate = data.TransactionDate;

            });

        }


        $ctrl.clear();

        $('#addConsultaionModal').modal('toggle');

    };

    $ctrl.setSelectedRow = function (row) {

        $ctrl.selectedRow = row;

    }


 
    $ctrl.clear = function ()
    {
        $ctrl.myConsultation = {

            Id: '',
            PatientId: '',
            TransactionDate: ''

        };



    };

    $ctrl.Print = function () {
        window.print();
    };


};