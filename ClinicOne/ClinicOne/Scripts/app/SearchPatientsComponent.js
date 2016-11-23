var searchPatientsComponent = angular.module('searchPatientsComponent', ['smart-table']);

searchPatientsComponent.component('searchPatient', {
    templateUrl: '/wwwroot/src/SearchPatientTemplate.html',
    bindings: {
        patient: '=',
    },
    controller: function MyController($http) {

        var $ctrl = this;

        function ToJavaScriptDate(value) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            var dt = new Date(parseFloat(results[1]));
            return dt;
        }

        $ctrl.selectPatient = function (row)
        {
            $ctrl.patient = row;
            $('#searchPatientModal').modal('toggle');
        
        }


        //GET PATIENTSLIST


        $ctrl.patientsList = [];
        $ctrl.patientsListDisplay = [].concat($ctrl.patientsList);

        $http({
            method: 'GET',
            url: '/Patients/getPatients'
        }).success(function (data) {


            angular.forEach(data, function (resdata) {


                var BirthDate = new Date(ToJavaScriptDate(resdata.BirthDate))
                resdata.BirthDate = BirthDate;


            });


            $ctrl.patientsList = data;

        }, function errorCallBack(data) {

        });
                
  
    }
});

