var labComponent = angular.module('labComponent', ['smart-table', 'dropDownServiceModule', 'consulatationServiceModule', 'ui.select']);

labComponent.component('lab', {
    templateUrl: '/Static/labTemplate.html',
    bindings: {
        labSummaries: '=',
        selectedConsultationId: '='
    },
    controller: myComponentCtrl
});


myComponentCtrl.$inject = ['dropDownSvc', 'consultationSvc'];

function myComponentCtrl(dropDownSvc, consultationSvc) {

    var $ctrl = this;

    $ctrl.initialLabCategories = [];

    $ctrl.selectedConsultationId;
    $ctrl.selectedLabCategory;
    $ctrl.mode;

    $ctrl.selectedConsultation;
    $ctrl.labSummaries = [];
    $ctrl.selectedLabSummary;
    $ctrl.labs = [];

    $ctrl.PatientLabModel = {
       
        Id: '',
        ConsultationId : '',
        Remarks : '',
        RecordTypeName: '',
        RecordValue:'',
        InputType: '',
        RecordCategoryName :''
        
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


        $ctrl.clearlabs();
        dropDownSvc.getLabRecordTypes($ctrl.selectedLabCategory.Id).then(function (data) {

          
            angular.forEach(data, function (value, key) {
              
                $ctrl.PatientLabModel = {
                    
                    ConsultationId: $ctrl.selectedConsultationId,
                    Remarks: '',
                    RecordTypeName: value.RecordTypeName,
                    RecordValue: '',
                    InputType: value.ValueTypeName,
                    RecordCategoryName: $ctrl.selectedLabCategory.Category

                };

                $ctrl.labs.push($ctrl.PatientLabModel);


            });

        });


    };


    $ctrl.showLabEditModal = function (row) {

        $ctrl.mode = 'edit';
            
        consultationSvc.getLab(row.ConsultationId, row.RecordCategoryName).then(function (data) {

            $ctrl.labs = data;

        });

        
        $('#ConsultationLabModal').modal('toggle');


    };

    $ctrl.showDeleteModal = function (row) {
        $ctrl.selectedLabSummary = row;
        $('#deleteModalLab').modal('toggle');

    };

    $ctrl.deleteLabSummary = function ()
    {
       
        consultationSvc.deleteLab($ctrl.selectedLabSummary.RecordCategoryName, $ctrl.selectedConsultationId).then(function (data) {


            var index = $ctrl.labSummaries.indexOf($ctrl.selectedLabSummary);

            $ctrl.labSummaries.splice(index, 1);


        });

    };

    $ctrl.deletePropertyValue = function (property)
    {
        property.RecordValue = '';

    }

    $ctrl.updateLab = function () {
             


        if ($ctrl.mode == 'add') {
        
            consultationSvc.addLab($ctrl.labs).then(function (data) {


                $ctrl.labSummary = {

                    ConsultationId: $ctrl.selectedConsultationId,
                    RecordCategoryName: $ctrl.selectedLabCategory.Category,
                    Remarks : ''
                    

                };

                $ctrl.labSummaries.push($ctrl.labSummary);

            });
                     
         
        }
        else {


            consultationSvc.editLab($ctrl.labs).then(function (data) {

                
            });

        }



        $ctrl.clearlabs(); 
        $('#ConsultationLabModal').modal('toggle');
       


    };


    $ctrl.clearlabs = function () {
        
        $ctrl.labs = [];


    };

};
