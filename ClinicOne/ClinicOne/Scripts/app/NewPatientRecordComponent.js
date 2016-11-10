var newPatientRecordComponent = angular.module('newPatientRecordComponent', ['smart-table', 'ui.select']);

newPatientRecordComponent.component('newPatientRecord', {
    templateUrl: '/Static/NewPatientRecordTemplate.html',
    bindings: {
        records: '=',
        selectedConsultationId: '=',
        disableButton: '='
    },
    controller: myComponentCtrl
});


myComponentCtrl.$inject = ['dropDownSvc', 'consultationSvc'];

function myComponentCtrl(dropDownSvc, consultationSvc) {

    var $ctrl = this;

    $ctrl.selectedRecord;
    $ctrl.mode;


    $ctrl.records = [];

    $ctrl.recordTypesListDropDown = [];
    $ctrl.recordListDisplay = [].concat($ctrl.records);


    dropDownSvc.getRecordTypes(1).then(function (data) {

        $ctrl.recordTypesListDropDown = data;

    });


    $ctrl.record = {
        Id: '',
        ConsultationId: '',
        RecordTypeName: '',
        RecordValue: ''
    };


    $ctrl.showRecordsModalAdd = function () {
        $ctrl.mode = 'add';
        $('#ConsultationRecordModal').modal('toggle');

    };

    $ctrl.showRecordsModalEdit = function (record) {
        $ctrl.mode = 'edit';
        $ctrl.record = record;
        $('#ConsultationRecordModal').modal('toggle');

    };

    $ctrl.showDeleteModal = function (row) {
        $ctrl.selectedRecord = row;
        $('#deleteModalRecord').modal('toggle');

    };




    $ctrl.deleteRecord = function (row) {

        consultationSvc.deleteRecord(row.Id).then(function (data) {

            var index = $ctrl.records.indexOf(data);
            $ctrl.records.splice(index, 1);


        });
        
                

    };


    $ctrl.updateRecord = function () {

        $ctrl.record.ConsultationId = $ctrl.selectedConsultationId;
        
        if ($ctrl.mode == 'add') {

            consultationSvc.addRecord($ctrl.record).then(function (data) {
               
               
                $ctrl.records.push(data);

            });

        }
        else {

            consultationSvc.editRecord($ctrl.record).then(function (data) {

            });

        }


        $ctrl.clearRecord();

        $('#ConsultationRecordModal').modal('toggle');

    };



    $ctrl.clearRecord = function () {

        $ctrl.record = {
            Id: '',
            ConsultationId: '',
            RecordTypeName: '',
            RecordValue: ''
        };

    };




};
