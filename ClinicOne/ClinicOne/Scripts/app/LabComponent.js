var labComponent = angular.module('labComponent', ['smart-table', 'dropDownServiceModule', 'ui.select']);

labComponent.component('lab', {
    templateUrl: '/Static/labTemplate.html',
    bindings: {
        labSummaries: '='
    },
    controller: myComponentCtrl
});


myComponentCtrl.$inject = ['dropDownSvc'];

function myComponentCtrl(dropDownSvc) {

    var $ctrl = this;

    $ctrl.initialLabCategories = [];


    $ctrl.selectedLabCategory;
    $ctrl.mode;

    $ctrl.selectedConsultation;
    $ctrl.labSummaries = [];
    $ctrl.selectedLabSummary;
    $ctrl.labs = [];

    $ctrl.lab = {

        ConsultationId : '',
        Remarks : '',
        RecordType:'',
        RecordValue:'',
        InputType: ''
        
    };


    $ctrl.labSummary = {

        Category: '',
        Remarks: '',
      
    };



    $ctrl.labSummariesListDisplay = [].concat($ctrl.labSummaries);
    
    $ctrl.labsDisplay = [].concat($ctrl.labs);


    $ctrl.showInitialAdd = function ()
    {
        dropDownSvc.getRecordsCategories(2).then(function (data) {

            $ctrl.initialLabCategories = data;

        });

     

        $('#labInitialaddModal').modal('toggle');
        
    };
    
    $ctrl.showLabAddModal = function () {
        
        $ctrl.mode = 'add';

        $('#labInitialaddModal').modal('toggle');
        $('#ConsultationLabModal').modal('toggle');


      
        dropDownSvc.getLabRecordTypes($ctrl.selectedLabCategory.Id).then(function (data) {

          
            angular.forEach(data, function (value, key) {
              
                $ctrl.lab = {

                    ConsultationId: '',
                    Remarks: '',
                    RecordType: value.RecordTypeName,
                    RecordValue: '',
                    InputType: value.ValueTypeName

                };

                $ctrl.labs.push($ctrl.lab);


            });

        });


    };


    $ctrl.showLabEditModal = function () {

        $ctrl.mode = 'edit';
            
        $('#ConsultationLabModal').modal('toggle');


    };




    $ctrl.deletePropertyValue = function (property)
    {
        property.RecordValue = '';

    }

    $ctrl.updateLab = function () {


        if ($ctrl.mode == 'add') {

            $ctrl.labSummary = {

                Category: $ctrl.selectedLabCategory.Category,
                Remarks: '',

            };

            $ctrl.labSummaries.push($ctrl.labSummary);
         
        }
        else {



        }

        $('#ConsultationLabModal').modal('toggle');
       


    };

};
