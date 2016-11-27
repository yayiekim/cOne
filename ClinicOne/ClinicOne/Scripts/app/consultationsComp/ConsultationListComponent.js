
var consultationListComponent = angular.module('consultationListComponent', []);

consultationListComponent.component('consultationList', {
    templateUrl: '/wwwroot/src/ConsultationList.html',
    bindings: {
        myConsultations: '<',
        selectedRow: '=',
        patient: '=',
        disableButtonConsultationList: '=',
        prescribeMeds: '='

    },
    controller: MyController
});

MyController.$inject = ['consultationSvc', '$uibModal'];

function MyController(consultationSvc, $uibModal) {

    var $ctrl = this;


    $ctrl.print = function () {
        window.print($('#invoice'));
    }


    $ctrl.consultationListDisplay = [].concat($ctrl.myConsultations);
    $ctrl.mode;

    $ctrl.myConsultation = {

        Id: '',
        PatientId: '',
        TransactionDate: ''

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



    $ctrl.clear = function () {
        $ctrl.myConsultation = {

            Id: '',
            PatientId: '',
            TransactionDate: ''

        };



    };


    $ctrl.showPrint = function (row) {

        $ctrl.selectedRow = row;
        $ctrl.myConsultation = row;

        var modalInstance = $uibModal.open({
            animation: true,
            component: 'modalComp',
            resolve: {

                patient: function () {
                    return $ctrl.patient;
                },


                prescribeMeds: function () {
                    return $ctrl.prescribeMeds;
                },

                myConsultations: function () {
                    return $ctrl.myConsultations;
                }

            }


        });

        modalInstance.result.then(function () {

        }, function () {

        });
    };


};



var modalComponent = angular.module('modalComponent', []);

modalComponent.component('modalComp',
{
    templateUrl: 'printModal',
    bindings: {
        resolve: '<',
        close: '&',
        dismiss: '&'
    },
    controller: function () {
        var $ctrl = this;
        
        $ctrl.$onInit = function () {
            $ctrl.patient = $ctrl.resolve.patient;
            $ctrl.prescribeMeds = $ctrl.resolve.prescribeMeds;
            $ctrl.myConsultations = $ctrl.resolve.myConsultations;
        };
        
        $ctrl.ok = function () {

           window.print();
          
        };

        $ctrl.cancel = function () {
            $ctrl.dismiss({ $value: 'cancel' });
        };
    }

});
