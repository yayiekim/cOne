var newPatientRecordComponent = angular.module('newPatientRecordComponent', ['smart-table', 'dropDownServiceModule', 'ui.select']);

newPatientRecordComponent.component('newPatientRecord', {
    templateUrl: '/Static/NewPatientRecordTemplate.html',
    bindings: {
        records: '='
    },
    controller:  myComponentCtrl 
});


myComponentCtrl.$inject = ['dropDownSvc'];

function myComponentCtrl(dropDownSvc) {
    
    var $ctrl = this;

    $ctrl.selectedRecord;
    $ctrl.mode;


    $ctrl.records = [];

    $ctrl.recordTypesListDropDown = [];
    $ctrl.recordListDisplay = [].concat($ctrl.records);


    dropDownSvc.getRecordTypes(2).then(function (data) {

        $ctrl.recordTypesListDropDown = data;

    });


    $ctrl.record = {
        ConsultationId: '',
        RecordType: '',
        RecordValue: ''
    };


    $ctrl.showRecordsModalAdd = function () {
        $ctrl.mode = 'add';
        $ctrl.clearRecord();
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

        var index = $ctrl.records.indexOf(row);

        $ctrl.records.splice(index, 1);



    };

    $ctrl.updateRecord = function () {

        if ($ctrl.mode == 'add') {


            $ctrl.records.push($ctrl.record);
        }
        else {

         
           
        }


        $ctrl.clearRecord();

        $('#ConsultationRecordModal').modal('toggle');

    };



    $ctrl.clearRecord = function () {

        $ctrl.record = {

            ConsultationId: '',
            RecordType: '',
            RecordValue: ''
        };

    };
       
  
    

};
