var searchPatientsComponent = angular.module('searchPatientsComponent', ['smart-table']);

searchPatientsComponent.component('searchPatient', {
    templateUrl: '/wwwroot/src/SearchPatientTemplate.html',
    bindings: {
        patient: '=',
    },
    controller: function MyController($http, $filter) {

        var $ctrl = this;

        $ctrl.selectedFilter;

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

        $ctrl.selectedDate = new Date();

        $ctrl.toggleFilter = function (filter)
        {
            if (filter == "all") {
                $ctrl.allPatients();

            }
            else if (filter == "waiting")
            {
                $ctrl.waitingPatients();
            }
            else if (filter == "date") {

                $ctrl.PatientByDate();
            }
        }


        $ctrl.PatientByDate = function () {

           
            $http({
                method: 'GET',
                url: '/Patients/getPatientByDate?date=' + $filter('date')($ctrl.selectedDate, "MM-dd-yyyy") + ''
            }).success(function (data) {


                angular.forEach(data, function (resdata) {


                    var BirthDate = new Date(ToJavaScriptDate(resdata.BirthDate))
                    resdata.BirthDate = BirthDate;


                });


                $ctrl.patientsList = data;

            }, function errorCallBack(data) {

            });

        }

        $ctrl.allPatients = function()
        {
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

        $ctrl.waitingPatients = function () {

            $http({
                method: 'GET',
                url: '/Patients/getWaitingPatients'
            }).success(function (data) {


                angular.forEach(data, function (resdata) {


                    var BirthDate = new Date(ToJavaScriptDate(resdata.BirthDate))
                    resdata.BirthDate = BirthDate;


                });


                $ctrl.patientsList = data;

            }, function errorCallBack(data) {

            });
        }

        //DELETE WAITING IN SEARCH PATIENT LIST

        $ctrl.waitingDone = function (row) {


            $http({
                method: 'GET',
                url: '/Waiting/deleteWaitingPatient?id=' + row.WaitingListId + ''
            }).success(function (data) {

                if (data == "ok") {                    

                    var index = $ctrl.patientsList.indexOf(row);

                    $ctrl.patientsList.splice(index, 1);
                }

            }, function errorCallback(data) {

            });


        };
     

      


        $ctrl.allPatients();


 

  
    }
});

