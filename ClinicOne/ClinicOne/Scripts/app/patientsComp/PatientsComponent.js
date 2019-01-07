var patientsComponent = angular.module('patientsComponent', ['smart-table']);

patientsComponent.component('patient', {
    templateUrl: '/wwwroot/src/PatientsTemplate.html',
    controller: function MyController($http, $filter) {

        $("#addModal").on('hidden.bs.modal', function () {
            $(this).data('bs.modal', null);
        });


        var $ctrl = this;

        $ctrl.submitType;

        $ctrl.disableInput = false;

        $ctrl.showAge = false;


        //GET PATIENTSLIST


        $ctrl.patientsList = [];
        $ctrl.patientsListDisplay = [].concat($ctrl.patientsList);

       

        $ctrl.getPatients = function () {

            $http({
                method: 'GET',
                url: '/Patients/getPatients?key=' + $ctrl.searchKey +''
            }).success(function (data) {


                angular.forEach(data, function (resdata) {


                    var BirthDate = new Date(ToJavaScriptDate(resdata.BirthDate))
                    resdata.BirthDate = BirthDate;


                });


                $ctrl.patientsList = data;

            }, function errorCallBack(data) {

            });

        };

        //ADD PATIENTS

        $ctrl.showAddModal = function () {

            $ctrl.Patient = {

                Id: '',
                FirstName: '',
                MiddleName: '',
                LastName: '',
                Gender: '',
                BirthDate: '',
                Age: '',
                BloodType: '',
                ContactNumber1: '',
                ContactNumber2: '',
                Address1: '',
                Address2: ''

            };

            $('#addModal').modal('toggle');

            $ctrl.submitType = 'add';

            $('#modalTitle').text('New');

            $ctrl.disableInput = false;

            $ctrl.showAge = false;
        }

        $ctrl.Patient = {

            Id: '',
            FirstName: '',
            MiddleName: '',
            LastName: '',
            Gender: '',
            BirthDate: '',
            Age: '',
            BloodType: '',
            ContactNumber1: '',
            ContactNumber2: '',
            Address1: '',
            Address2: ''

        };

        $ctrl.updatePatient = function () {

            $ctrl.Patient.BirthDate = $filter('date')($ctrl.Patient.BirthDate, "yyyy-MM-dd HH:mm:ss");

            if ($ctrl.submitType == 'add') {

                $http({
                    method: 'POST',
                    url: '/Patients/addPatient',
                    data: $.param({ patient: $ctrl.Patient }),
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).success(function (data) {

                    var BirthDate = new Date(ToJavaScriptDate(data.BirthDate))
                    data.BirthDate = BirthDate;


                    $ctrl.patientsList.push(data);

                });

            }
            else {

                $http({
                    method: 'POST',
                    url: '/Patients/editPatient',
                    data: $.param({ patient: $ctrl.Patient }),
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).success(function (data) {



                });
            }


        }


        //EDIT PATIENTS


        $ctrl.showEditModal = function (row) {

            $('#addModal').modal('toggle');

            $('#modalTitle').text('Edit');

            $ctrl.Patient = row;

            $ctrl.submitType = 'edit';

            $ctrl.disableInput = false;

            $ctrl.showAge = false;
        }


        //DETAILS PATIENTS

        $ctrl.showDetailModal = function (row) {

            $('#addModal').modal('toggle');

            $('#modalTitle').text('Details');

            $ctrl.Patient = row;

            $ctrl.disableInput = true;

            $ctrl.showAge = true;

        }


        //DELETE PATIENTS



        $ctrl.showConfirmDeleteModal = function (row) {

            $ctrl.Patient = row;

            $('#deleteModal').modal('toggle');

            $('#deletePatientFullName').text(row.FirstName + ' ' + row.MiddleName + ' ' + row.LastName);
        }


        $ctrl.deletePatient = function () {


            $http({
                method: 'GET',
                url: '/Patients/delete?id=' + $ctrl.Patient.Id + ''
            }).success(function (data) {

                if (data == "ok") {

                    var index = $ctrl.patientsList.indexOf($ctrl.Patient);

                    $ctrl.patientsList.splice(index, 1);
                }

            }, function errorCallback(data) {

            });
        };


    }
});

